using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Questing
{
    public class RewardHud : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI rewardName = null;
        [SerializeField] private Image image = null;

        public void SetData(Reward reward)
        {
            var item = reward.reward;
            rewardName.text = item.Name + " x" + reward.Count;
            if (image.sprite != null)
            {
                image.sprite = item.Sprite;
            }
            if (item.Category != null)
            {
                image.color = item.Category.Color;
            }
        }
    }
}
