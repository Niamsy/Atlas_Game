using Localization;
using UnityEngine;

namespace Networking
{
	[CreateAssetMenu(fileName = "Requests error Dictionnary", menuName = "Localization/Request Error Dictionnary")]
	public class RequestErrorDictionnary : ScriptableObject
	{	
		[Header("Commons")]
		public LocalizedText		UnknowError;
		public LocalizedText		ApiUnreachable;
		public LocalizedText 		ApiError;

		[Header("Authentication")]
		public LocalizedText 		AuthentificationWrongPairing;
		
		[Header("Registration")]
		public LocalizedText 		RegistrationEmailAlreadyUsed;
		public LocalizedText 		RegistrationPasswordNotLongEnought;
		
		[Header("Reset password")]
		public LocalizedText 		ResetPasswordNoAccount;
	}
}
