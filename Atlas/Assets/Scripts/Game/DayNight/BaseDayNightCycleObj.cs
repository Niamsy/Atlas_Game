﻿using UnityEngine;
using UnityEngine.Experimental.Rendering.HDPipeline;
using UnityEngine.Rendering;

namespace Game.DayNight
{
   
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    public abstract class BaseDayNightCycleObj : MonoBehaviour
    {
#if UNITY_EDITOR
        public bool PreviewInEditor = true;
#endif
        #region Private Variables
        [SerializeField]
        private CalendarManager _calendar;
        #endregion


        private void Start()
        {
            
#if UNITY_EDITOR
            if (Application.isPlaying)
#endif
            _calendar = CalendarManager.Instance;
        }

        private void Update ()
        {
            
#if UNITY_EDITOR
            if (!PreviewInEditor && !Application.isPlaying)
                return;
#endif
            Date date = _calendar.ActualDate;
            var dayAdvancement = date.DayAdvancement;
            var dayAdvancement01 = date.DayAdvancement / Date.HourPerDay;
            UpdateScene(date, dayAdvancement, dayAdvancement01);
        }

        protected abstract void UpdateScene(Date date, float dayAdvancement, float dayAdvancement01);
    }
}
