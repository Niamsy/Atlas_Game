using Game.DayNight;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.HUD
{
    public class HoursDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text = null;

        private CalendarManager _calendar = null;

        private void Start()
        {
            _calendar = CalendarManager.Instance;
        }
        
        private void Update()
        {
            if (_text && _calendar)
                _text.text = _calendar.ActualDate.Hours.ToString("00") + ":" + _calendar.ActualDate.Minutes.ToString("00");
        }
    }
}
