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
        public string                           LevelTitle => (_levelTitle);
        [SerializeField] private string         _timePlayed = null;
        public string                           TimePlayed => (_timePlayed);
        [SerializeField] private LocalizedText  _levelDescription = null;
        public string LevelDescription =>       (_levelDescription);
        [SerializeField] private Sprite         _levelImage = null;
        public Sprite LevelImage =>             (_levelImage);
        [SerializeField] private string         _levelSceneNameToLoad = null;
        public string LevelSceneName =>         (_levelSceneNameToLoad);
        
        [SerializeField] private int            _challengeOnThisLevelToUnlock = 0;
        public int                              ChallengeOnThisLevelToUnlockComplete => (_challengeOnThisLevelToUnlock);
    }
}
