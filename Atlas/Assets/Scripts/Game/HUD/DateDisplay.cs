using Game.DayNight;
using UnityEngine;
using UnityEngine.UI;

namespace Game.HUD
{
    public class DateDisplay : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private LocalizedTextArray _months;
        private CalendarManager _calendar;

        private void Start()
        {
            _calendar = CalendarManager.Instance;
        }

        private void Update()
        {
            _text.text = _calendar.Day.ToString("00") + " " + _months.Entries[_calendar.Month];
        }
    }
}
