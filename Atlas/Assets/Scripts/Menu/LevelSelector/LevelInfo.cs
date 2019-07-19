using Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menu.LevelSelector
{
    [CreateAssetMenu(fileName = "LevelInfo", menuName = "Menu/LevelInfo", order = 1)]
    public class LevelInfo : ScriptableObject
    {
        [Header("LevelInfo")]
        [SerializeField] private LocalizedText  _levelTitle = null;
        public string                           LevelTitle => (_levelTitle.name);
        [SerializeField] private string         _timePlayed = null;
        public string                           TimePlayed => (_timePlayed);
        [SerializeField] private LocalizedText  _levelDescription = null;
        public string LevelDescription =>       (_levelDescription.name);
        [SerializeField] private Sprite         _levelImage = null;
        public Sprite LevelImage =>             (_levelImage);
        [SerializeField] private string         _levelSceneNameToLoad = null;
        public string LevelSceneName =>         (_levelSceneNameToLoad);

        private bool _challenge_one_complete = false;
        public bool ChallengeOneComplete => (_challenge_one_complete);

        private bool _challenge_two_complete = false;
        public bool ChallengeTwoComplete => (_challenge_two_complete);

        private bool _challenge_three_complete = false;
        public bool ChallengeThreeComplete => (_challenge_three_complete);

        public int NumberChallengeComplete()
        {
            return ((_challenge_one_complete) ? 1 : 0) + ((_challenge_two_complete) ? 1 : 0) + ((_challenge_three_complete) ? 1 : 0);
        }

        [Header("Level")]
        [SerializeField] private int            _challengeOnThisLevelToUnlock = 0;
        public int                              ChallengeOnThisLevelToUnlockComplete => (_challengeOnThisLevelToUnlock);
    }
}
