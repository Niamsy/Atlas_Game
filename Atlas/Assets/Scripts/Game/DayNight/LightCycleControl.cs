using UnityEngine;
using UnityEngine.Experimental.Rendering.HDPipeline;
using UnityEngine.Rendering;

namespace Game.DayNight
{
    [RequireComponent(typeof(Light))]
    public class LightCycleControl : BaseDayNightCycleObj
    {
        
        #region Public Variables
        public AnimationCurve LightAngle = AnimationCurve.Linear(0, 180, 24, 360);
        public AnimationCurve LightIntensity = AnimationCurve.Linear(0, 0, 24, 0);
        public Gradient LightColor = new Gradient();
        #endregion

        #region Private Variables
        private Light _light;
        private float _latitude = 90;
        private float _longitude = 170;
        #endregion

        private void Awake()
        {
            _light = GetComponent<Light>();
        }
        
        protected override void UpdateScene(Date date, float dayAdvancement, float dayAdvancement01)
        {
            _light.intensity = LightIntensity.Evaluate(dayAdvancement);
            _light.enabled = (_light.intensity > 0);
            if (_light.enabled)
            {
                _light.transform.localRotation =
                    Quaternion.Euler(LightAngle.Evaluate(dayAdvancement) - _latitude, _longitude, 0);
                _light.color = LightColor.Evaluate(dayAdvancement01);
            }
        }
    }
}
