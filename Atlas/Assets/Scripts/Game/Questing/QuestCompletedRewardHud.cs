using System;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Questing
{
    public class QuestCompletedRewardHud : ALiveQuestConsumer
    {
        [SerializeField] private TextMeshProUGUI xp = null;
        [SerializeField] private Transform contentTransform = null;
        [SerializeField] private ObjectPool rewardPool = null;
        [SerializeField] private Button okButton = null;

        private LiveQuest _quest;
        private OnOkClickDelegate _onOkClickDelegate;

        public override void ConsumeLiveQuest(LiveQuest liveQuest)
        {
            _quest = liveQuest;
            xp.text = _quest.Quest.Xp + " XP";
            RefreshHud();
        }
        
        

        public override void SetOnOkClickDelegate(OnOkClickDelegate _delegate)
        {
            _onOkClickDelegate = _delegate;
        }

        private void ClearRewards()
        {
            while (contentTransform.childCount > 0)
            {
                var toRemove = contentTransform.GetChild(0).gameObject;
                rewardPool.ReturnObject(toRemove);
            }
        }

        private void AddRewards()
        {
            foreach (var reward in _quest.Quest.Rewards)
            {
                var rewardObj = rewardPool.GetObject();
                rewardObj.transform.SetParent(contentTransform);
                rewardObj.transform.localScale = new Vector3(1, 1, 1);
                var rewardHud = rewardObj.GetComponent<RewardHud>();
                rewardHud.SetData(reward);
            }
        }

        private void RefreshHud()
        {
            ClearRewards();
            AddRewards();
        }

        private void Awake()
        {
            okButton.onClick.AddListener(() => _onOkClickDelegate?.Invoke());
        }
    }
}