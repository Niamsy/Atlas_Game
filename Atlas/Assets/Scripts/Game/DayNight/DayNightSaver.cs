using Game.SavingSystem;
using Game.SavingSystem.Datas;
using UnityEngine;

namespace Game.DayNight
{
    [RequireComponent(typeof(CalendarManager))]
    public class DayNightSaver : MapSavingBehaviour
    {
        private CalendarManager _calendar;

        protected override void SavingMapData(MapData data)
        {
            data.CalendarData = _calendar.ActualDate;
        }

        protected override void Awake()
        {
            base.Awake();
            _calendar = GetComponent<CalendarManager>();
        }
    }
}
