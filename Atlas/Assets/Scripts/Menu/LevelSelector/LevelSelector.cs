using System.Collections;
using System.Collections.Generic;
using TMPro;
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
                    if (ui)
                    {
 
                        if (ui.name == "LevelTitle")
                        {
                            TextMeshProUGUI levelTitle = ui.GetComponent<TextMeshProUGUI>();
                            levelTitle.text = Levels[i].LevelTitle;
                        }
                        else if (ui.name == "LevelDescription")
                        {
                            TextMeshProUGUI levelDesc = ui.GetComponent<TextMeshProUGUI>();
                            levelDesc.text = Levels[i].LevelDescription;
                        }
                        else if (ui.name == "PlayTime")
                        {
                            TextMeshProUGUI timePlayed = ui.GetComponent<TextMeshProUGUI>();
                            if (Levels[i].TimePlayed.Length > 0)
                                timePlayed.text = Levels[i].TimePlayed;
                        }
                        else if (ui.name == "LevelImage")
                        {
                            Image img = ui.GetComponent<Image>();
                            img.sprite = Levels[i].LevelImage;
                        }
                        else if (ui.name == "Challenge1")
                        {
                            if (!Levels[i].ChallengeOneComplete)
                            {
                                Image img = ui.GetComponent<Image>();
                                var tempColor = img.color;
                                tempColor.a = 0.5f;
                                img.color = tempColor;
                            }
                        }
                        else if (ui.name == "Challenge2")
                        {
                            if (!Levels[i].ChallengeTwoComplete)
                            {
                                Image img = ui.GetComponent<Image>();
                                var tempColor = img.color;
                                tempColor.a = 0.5f;
                                img.color = tempColor;
                            }
                        }
                        else if (ui.name == "Challenge3")
                        {
                            if (!Levels[i].ChallengeThreeComplete)
                            {
                                Image img = ui.GetComponent<Image>();
                                var tempColor = img.color;
                                tempColor.a = 0.5f;
                                img.color = tempColor;
                            }
                        }
                        else if (ui.name == "PanelLocked")
                        {
                            // TODO Make General Data for this game. Like Number Challenge complete. Change this condition
                            Debug.Log(Levels[i].ChallengeOnThisLevelComplete);

                            if (Levels[i].ChallengeOnThisLevelComplete >= 0)
                            {
                                GameObject panel = ui.GetComponent<GameObject>();
                                if (panel)
                                    panel.SetActive(false);
                            }
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
