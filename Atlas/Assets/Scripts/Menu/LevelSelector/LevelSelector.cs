using Game.Player.Stats;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        [SerializeField]
        public CharacterDataInfo CharacterInfo;

        [SerializeField]
        public LoadLevel load;

        protected override void InitialiseWidget()
        {
            if (Levels.Count > 0)
            {
                _currentLevel = Levels[0];
            }
            bool disableLaunch = false;
            for (int i = 0; i < Levels.Count; ++i)
            {
                disableLaunch = false;
                GameObject level = Instantiate(PrefabLevel, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                level.transform.SetParent(GameObject.Find("ListLevels").transform, false);
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
                            timePlayed.text = CharacterInfo.GetTimePlayedToString();
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
                            if (CharacterInfo.PlayerChallengeOwned >= Levels[i].ChallengeOnThisLevelToUnlockComplete)
                            {
                                CanvasGroup panel = ui.GetComponent<CanvasGroup>();
                                if (panel)
                                {
                                    Debug.Log("hola " + i);
                                    panel.alpha = 0;
                                    panel.interactable = false;
                                    disableLaunch = true;
                                }
                            }
                            else
                            {
                                foreach (Transform uipanel in ui.GetComponentsInChildren<Transform>())
                                {
                                    if (ui.name == "NumberChallengeComplete")
                                    {
                                        TextMeshProUGUI challengeComplete = ui.GetComponent<TextMeshProUGUI>();
                                        challengeComplete.text = Levels[i].NumberChallengeComplete().ToString();
                                    }
                                    if (ui.name == "NumberChallengeAsking")
                                    {
                                        TextMeshProUGUI challengeAsking = ui.GetComponent<TextMeshProUGUI>();
                                        challengeAsking.text = Levels[i].ChallengeOnThisLevelToUnlockComplete.ToString();
                                    }
                                }
                            }
                        }
                        else if (ui.name == "LaunchLevelButton")
                        {
                            Button btn = ui.GetComponent<Button>();
                            if (!disableLaunch)
                            {
                                Debug.Log("holo " + i);
                                btn.enabled = false;
                                break;
                            }
                            if (btn.enabled == true)
                            {
                                Debug.Log("I can launch scene : " + i);
                            }
                            string sceneName = Levels[i].LevelSceneName;
                            Debug.Log("Btn name: " + btn.name);
                            Debug.Log("Scene name launched: " + sceneName);
                            btn.onClick.AddListener(() => load.LoadSceneIndex(sceneName));
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
