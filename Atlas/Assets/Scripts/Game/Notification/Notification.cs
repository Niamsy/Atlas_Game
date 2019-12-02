using Localization;
using UnityEngine;

namespace Game.Notification
{
    [CreateAssetMenu(fileName = "NotificationData", menuName = "Notification/Notification")]
    public class Notification : ScriptableObject
    {
        [SerializeField] private LocalizedText title = null;
        [SerializeField] private LocalizedText description = null;
        [SerializeField] private Sprite sprite = null;
        [SerializeField] private Color color = Color.white;
        
        public LocalizedText Title => title;
        public LocalizedText Description => description;
        public Sprite Sprite => sprite;
        public Color Color => color;
    }
}
