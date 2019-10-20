using Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Questing
{
    public class AnnouncementHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI announcement = null;
        [SerializeField] private TextMeshProUGUI title = null;
        [SerializeField] private Image image = null;

        [SerializeField] private LocalizedText newQuest = null;
        [SerializeField] private LocalizedText newObjective = null;

        [SerializeField] private Sprite questSprite = null;
        [SerializeField] private Sprite objectiveSprite = null;
        
        public void SetData(LiveQuest quest)
        {
            announcement.text = newQuest;
            title.text = quest.Quest.Name;
            image.sprite = questSprite;
        }

        public void SetData(Objective objective)
        {
            announcement.text = newObjective;
            title.text = objective.Goal;
            image.sprite = objectiveSprite;
        }
    }
}
