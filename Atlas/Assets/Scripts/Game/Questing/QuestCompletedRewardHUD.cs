using TMPro;
using Tools;
using UnityEngine;

namespace Game.Questing
{
    public class QuestCompletedRewardHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI xp = null;
        [SerializeField] private Transform contentTransform = null;
        [SerializeField] private ObjectPool rewardPool = null;

        private LiveQuest _quest;
        
        public void SetData(LiveQuest quest)
        {
            _quest = quest;
            xp.text = _quest.Quest.Xp + " XP";
            RefreshHUD();
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
                var rewardHud = rewardObj.GetComponent<RewardHUD>();
                rewardHud.SetData(reward);
            }
        }

        private void RefreshHUD()
        {
            ClearRewards();
            AddRewards();
        }
    }
}