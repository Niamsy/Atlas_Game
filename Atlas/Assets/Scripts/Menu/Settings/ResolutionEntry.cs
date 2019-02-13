using UnityEngine;
using UnityEngine.UI;

namespace Menu.Settings
{
    public class ResolutionEntry : MonoBehaviour
    {
        [SerializeField] private Text _text;

        public void SetResolution(Resolution resolution)
        {
            _text.text = resolution.height + "x" + resolution.width;
        }
    }
}
