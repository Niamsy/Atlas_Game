using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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
		private string _ApiToken = null;
		public bool IsConnected() { return (_ApiToken != null);}
		#endregion

		#region NetworkManager Status
		private Coroutine 				_actualOperation = null;
		public bool 					CanReceiveANewRequest
		{
			get { return (_actualOperation == null); }
		}
		#endregion
		
		#region JSon body
		[Serializable]
		private sealed class Body_ReturnBase
		{
			public string message = "";
		}
		
		[Serializable]
		private sealed class Body_ReturnApiToken
		{
			public string message = "";
			public string api_token = "";
		}
		
		[Serializable]
		private sealed class Body_SendRegister
		{
			public string username = "";
			public string email = "";
			public string password = "";
		}
		
		
		#endregion
		
		public delegate void RequestFinishedDelegate(bool sucess, string message);

		
		#endregion
		/// <summary>
		/// Initialize the manager
		/// </summary>
		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}

		/// <summary>
		/// Clean the manager to be ready to receive the next request
		/// </summary>
		private void CleanForNextRequest()
		{
			_actualOperation = null;
		}
		
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

			Body_ReturnApiToken bodyReturn = JsonUtility.FromJson<Body_ReturnApiToken>(postRequest.downloadHandler.text);

			if (success)
				_ApiToken = bodyReturn.api_token;
			else
				Debug.Log("ERROR HTTP: " + postRequest.responseCode + ":" + postRequest.error);
			
			CleanForNextRequest();
			if (OnConnectionFinished != null)
				OnConnectionFinished(success, bodyReturn.message);
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
			Body_SendRegister body = new Body_SendRegister {username = username, email = email, password = password};
			string bodyJson = JsonUtility.ToJson(body) ?? "";
			
			UnityWebRequest postRequest = UnityWebRequest.Put(ApiAdress + RegisterPath, bodyJson);
			postRequest.method = UnityWebRequest.kHttpVerbPOST;
			postRequest.SetRequestHeader("Content-Type", "application/json");
			yield return postRequest.SendWebRequest();

			bool success = (postRequest.responseCode == 200);

			Body_ReturnApiToken bodyReturn = JsonUtility.FromJson<Body_ReturnApiToken>(postRequest.downloadHandler.text);

			if (success)
				_ApiToken = bodyReturn.api_token;
			else
				Debug.Log("ERROR HTTP: " + postRequest.responseCode + ":" + bodyReturn.message + ". For " + bodyJson);

			CleanForNextRequest();
			if (OnRegisterFinished != null)
				OnRegisterFinished(success, bodyReturn.message);
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
			postRequest.SetRequestHeader("api_token", _ApiToken);
			yield return postRequest.SendWebRequest();

			bool success = (postRequest.responseCode == 200);

			Body_ReturnBase bodyReturn = JsonUtility.FromJson<Body_ReturnBase>(postRequest.downloadHandler.text);

			if (success)
				_ApiToken = null;
			else
				Debug.Log("ERROR HTTP: " + postRequest.responseCode + ":" + postRequest.error);
			
			CleanForNextRequest();
			if (OnDisconnectionFinished != null)
				OnDisconnectionFinished(success, bodyReturn.message);
		}

		public event RequestFinishedDelegate OnDisconnectionFinished;

		#endregion
	}
}
