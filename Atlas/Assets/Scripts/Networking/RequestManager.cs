using System;
using System.Collections;
using System.Collections.Generic;
using Localization;
using UnityEngine;
using UnityEngine.Networking;

namespace Networking
{
	public class RequestManager : MonoBehaviour
	{
		#region Variables
		#region API Adress & Request adress
		#if ATLAS_RELEASE
				public static string ApiAdress { get { return ("http://163.5.84.246:3000/"); } }
		#else
				public static string ApiAdress { get { return ("http://163.5.84.246:3001/"); } }
		#endif

		public static string ConnectionPath { get { return ("user/authentication"); } }
		public static string DisconnectionPath { get { return ("disconnection"); } }
		public static string RegisterPath { get { return ("user/registration"); } }

		#endregion

		#region API Token
		private string _apiToken = null;
		public bool IsConnected() { return (_apiToken != null);}
		#endregion

		#region NetworkManager Status
		private Coroutine 				_actualOperation = null;
		public bool 					CanReceiveANewRequest
		{
			get { return (_actualOperation == null && _quiting == false); }
		}
		#endregion
		
		#region JSon body
		[Serializable]
		private sealed class BodyReturnBase
		{
			public string Message = "";
		}
		
		[Serializable]
		private sealed class BodyReturnApiToken
		{
			public string Message = "";
			public string ApiToken = "";
		}
		
		[Serializable]
		private sealed class BodySendRegister
		{
			public string Username = "";
			public string Email = "";
			public string Password = "";
		}
		
		
		#endregion
		
		public delegate void RequestFinishedDelegate(bool sucess, string message);

		[SerializeField] private RequestErrorDictionnary _errorDictionnary;
		#endregion

		/// <summary>
		/// Initialize the manager
		/// </summary>
		private void Awake()
		{
//			errorCode.GetLocaleValue(LocalizationManager.Instance.CurrentLanguage);
			
			DontDestroyOnLoad(gameObject);
			Application.wantsToQuit += StartQuitSequence;
		}
		
		/// <summary>
		/// Clean the manager to be ready to receive the next request
		/// </summary>
		private void CleanForNextRequest()
		{
			_actualOperation = null;
		}

		#region Disconnection on Quit
		private bool _quiting;
		private bool StartQuitSequence()
		{
			if (_apiToken == null)
				return true;

			if (_quiting)
				return (false);
			
			if (_actualOperation != null)
				StopCoroutine(_actualOperation);
			OnDisconnectionFinished += QuitOnAfterDisconnection;
			_actualOperation = StartCoroutine(DisconnectionCoroutine());
			_quiting = true;
			return false;
		}

		private void QuitOnAfterDisconnection(bool sucess, string message)
		{
			Debug.Log("Quit");
			Application.Quit();
		}
		#endregion
		
		#region Connection
		
		/// <summary>
		/// Launch a connection request
		/// </summary>
		/// <param name="username">The username to use</param>
		/// <param name="password">The password to use</param>
		/// <returns>Return true if the networkManager could process the request and false if another request was pending</returns>
		public bool Connect(string username, string password)
		{
			if (!CanReceiveANewRequest)
				return (false);
			
			
			_actualOperation = StartCoroutine(ConnectionCoroutine(username, password));
			
			return (true);
		}

		private IEnumerator ConnectionCoroutine(string username, string password)
		{
			UnityWebRequest postRequest = UnityWebRequest.Post(ApiAdress + ConnectionPath, "");
			postRequest.SetRequestHeader("username", username);
			postRequest.SetRequestHeader("password", password);
			yield return postRequest.SendWebRequest();

			bool success = (postRequest.responseCode == 200);

			BodyReturnApiToken bodyReturn = JsonUtility.FromJson<BodyReturnApiToken>(postRequest.downloadHandler.text);

			string errorMsg = "";
			if (success)
				_apiToken = bodyReturn.ApiToken;
			else
			{
				Debug.Log("ERROR HTTP: " + postRequest.responseCode + ":" + postRequest.error);
				switch (postRequest.responseCode)
				{
					case (500):
						errorMsg = _errorDictionnary.ApiError;
						break;
					case (0):
						errorMsg = _errorDictionnary.ApiUnreachable;
						break;
					case (400):
						errorMsg = _errorDictionnary.AuthentificationWrongPairing;
						break;
					default:
						errorMsg = _errorDictionnary.UnknowError;
						break;
				}
			}

			CleanForNextRequest();
			if (OnConnectionFinished != null)
				OnConnectionFinished(success, errorMsg);
		}
		
