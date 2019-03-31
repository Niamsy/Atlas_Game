using Game.DayNight;
using UnityEngine;
using UnityEngine.UI;

namespace Game.HUD
{
    public class HoursDisplay : MonoBehaviour
    {
        [SerializeField] private Text _text;

        private CalendarManager _calendar;

        private void Start()
        {
            _calendar = CalendarManager.Instance;
        }
        
        private void Update()
        {
            _text.text = _calendar.Hours.ToString("00") + ":" + _calendar.Minutes.ToString("00");
        }
    }
}
