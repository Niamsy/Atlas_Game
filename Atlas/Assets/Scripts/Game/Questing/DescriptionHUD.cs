using TMPro;
using Tools;
using UnityEngine;

namespace Game.Questing
{
    public class DescriptionHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI xp = null;
        [SerializeField] private TextMeshProUGUI description = null;
        [SerializeField] private Transform contentTransform = null;
        [SerializeField] private ObjectPool rewardPool = null;

        private Quest _quest = null;
        
        public void SetData(Quest quest)
        {
            _quest = quest;
            xp.text = _quest.Xp + " XP";
            description.text = _quest.Description;
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
            foreach (var reward in _quest.Rewards)
            {
                var rewardObj = rewardPool.GetObject();
                rewardObj.transform.SetParent(contentTransform);
                
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
