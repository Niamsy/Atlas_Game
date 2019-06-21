using Localization;
using UnityEngine;

namespace Menu.Main
{
    public class RetreivePasswordSucessWidget : RequestManagerWidget
    {
        [SerializeField] private LocalizedTextBehaviour _text;
        
        #region Initialisation/Destruction
        protected override void InitialiseWidget() {}
        #endregion
        
        #region Request
        public void SetEmail(string email)
        {
            _text.FormatArgs = new string[1] { email };
        }
        #endregion
    }
}
