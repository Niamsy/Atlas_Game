using UnityEngine;
using UnityEngine.Experimental.Rendering.HDPipeline;
using UnityEngine.Rendering;

namespace Game.DayNight
{
   
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    public class DayNightCycle : MonoBehaviour
    {
#if UNITY_EDITOR
        public bool PreviewInEditor = true;
#endif
        
        #region Public Variables

        [Header("Scene Ambiance")]
        public Volume DayVolume;
        public Volume NightVolume;
        public AnimationCurve DayBlendOverDay = AnimationCurve.Linear(0, 0, 24, 0);

        [Header("Sun")]
        public Light Sun;
        public AnimationCurve SunPosition = AnimationCurve.Linear(0, 180, 24, 360);
        public AnimationCurve SunIntensity = AnimationCurve.Linear(0, 0, 24, 0);
        public Gradient SunColor = new Gradient();
        
        [Header("Moon")]
        public Light Moon;
        public AnimationCurve MoonPosition = AnimationCurve.Linear(0, 180, 24, 360);
        public AnimationCurve MoonIntensity = AnimationCurve.Linear(0, 0, 24, 0);
        public Gradient MoonColor = new Gradient();
        #endregion

        #region Private Variables
        [SerializeField]
        private CalendarManager _calendar;
        private float _latitude = 90;
        private float _longitude = 170;
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
            UpdateScene(_calendar.ActualDate);
        }

        void UpdateScene(Date date)
        {
            var dayAdvancement = date.DayAdvancement;
            var dayAdvancement01 = date.DayAdvancement / Date.HourPerDay;

            Sun.intensity = SunIntensity.Evaluate(dayAdvancement);
            Sun.enabled = (Sun.intensity > 0);
            if (Sun.enabled)
            {
                Sun.transform.localRotation =
                    Quaternion.Euler(SunPosition.Evaluate(dayAdvancement) - _latitude, _longitude, 0);
                Sun.color = SunColor.Evaluate(dayAdvancement01);
            }

            Moon.intensity = MoonIntensity.Evaluate(dayAdvancement);
            Moon.enabled = (Moon.intensity > 0);
            if (Moon.enabled)
            {
                Moon.transform.localRotation =
                    Quaternion.Euler(MoonPosition.Evaluate(dayAdvancement) - _latitude, _longitude, 0);
                Moon.color = MoonColor.Evaluate(dayAdvancement01);
            }

            DayVolume.weight = DayBlendOverDay.Evaluate(dayAdvancement);
            NightVolume.weight = 1-DayBlendOverDay.Evaluate(dayAdvancement);
        }

        private void SetLatitude(float value)
        {
            _latitude = value;
        }

        public void SetLongitude(float value)
        {
            _longitude = value; 
        }
    }
}
