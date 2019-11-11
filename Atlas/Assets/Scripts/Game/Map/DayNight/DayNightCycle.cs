using Game.Map.DayNight;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Game.DayNight
{
    public class DayNightCycle : BaseDayNightCycleObj
    {
        #region Public Variables

        [Header("Scene Ambiance")]
        public PostProcessVolume   DayVolume;
        public PostProcessVolume   NightVolume;
        public AnimationCurve      DayBlendOverDay = AnimationCurve.Linear(0, 0, 24, 0);
        public Gradient            AmbientColor = new Gradient();
        public AnimationCurve      SkyExposure = AnimationCurve.Linear(0, 0, 24, 0);
        public AnimationCurve      SkyThickness = AnimationCurve.Linear(0, 0, 24, 0);

        [Header("Sun")]
        public Light               Sun;
        public AnimationCurve      SunPosition = AnimationCurve.Linear(0, 180, 24, 360);
        public AnimationCurve      SunIntensity = AnimationCurve.Linear(0, 0, 24, 0);

        [Header("Moon")]
        public Light               Moon;
        public AnimationCurve      MoonPosition = AnimationCurve.Linear(0, 180, 24, 360);
        public AnimationCurve      MoonIntensity = AnimationCurve.Linear(0, 0, 24, 0);

        public ParticleSystem      Stars;
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

            RenderSettings.ambientSkyColor = AmbientColor.Evaluate(dayAdvancement01);
            if (Sun.enabled)
            {
                if (_sunShow) _sunShow.Raise();
                Sun.transform.localRotation =
                    Quaternion.Euler(SunPosition.Evaluate(dayAdvancement) - _latitude, _longitude, 0);
                
            }

            if (Moon.enabled)
            {
                if (_sunHide) _sunHide.Raise();
                Moon.transform.localRotation =
                    Quaternion.Euler(MoonPosition.Evaluate(dayAdvancement) - _latitude, _longitude, 0);
            }
            if (Stars.isPlaying && !Moon.enabled)
                Stars.Stop();
            if (!Stars.isPlaying && Moon.enabled)
                Stars.Play();

            RenderSettings.skybox.SetFloat("_AtmosphereThickness", SkyThickness.Evaluate(dayAdvancement));
            RenderSettings.skybox.SetFloat("_Exposure", SkyExposure.Evaluate(dayAdvancement));
            DayVolume.weight = DayBlendOverDay.Evaluate(dayAdvancement);
            NightVolume.weight = 1-DayBlendOverDay.Evaluate(dayAdvancement);
        }
    }
}
