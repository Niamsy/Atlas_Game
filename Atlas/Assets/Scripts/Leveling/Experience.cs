﻿using System.Collections;
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
        private Variables.FloatVariable _Level = null;
        [SerializeField]
        private Variables.FloatVariable _LevelFloor = null;
        [SerializeField]
        private Variables.FloatVariable _LevelRoof = null;
        [SerializeField]
        private Variables.FloatVariable _CurrentXP = null;
        [SerializeField]
        private LevelingEvent _EGainXP = null;
        [SerializeField]
        private LevelingEvent _EGainLevel = null;
        [SerializeField]
        private LevelingEvent _ECannotGainXP = null;
        [SerializeField]
        private LevelingEvent _ELoseXP = null;
        [SerializeField]
        private LevelingEvent _ELostLevel = null;
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
                else if (CurrentXP + value < LevelFloor && LevelFloor > 1)
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
                else
                {
                    _CurrentXP.Value += value;
                }
                if (value > 0 && _EGainXP)
                {
                    _EGainXP.Raise(CurrentXP, value);
                }
                else if (value < 0 && _ELoseXP)
                {
                    _ELoseXP.Raise(CurrentXP, value);
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