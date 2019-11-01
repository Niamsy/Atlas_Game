using Localization;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.Questing
{
    public class AnnouncementHud : ALiveQuestConsumer
    {
        [SerializeField] private TextMeshProUGUI announcement = null;
        [SerializeField] private TextMeshProUGUI title = null;
        [SerializeField] private Image image = null;

        [FormerlySerializedAs("newQuest")] 
        [SerializeField] private LocalizedText announcementTitle = null;

        [FormerlySerializedAs("questSprite")] 
        [SerializeField] private Sprite announcementSprite = null;

        public override void ConsumeLiveQuest(LiveQuest liveQuest)
        {
            image.sprite = announcementSprite;
            title.text = liveQuest.Quest.Name;
            announcement.text = announcementTitle;
        }

        public override void SetOnOkClickDelegate(OnOkClickDelegate _delegate)
        {
        }
    }
}
