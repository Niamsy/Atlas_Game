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
            GameControl.Control.GameData.CalendarData = _calendar.ActualDate;
        }

        private void Awake()
        {
            _calendar = GetComponent<CalendarManager>();
            _calendar.ActualDate = GameControl.Control.GameData.CalendarData;
            _lastSavedTime = Time.time;
        }

        // Update is called once per frame
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
