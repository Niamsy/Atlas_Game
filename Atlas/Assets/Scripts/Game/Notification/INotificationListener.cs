using UnityEngine;

namespace Game.Notification
{
    public interface INotificationListener
    {
        void OnEventRaised(Notification notification);
    }
}
