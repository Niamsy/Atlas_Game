using System.Collections;
using System.Collections.Generic;
using Menu;
using UnityEngine;

namespace Game.Notification
{
    public class NotificationMenu : MenuWidget
    {
        [SerializeField] private NotificationHud notificationHud = null;
        [Range(1f, 60f)]
        [SerializeField] private float notificationDuration = 5f;
        [Range(0f, 3f)]
        [SerializeField] private float delayBetweenNotifications = 0.5f;
        
        private Stack<Notification> notifications = new Stack<Notification>();
        
        protected override void InitialiseWidget()
        {
             
        }

        public void SetData(Notification notification)
        {
            if (notificationHud == null) return;

            if (Displayed)
            {
                notifications.Push(notification);
                return;
            }
            
            notificationHud.SetData(notification);

            Show(true);
            Invoke(nameof(CloseAfter), notificationDuration);
        }

        private void SetNextNotification()
        {
            if (notifications.Count > 0)
            {
                notificationHud.SetData(notifications.Pop());
            }
            
            Show(true);
            Invoke(nameof(CloseAfter), notificationDuration);
        }

        private void CloseAfter()
        {
            if (notifications.Count > 0)
            {
                Invoke(nameof(SetNextNotification), delayBetweenNotifications);                
            }
            Show(false);
        }
    }
}
