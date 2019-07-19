﻿using Game.SavingSystem;
using Game.SavingSystem.Datas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Variables;

namespace Game.Player.Stats
{
    public class CharacterDataInfo : AccountSavingBehaviour
    {
        #region Public Accessors

        public int PlayerChallengeOwned
        {
            get { return _playerChallengeOwned; }
            set { _playerChallengeOwned = value; }
        }

        public int PlayerTimePlayed
        {
            get
            {
                if (_playerTimePlayed == null)
                    return 0;
                return (int)_playerTimePlayed.Value;
            }
            set { _playerTimePlayed.Value = value; }
        }

        #endregion

        #region Private Members

        private int     _playerChallengeOwned = 0;

        private FloatVariable   _playerTimePlayed = null;

        #endregion

        #region Public Methods
        protected override void SavingAccountData(AccountData data)
        {
            data.CharacterGlobalInfo.PlayerChallengeOwned = PlayerChallengeOwned;
            data.CharacterGlobalInfo.PlayerTimePlayed = PlayerTimePlayed;
        }

        protected override void LoadingAccountData(AccountData data)
        {
            PlayerTimePlayed = data.CharacterGlobalInfo.PlayerTimePlayed;
            PlayerChallengeOwned = data.CharacterGlobalInfo.PlayerChallengeOwned;
        }

        public void NewChallengeSucceed()
        {
            PlayerChallengeOwned++;
        }

        public void AmountTimePlayed(int time)
        {
            PlayerTimePlayed += time;
        }

        public string GetTimePlayedToString()
        {
            int Day = PlayerTimePlayed / (24 * 60);
            int TimeLeft = (PlayerTimePlayed - (Day * 24 * 60));
            int Hours = TimeLeft / 60;
            int Min = TimeLeft - (Hours * 60);
            return Day.ToString() + ':' + Hours.ToString() + ':' + Min.ToString();
        }
        #endregion
    }
}