﻿using UnityEngine;
using UnityEngine.Events;

namespace AtlasEvents
{
    public class EventListener : MonoBehaviour, IEventListener
    {
        [Tooltip("Event to register with.")]
        public Event Event;

        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEvent Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            Response.Invoke();
        }
    }
}