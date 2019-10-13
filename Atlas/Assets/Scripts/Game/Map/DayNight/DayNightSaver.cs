using Game.SavingSystem;
using Game.SavingSystem.Datas;
using UnityEngine;

namespace Game.Map.DayNight
{
    [RequireComponent(typeof(CalendarManager))]
    public class DayNightSaver : MapSavingBehaviour
    {
        private CalendarManager _calendar;

        protected override void SavingMapData(MapData data)
        {
            if (data.CalendarData == null)
                data.CalendarData = new DateData(_calendar.ActualDate);
            else
                data.CalendarData.Save(_calendar.ActualDate);
        }

        protected override void LoadingMapData(MapData data)
        {
            _calendar.ActualDate.Set(data.CalendarData.Day, data.CalendarData.Month, data.CalendarData.Year, data.CalendarData.Hours, data.CalendarData.Minutes, data.CalendarData.Seconds);
        }

        protected override void Awake()
        {
            base.Awake();
            _calendar = GetComponent<CalendarManager>();
        }
    }
}
