using UnityEngine;
using Game.SavingSystem;
using Game.SavingSystem.Datas;

namespace Leveling
{
    public abstract class Experience : MapSavingBehaviour
    {
        #region Public Accessors
        public int Level
        {
            get { return (int)_Level.Value; }
            set { _Level.Value = value; }
        }

        public int CurrentXP
        {
            get { return (int)_CurrentXP.Value; }
            set { _CurrentXP.Value = value; }
        }

        public int LevelFloor
        {
            get { return (int)_LevelFloor.Value; }
            set { _LevelFloor.Value = value; }
        }

        public int LevelRoof
        {
            get { return (int)_LevelRoof.Value; }
            set { _LevelRoof.Value = value; }
        }

        public bool CanGainXP
        {
            get { return _CanGainXP; }
            private set { }
        }

        public bool Reset
        {
            get { return _ResetExperience; }
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
        [SerializeField]
        private bool RESET_IN_EDITOR = true;
        #endregion

        #region Public Methods
        protected override void SavingMapData(MapData data)
        {
            data.XPData.PlayerXP = CurrentXP;
            data.XPData.PlayerLevel = Level;
            data.XPData.RoofLevel = LevelRoof;
            data.XPData.FloorLevel = LevelFloor;
            data.XPData.NotFirstTime = !Reset;
        }

        protected override void LoadingMapData(MapData data)
        {
            if (data.XPData.NotFirstTime)
            {
                CurrentXP = (int)data.XPData.PlayerXP;
                LevelFloor = (int)data.XPData.FloorLevel;
                LevelRoof = (int)data.XPData.RoofLevel;
                Level = (int)data.XPData.PlayerLevel;
            }
            else
            {
                ResetXP();
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
                    _LevelRoof.Value = CalculateNextLevelXPNeeded(Level + 1, LevelRoof);
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

        private void ResetXP()
        {
            _CurrentXP.Value = 0;
            _LevelFloor.Value = 0;
            _LevelRoof.Value = CalculateNextLevelXPNeeded(2, 0);
            _Level.Value = 1;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

#if UNITY_EDITOR
            if (RESET_IN_EDITOR)
            {
                ResetXP();
            }
#else
            if (Reset)
            {
                ResetXP();
            }         
#endif
        }
        #endregion
    }

}