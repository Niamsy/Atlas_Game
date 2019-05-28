using UnityEngine;

namespace Game.DayNight
{
    [RequireComponent(typeof(CalendarManager))]
    public class DayNightSaver : MonoBehaviour
    {
        private CalendarManager _calendar;
        private float _lastSavedTime = 0f;

        public void Save()
        {
            GameControl.Instance.GameData.CalendarData = _calendar.ActualDate;
        }

        private void Awake()
        {
            _calendar = GetComponent<CalendarManager>();
            _calendar.ActualDate = GameControl.Instance.GameData.CalendarData;
            _lastSavedTime = Time.time;
        }

        void Update()
        {
            if (Time.time - _lastSavedTime > 1.0f)
            {
                Save();
                _lastSavedTime = Time.time;
            }
        }
    }
}
