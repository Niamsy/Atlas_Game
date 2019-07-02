using Game.DayNight;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.HUD
{
    public class DateDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text = null;
        [SerializeField] private LocalizedTextArray _months = null;
        private CalendarManager _calendar = null;

        private void Start()
        {
            _calendar = CalendarManager.Instance;
        }

        private void Update()
        {
            if (_calendar && _text && _months)
            {
                int day = _calendar.ActualDate.Day + 1;
                _text.text = day.ToString("00") + " " + _months.Entries[_calendar.ActualDate.Month];
            }
        }
    }
}
