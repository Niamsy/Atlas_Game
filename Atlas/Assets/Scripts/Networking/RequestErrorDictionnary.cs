using Localization;
using UnityEngine;

namespace Networking
{
	[CreateAssetMenu(fileName = "Requests error Dictionnary", menuName = "Localization/Request Error Dictionnary")]
	public class RequestErrorDictionnary : ScriptableObject
	{	
		public LocalizedText		UnknowError;
		public LocalizedText		ApiUnreachable;
		public LocalizedText 		ApiError;
		public LocalizedText 		AuthentificationWrongPairing;
		public LocalizedText 		RegistrationEmailAlreadyUsed;
		public LocalizedText 		RegistrationPasswordNotLongEnought;
	}
}
