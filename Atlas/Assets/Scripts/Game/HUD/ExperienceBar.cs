using Game.HUD.Commons;
using Leveling;
using UnityEngine;

namespace Game.HUD
{
    [RequireComponent(typeof(Animator))]
    public class ExperienceBar : MonoBehaviour, ILevelingEventListener
    {
        #region Variables
        #region ObjReferences
        [Header("Objects references")]
        [SerializeField] private HUDJauge _jauge;
        [SerializeField] private Variables.FloatVariable _experience;
        [SerializeField] private Variables.FloatVariable _levelRoof;
        [SerializeField] private LevelingEvent _gainXp;
        private Animator _animator;
        #endregion
        
        #region Parameters
        [Header("Parameters variables")]
        [Tooltip("Number of seconds after the bar finished loading we should keep the display on")]
        public float DisplayLength = 2f;
        #endregion

        #region Private variables
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
        }

        public void OnEventRaised(int currentXp, int xpGain)
        {
            _jauge.SetValue(_experience.Value);
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
