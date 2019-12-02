using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Notification
{
    public class NotificationHud : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title = null;
        [SerializeField] private TextMeshProUGUI description = null;
        [SerializeField] private Image image = null;

        public void SetData(Notification notification)
        {
            if (title != null)
            {
                title.text = notification.Title;
            }

            if (description != null)
            {
                description.text = notification.Description;
            }

            if (image != null)
            {
                image.sprite = notification.Sprite;
                image.color = notification.Color;
            }
        }
    }
}
