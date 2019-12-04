using Game.SavingSystem.Datas;
using UnityEngine;

namespace Menu.LevelSelector
{
    public class LiveLevelInfo
    {
        public LevelInfo LevelInfo = null;
        
        private bool _challenge_one_complete = false;

        public bool ChallengeOneComplete
        {
            get => (_challenge_one_complete);
            set { _challenge_one_complete = value; }
        }

        private bool _challenge_two_complete = false;
        public bool ChallengeTwoComplete
        {
            get => (_challenge_two_complete);
            set { _challenge_two_complete = value; }
        }

        private bool _challenge_three_complete = false;
        public bool ChallengeThreeComplete
        {
            get => (_challenge_three_complete);
            set { _challenge_three_complete = value; }
        }

        public LiveLevelInfo(LevelInfo levelInfo, levelInfoData levelInfoData)
        {
            LevelInfo = levelInfo;
            ChallengeOneComplete = levelInfoData.Challenge1Complete;
            ChallengeTwoComplete = levelInfoData.Challenge2Complete;
            ChallengeThreeComplete = levelInfoData.Challenge3Complete;
        }

        public LiveLevelInfo(LevelInfo levelInfo)
        {
            LevelInfo = levelInfo;
            ChallengeOneComplete = false;
            ChallengeTwoComplete = false;
            ChallengeThreeComplete = false;
        }
        
        public int NumberChallengeComplete()
        {
            return ((ChallengeOneComplete) ? 1 : 0) + ((ChallengeTwoComplete) ? 1 : 0) + ((ChallengeThreeComplete) ? 1 : 0);
        }
        
        public bool                             IsComplete => NumberChallengeComplete() == 3;
    }
}
