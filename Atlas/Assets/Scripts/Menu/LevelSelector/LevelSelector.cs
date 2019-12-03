using System.Collections;
using Game.Player.Stats;
using System.Collections.Generic;
using SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.LevelSelector
{
    public class LevelSelector : MenuWidget
    {
        private LiveLevelInfo  _currentLevel;

        [Header("Prefab Level")]
        [SerializeField]
        public GameObject PrefabLevel;
        
        [SerializeField]
        public CharacterDataInfo CharacterInfo;

        protected override void InitialiseWidget()
        {
        }

        public void UpdateWidget(List<LiveLevelInfo> levels)
        {
            if (levels.Count > 0)
            {
                _currentLevel = levels[0];
            }

            for (int i = 0; i < levels.Count; ++i)
            {
                var disableLaunch = false;
                GameObject level = Instantiate(PrefabLevel, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                level.transform.SetParent(GameObject.Find("ListLevels").transform, false);
                level.name = "Level " + i;
                GameObject child = level.transform.Find("Level").gameObject;
                var transforms = child.GetComponentsInChildren<Transform>();
                foreach (Transform ui in transforms)
                {
                    if (ui.name == "LevelTitle")
                    {
                        TextMeshProUGUI levelTitle = ui.GetComponent<TextMeshProUGUI>();
                        levelTitle.text = levels[i].LevelInfo.LevelTitle;
                    }
                    else if (ui.name == "LevelDescription")
                    {
                        TextMeshProUGUI levelDesc = ui.GetComponent<TextMeshProUGUI>();
                        levelDesc.text = levels[i].LevelInfo.LevelDescription;
                    }
                    else if (ui.name == "PlayTime")
                    {
                        TextMeshProUGUI timePlayed = ui.GetComponent<TextMeshProUGUI>();
                        timePlayed.text = CharacterInfo.GetTimePlayedToString();
                    }
                    else if (ui.name == "LevelImage")
                    {
                        Image img = ui.GetComponent<Image>();
                        img.sprite = levels[i].LevelInfo.LevelImage;
                    }
                    else if (ui.name == "Challenge1")
                    {
                        if (!levels[i].ChallengeOneComplete)
                        {
                            Image img = ui.GetComponent<Image>();
                            var tempColor = img.color;
                            tempColor.a = 0.5f;
                            img.color = tempColor;
                        }
                    }
                    else if (ui.name == "Challenge2")
                    {
                        if (!levels[i].ChallengeTwoComplete)
                        {
                            Image img = ui.GetComponent<Image>();
                            var tempColor = img.color;
                            tempColor.a = 0.5f;
                            img.color = tempColor;
                        }
                    }
                    else if (ui.name == "Challenge3")
                    {
                        if (!levels[i].ChallengeThreeComplete)
                        {
                            Image img = ui.GetComponent<Image>();
                            var tempColor = img.color;
                            tempColor.a = 0.5f;
                            img.color = tempColor;
                        }
                    }
                    else if (ui.name == "PanelLocked")
                    {
                        if (CharacterInfo.PlayerChallengeOwned >= levels[i].ChallengeOnThisLevelToUnlockComplete)
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
                                    challengeComplete.text = levels[i].NumberChallengeComplete().ToString();
                                }
                                if (ui.name == "NumberChallengeAsking")
                                {
                                    TextMeshProUGUI challengeAsking = ui.GetComponent<TextMeshProUGUI>();
                                    challengeAsking.text = levels[i].ChallengeOnThisLevelToUnlockComplete.ToString();
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
                        string sceneName = levels[i].LevelInfo.LevelSceneName;
                        Debug.Log("Btn name: " + btn.name);
                        Debug.Log("Scene name launched: " + sceneName);
                        btn.onClick.AddListener(() => SceneLoader.Instance.LoadScene(2, 1));
                    }
                }
            }
        }
    }
}