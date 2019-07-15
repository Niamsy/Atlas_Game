using InputManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Game.HUD.Commons
{
    public class HUDKeyDisplay : MonoBehaviour
    {
        public InputKey KeyListened;
        public Text Keyboard_TextDisplay;
    
        private void Awake()
        {
            Keyboard_TextDisplay.text = KeyListened.Default.Value;
        }
    }
}
