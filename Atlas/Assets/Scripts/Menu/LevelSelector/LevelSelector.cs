using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menu.LevelSelector
{
    public class LevelSelector : MenuWidget
    {
        private LevelInfo       _currentLevel;
        [SerializeField]
        public List<LevelInfo>  Levels;

        protected override void InitialiseWidget()
        {
            if (Levels.Count > 0)
            {
                _currentLevel = Levels[0];
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
