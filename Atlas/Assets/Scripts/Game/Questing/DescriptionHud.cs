using System;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Questing
{
    public class DescriptionHud : ALiveQuestConsumer
    {
        [SerializeField] private TextMeshProUGUI xp = null;
        [SerializeField] private TextMeshProUGUI description = null;
        [SerializeField] private Transform contentTransform = null;
        [SerializeField] private ObjectPool rewardPool = null;
        [SerializeField] private Button button = null;

        private LiveQuest _quest;
        private OnOkClickDelegate _onOkClickDelegate = null;
        
        public override void SetOnOkClickDelegate(OnOkClickDelegate _delegate)
        {
            _onOkClickDelegate = _delegate;
        }

        private void Awake()
        {
            button.onClick.AddListener(() => { _onOkClickDelegate?.Invoke(); });
        }

        public override void ConsumeLiveQuest(LiveQuest quest)
        {
            _quest = quest;
            xp.text = _quest.Quest.Xp + " XP";
            if (description != null)
                description.text = _quest.Quest.Description;
            RefreshHud();
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
    }
}
