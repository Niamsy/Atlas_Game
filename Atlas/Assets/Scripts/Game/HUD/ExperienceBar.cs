using Game.HUD.Commons;
using Leveling;
using UnityEngine;
using UnityEngine.UI;

namespace Game.HUD
{
    public class ExperienceBar : MonoBehaviour, ILevelingEventListener
    {
        #region Variables
        #region ObjReferences
        [Header("Float value")]
        [SerializeField] private Variables.FloatVariable _experience = null;
        [SerializeField] private Variables.FloatVariable _level = null;
        [SerializeField] private Variables.FloatVariable _levelFloor = null;
        [SerializeField] private Variables.FloatVariable _levelRoof = null;
        [Header("Objects references")]
        [SerializeField] private HUDJauge _jauge = null;
        [SerializeField] private Text _levelDisplay = null;
        [SerializeField] private LevelingEvent _gainXp = null;
        #endregion
        
        #region Parameters
        [Header("Parameters variables")]
        [Tooltip("Number of seconds after the bar finished loading we should keep the display on")]
        public float DisplayLength = 2f;
        [Tooltip("{0} Contains the int of the actual level")]
        public string LevelDisplayFormat = "Lv. {0}";
        #endregion

        #region Private variables
        private float _lastKnowLevel = 0;
        private float _timeBeforeFadeOut = 0;
        private bool _isDisplaying;
        #endregion
        #endregion

        #region Methods
        private void Awake()
        {
            _gainXp.RegisterListener(this);
            // TODO: Can't access Max Exp per level now
            _jauge.Initialize(_experience.Value, _levelFloor.Value, _levelRoof.Value);
            _lastKnowLevel = _level.Value;
            _levelDisplay.text = string.Format(LevelDisplayFormat, _level.Value);
        }

        public void OnEventRaised(int currentXp, int xpGain)
        {
            if (_level.Value != _lastKnowLevel)
                _jauge.Initialize(_experience.Value, _levelFloor.Value, _levelRoof.Value);
            else
                _jauge.SetValue(_experience.Value);
            if (_levelDisplay != null)
                _levelDisplay.text = string.Format(LevelDisplayFormat, _level.Value);
            Display();
        }

        private void Update()
        {
            if (_isDisplaying && !_jauge.IsMoving)
            {
                _timeBeforeFadeOut -= Time.deltaTime;
                if (_timeBeforeFadeOut < 0)
                    Hide();
            }
        }

        public void Display()
        {
            _timeBeforeFadeOut = DisplayLength;
            _isDisplaying = true;
        }
        
        public void Hide()
        {
            _timeBeforeFadeOut = 0;
            _isDisplaying = false;
        }
        #endregion
    }
}
