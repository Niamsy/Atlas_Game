using System.Collections;
using Game.Player.Stats;
using System.Collections.Generic;
using SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Game.SavingSystem;

namespace Menu.LevelSelector
{
    public class LevelSelector : MenuWidget
    {
        private LiveLevelInfo  _currentLevel;

        [Header("Prefab Level")]
        [SerializeField]
        public GameObject PrefabLevel = null;
        
        [SerializeField]
        public CharacterDataInfo CharacterInfo = null;

        [SerializeField] private Sprite CompletedChallenge = null;

        protected override void InitialiseWidget()
        {
        }

        public void UpdateWidget(List<LiveLevelInfo> levels, int nbChallengeComplete)
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
                    else if (ui.name == "Challenge1" || ui.name == "Challenge2" || ui.name == "Challenge3")
                    {
                        SetImageSprite(ui, levels[i]);
                    }
                    else if (ui.name == "PanelLocked")
                    {
                        if (i >= 1)
                        {
                            Debug.Log("Has unlock " + nbChallengeComplete);
                            Debug.Log("to Unlock" + levels[i].LevelInfo.ChallengeOnThisLevelToUnlockComplete);
                        }
                        if (nbChallengeComplete >= levels[i].LevelInfo.ChallengeOnThisLevelToUnlockComplete)
                        {
                            CanvasGroup panel = ui.GetComponent<CanvasGroup>();
                            if (panel)
                            {
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
                                    challengeAsking.text = levels[i].LevelInfo.ChallengeOnThisLevelToUnlockComplete.ToString();
                                }
                            }
                        }
                    }
                    else if (ui.name == "LaunchLevelButton")
                    {
                        Button btn = ui.GetComponent<Button>();
                        if (!disableLaunch)
                        {
                            btn.enabled = false;
                            break;
                        }
                        int sceneIndex = i + 2;
                        btn.onClick.AddListener(() => {
                            Debug.Log("Scene to load :" + sceneIndex);
                            SceneLoader.Instance.LoadScene(sceneIndex, 1);
                            });
                    }
                }
            }
        }

        private void SetImageSprite(Component ui, LiveLevelInfo level)
        {
            var img = ui.GetComponent<Image>();
            if (!level.ChallengeTwoComplete)
            {
                var tempColor = img.color;
                tempColor.a = 0.5f;
                img.color = tempColor;
            }
            else
            {
                if (CompletedChallenge != null)
                {
                    img.sprite = CompletedChallenge;
                }
                else
                {
                    img.color = new Color(166f, 212f, 153, 200f);
                }
            }
        }
    }
}