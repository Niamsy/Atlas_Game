using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leveling
{
    public abstract class Experience : MonoBehaviour
    {
        #region Public Accessors
        public int Level
        {
            get { return (int)_Level.Value; }
            private set { }
        }

        public int CurrentXP
        {
            get { return (int)_CurrentXP.Value; }
            private set { }
        }

        public int LevelFloor
        {
            get { return (int)_LevelFloor.Value; }
            private set { }
        }

        public int LevelRoof
        {
            get { return (int)_LevelRoof.Value; }
            private set { }
        }
        #endregion

        #region Private Members
        [SerializeField]
        private Variables.FloatVariable _Level;
        [SerializeField]
        private Variables.FloatVariable _LevelFloor;
        [SerializeField]
        private Variables.FloatVariable _LevelRoof;
        [SerializeField]
        private Variables.FloatVariable _CurrentXP;
        [SerializeField]
        private LevelingEvent _EGainXP;
        [SerializeField]
        private LevelingEvent _EGainLevel;
        [SerializeField]
        private LevelingEvent _ECannotGainXP;
        [SerializeField]
        private LevelingEvent _ELoseXP;
        [SerializeField]
        private LevelingEvent _ELostLevel;
        [SerializeField]

        private bool _CanGainXP = true;
        [SerializeField]
        private bool _ResetExperience = false;
        #endregion

        #region Public Methods
        public void Start()
        {
            if (_ResetExperience)
            {
                _CurrentXP.Value = 0;
                _LevelFloor.Value = 0;
                _LevelRoof.Value = CalculateNextLevelXPNeeded(2, 0);
                _Level.Value = 1;
                _ResetExperience = false;
            }
        }

        public void EnableXPGain()
        {
            _CanGainXP = true;
        }

        public void DisableXPGain()
        {
            _CanGainXP = false;
        }

        public void Gain(int XPValue)
        {
            Gain(XPValue, 1);
        }
        public void Gain(int XPValue, int Modifier)
        {
            int value = XPValue * Modifier;

            if (_CanGainXP)
            {
                if (value > 0 && _EGainXP)
                {
                    _EGainXP.Raise(CurrentXP, value);
                }
                else if (value < 0 && _ELoseXP)
                {
                    _ELoseXP.Raise(CurrentXP, value);
                }

                if (CurrentXP + value >= LevelRoof)
                {
                    if (_EGainLevel)
                    {
                        _EGainLevel.Raise(CurrentXP, value);
                    }
                    _Level.Value += 1;
                    _CurrentXP.Value += value;
                    _LevelFloor.Value = _LevelRoof.Value;
                    _LevelRoof.Value = CalculateNextLevelXPNeeded(Level, LevelRoof);
                }
                else if (CurrentXP + value < LevelFloor && LevelFloor > 0)
                {
                    if (_ELostLevel)
                    {
                        _ELostLevel.Raise(CurrentXP, value);
                    }

                    _Level.Value -= 1;
                    _CurrentXP.Value += value;
                    _LevelRoof.Value = _LevelFloor.Value;
                    _LevelFloor.Value = CalculateNextLevelXPNeeded(Level, LevelRoof);

                }
            }
            else
            {
                if (_ECannotGainXP)
                {
                    _ECannotGainXP.Raise(CurrentXP, value);
                }
            }
        }
        #endregion

        #region Protected and Private methods
        protected abstract int CalculateNextLevelXPNeeded(int NextLevel, int CurrentXPNeed);
        #endregion
    }

}