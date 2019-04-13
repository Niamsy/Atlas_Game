using Game.HUD.Commons;
using Leveling;
using UnityEngine;
using UnityEngine.UI;

namespace Game.HUD
{
    [RequireComponent(typeof(Animator))]
    public class ExperienceBar : MonoBehaviour, ILevelingEventListener
    {
        #region Variables
        #region ObjReferences
        [Header("Float value")]
        [SerializeField] private Variables.FloatVariable _experience;
        [SerializeField] private Variables.FloatVariable _level;
        [SerializeField] private Variables.FloatVariable _levelRoof;
        [Header("Objects references")]
        [SerializeField] private HUDJauge _jauge;
        [SerializeField] private Text _levelDisplay;
        [SerializeField] private LevelingEvent _gainXp;
        private Animator _animator;
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
        private readonly int _hashDisplay = Animator.StringToHash("Display");
        #endregion
        #endregion

        #region Methods
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _gainXp.RegisterListener(this);
            // TODO: Can't access Max Exp per level now
            _jauge.Initialize(_experience.Value, _levelRoof.Value);
            _lastKnowLevel = _level.Value;
            _levelDisplay.text = string.Format(LevelDisplayFormat, _level.Value);
        }

        public void OnEventRaised(int currentXp, int xpGain)
        {
            if (_level.Value != _lastKnowLevel)
                _jauge.Initialize(_experience.Value, _levelRoof.Value);
            else
                _jauge.SetValue(_experience.Value);
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
            _animator.SetBool(_hashDisplay, _isDisplaying);
        }
        
        public void Hide()
        {
            _timeBeforeFadeOut = 0;
            _isDisplaying = false;
            _animator.SetBool(_hashDisplay, _isDisplaying);
        }
        #endregion
    }
}
