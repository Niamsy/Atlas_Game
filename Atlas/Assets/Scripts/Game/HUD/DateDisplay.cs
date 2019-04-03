using Game.DayNight;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.HUD
{
    public class DateDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private LocalizedTextArray _months;
        private CalendarManager _calendar;

        private void Start()
        {
            _calendar = CalendarManager.Instance;
        }

        private void Update()
        {
            int day = _calendar.Day + 1;
            _text.text = day.ToString("00") + " " + _months.Entries[_calendar.Month];
        }
    }
}
