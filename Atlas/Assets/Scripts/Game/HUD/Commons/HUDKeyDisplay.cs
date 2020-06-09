using Game.SavingSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Game.HUD.Commons
{
    public class HUDKeyDisplay : MonoBehaviour
    {
        public string KeyListened;
        public Text Keyboard_TextDisplay;
        
        private void Awake()
        {
            if (KeyListened != "")
            {
                Keyboard_TextDisplay.text = SaveManager.Instance.InputControls.Player.Get().FindAction(KeyListened).controls[0].name.ToUpper();
            }
        }
    }
}
