using UnityEngine;
using UnityEngine.Experimental.Rendering.HDPipeline;
using UnityEngine.Experimental.VFX;
using UnityEngine.Rendering;

namespace Game.DayNight
{
    public class DayNightCycle : BaseDayNightCycleObj
    {
        #region Public Variables

        [Header("Scene Ambiance")]
        public Volume            DayVolume;
        public Volume            NightVolume;
        public AnimationCurve    DayBlendOverDay = AnimationCurve.Linear(0, 0, 24, 0);

        [Header("Sun")]
        public Light            Sun;
        public AnimationCurve   SunPosition = AnimationCurve.Linear(0, 180, 24, 360);
        public AnimationCurve   SunIntensity = AnimationCurve.Linear(0, 0, 24, 0);
        public Gradient         SunColor = new Gradient();
        
        [Header("Moon")]
        public Light            Moon;
        public AnimationCurve   MoonPosition = AnimationCurve.Linear(0, 180, 24, 360);
        public AnimationCurve   MoonIntensity = AnimationCurve.Linear(0, 0, 24, 0);
        public Gradient         MoonColor = new Gradient();

        public VisualEffect     Stars;
        private readonly int    _starsDisplay = Shader.PropertyToID("Display");
        #endregion

        #region Private Variables
        private float _latitude = 90;
        private float _longitude = 170;

        [Header("Events")]
        [SerializeField] private AtlasEvents.Event _sunHide = null;
        [SerializeField] private AtlasEvents.Event _sunShow = null;
        #endregion

        protected override void UpdateScene(Date date, float dayAdvancement, float dayAdvancement01)
        {
            Sun.intensity = SunIntensity.Evaluate(dayAdvancement);
            Moon.intensity = MoonIntensity.Evaluate(dayAdvancement);
            
            Sun.enabled = (Sun.intensity > Moon.intensity);
            Moon.enabled = (Moon.intensity > Sun.intensity);
            
            if (Sun.enabled)
            {
                if (_sunShow) _sunShow.Raise();
                Sun.transform.localRotation =
                    Quaternion.Euler(SunPosition.Evaluate(dayAdvancement) - _latitude, _longitude, 0);
                Sun.color = SunColor.Evaluate(dayAdvancement01);
            }

            if (Moon.enabled)
            {
                if (_sunHide) _sunHide.Raise();
                Moon.transform.localRotation =
                    Quaternion.Euler(MoonPosition.Evaluate(dayAdvancement) - _latitude, _longitude, 0);
                Moon.color = MoonColor.Evaluate(dayAdvancement01);
            }
            Stars.SetBool(_starsDisplay, Moon.enabled);

            DayVolume.weight = DayBlendOverDay.Evaluate(dayAdvancement);
            NightVolume.weight = 1-DayBlendOverDay.Evaluate(dayAdvancement);
        }
    }
}
