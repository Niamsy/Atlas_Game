using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Questing
{
    public class RewardHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI rewardName = null;
        [SerializeField] private Image image = null;

        public void SetData(Reward reward)
        {
            rewardName.text = reward.Name;
            image.sprite = reward.reward.Sprite;
        }
    }
}
