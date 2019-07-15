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
        public string LevelDescription => (_levelDescription.name);
        [SerializeField] private Sprite _levelImage = null;
        public Sprite LevelImage => (_levelImage);

        private bool _challenge_one_complete = false;
        public bool ChallengeOneComplete => (_challenge_one_complete);

        private bool _challenge_two_complete = false;
        public bool ChallengeTwoComplete => (_challenge_two_complete);

        private bool _challenge_three_complete = false;
        public bool ChallengeThreeComplete => (_challenge_three_complete);

        [Header("Level")]
        [SerializeField] private int            _challengeOnThisLevelComplete = 0;
        public int                              ChallengeOnThisLevelComplete => (_challengeOnThisLevelComplete);
    }
}
