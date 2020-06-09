using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.HUD
{
    public class RefreshPopupHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _successPopup = null;
        [SerializeField] private GameObject _failurePopup = null;
        [SerializeField] private TextMeshProUGUI _failurePopupText = null;
        [SerializeField] private float _displayTime = 0;

        private float _timer;
        private bool _displaying = false;

        private void Start()
        {
            HidePopups();    
        }

        private void HandlePopup(bool state, string message)
        {
            _successPopup.SetActive(state);
            _failurePopup.SetActive(!state);
            _failurePopupText.text += message;
            _displaying = true;
            _timer = 0;
        }

        private void HidePopups()
        {
            _successPopup.SetActive(false);
            _failurePopup.SetActive(false);
            _displaying = false;
        }

        public void RefreshResult(bool result, string message) 
        {
            HandlePopup(result, message);
            _failurePopupText.text = "";
        }

        void Update()
        {
            if (_displaying)
            {
                _timer += Time.fixedDeltaTime;
                if (_timer > _displayTime)
                    HidePopups();
            }
        }
    }
}
