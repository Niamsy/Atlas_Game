using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.LevelSelector
{
    public class LevelSelector : MenuWidget
    {
        private LevelInfo       _currentLevel;

        [Header("Prefab Level")]
        [SerializeField]
        public GameObject PrefabLevel;

        [SerializeField]
        public List<LevelInfo>  Levels;

        protected override void InitialiseWidget()
        {
            if (Levels.Count > 0)
            {
                _currentLevel = Levels[0];
            }
            for (int i = 0; i < Levels.Count; ++i)
            {
                GameObject level = Instantiate(PrefabLevel, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                level.transform.parent = GameObject.Find("ListLevels").transform;
                level.name = "Level " + i;
                GameObject child = level.transform.Find("Level").gameObject;
                foreach (Transform ui in child.GetComponentsInChildren<Transform>())
                {
                    Debug.Log(ui.name);
                    if (ui && ui.name == "LevelTitle")
                    {
                        Text levelTitle = ui.GetComponent<Text>();
                        levelTitle.text = Levels[i].LevelTitle;
                    }
                    else if (ui && ui.name == "LevelDescription")
                    {
                        Text levelDesc = ui.GetComponent<Text>();
                        levelDesc.text = Levels[i].LevelDescription;
                    }
                    else if (ui && ui.name == "PlayTime")
                    {
                        Text timePlayed = ui.GetComponent<Text>();
                        timePlayed.text = Levels[i].TimePlayed;
                    }
                    else if (ui && ui.name == "PanelLocked")
                    {
                        // TODO Make General Data for this game. Like Number Challenge complete. Change this condition
                        if (Levels[i].ChallengeOnThisLevelComplete > 0)
                        {
                            GameObject panel = ui.GetComponent<GameObject>();
                            if (panel)
                                panel.SetActive(false);
                        }
                    }
                }
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