		public event RequestFinishedDelegate OnConnectionFinished;
		#endregion

		#region Registration
		/// <summary>
		/// Launch a register request
		/// </summary>
		/// <param name="username">The username to use</param>
		/// <param name="email">The email to use</param>
		/// <param name="password">The password to use</param>
		/// <returns>Return true if the networkManager could process the request and false if another request was pending</returns>
		public bool Register(string username, string email, string password)
		{
			if (!CanReceiveANewRequest)
				return (false);
			
			
			_actualOperation = StartCoroutine(RegisterCoroutine(username, email, password));
			
			return (true);
		}

		private IEnumerator RegisterCoroutine(string username, string email, string password)
		{
			BodySendRegister body = new BodySendRegister {Username = username, Email = email, Password = password};
			string bodyJson = JsonUtility.ToJson(body) ?? "";
			
			UnityWebRequest postRequest = UnityWebRequest.Put(ApiAdress + RegisterPath, bodyJson);
			postRequest.method = UnityWebRequest.kHttpVerbPOST;
			postRequest.SetRequestHeader("Content-Type", "application/json");
			yield return postRequest.SendWebRequest();

			bool success = (postRequest.responseCode == 200);

			BodyReturnApiToken bodyReturn = JsonUtility.FromJson<BodyReturnApiToken>(postRequest.downloadHandler.text);

			string errorMsg = "";

			if (success)
				_apiToken = bodyReturn.ApiToken;
			else
			{
				Debug.Log("ERROR HTTP: " + postRequest.responseCode + ":" + postRequest.error);
				switch (postRequest.responseCode)
				{
					case (500):
						errorMsg = _errorDictionnary.ApiError;
						break;
					case (0):
						errorMsg = _errorDictionnary.ApiUnreachable;
						break;
					case (400):
						errorMsg = _errorDictionnary.RegistrationPasswordNotLongEnought;
						break;
					case (401):
						errorMsg = _errorDictionnary.RegistrationEmailAlreadyUsed;
						break;
					default:
						errorMsg = _errorDictionnary.UnknowError;
						break;
				}
			}
			
			CleanForNextRequest();
			if (OnRegisterFinished != null)
				OnRegisterFinished(success, errorMsg);
		}
		
		public event RequestFinishedDelegate OnRegisterFinished;
		#endregion
		
		#region Disconnection
		
		/// <summary>
		/// Launch a disconnection request
		/// </summary>
		/// <param name="username">The username to use</param>
		/// <param name="email">The email to use</param>
		/// <param name="password">The password to use</param>
		/// <returns>Return true if the networkManager could process the request and false if another request was pending</returns>
		public bool Disconnection()
		{
			if (!CanReceiveANewRequest || !IsConnected())
				return (false);
			
			
			_actualOperation = StartCoroutine(DisconnectionCoroutine());
			
			return (true);
		}

		private IEnumerator DisconnectionCoroutine()
		{
			UnityWebRequest postRequest = UnityWebRequest.Post(ApiAdress + DisconnectionPath, "");
			postRequest.SetRequestHeader("api_token", _apiToken);
			yield return postRequest.SendWebRequest();

			bool success = (postRequest.responseCode == 200);

//			BodyReturnBase bodyReturn = JsonUtility.FromJson<BodyReturnBase>(postRequest.downloadHandler.text);

			_apiToken = null;

			string errorMsg = "";

			if (!success)
			{
				Debug.Log("ERROR HTTP: " + postRequest.responseCode + ":" + postRequest.error);
				switch (postRequest.responseCode)
				{
					case (500):
						errorMsg = _errorDictionnary.ApiError;
						break;
					case (0):
						errorMsg = _errorDictionnary.ApiUnreachable;
						break;
					default:
						errorMsg = _errorDictionnary.UnknowError;
						break;
				}
			}
			
			CleanForNextRequest();
			if (OnDisconnectionFinished != null)
				OnDisconnectionFinished(success, errorMsg);
		}

		public event RequestFinishedDelegate OnDisconnectionFinished;

		#endregion
	}
}
