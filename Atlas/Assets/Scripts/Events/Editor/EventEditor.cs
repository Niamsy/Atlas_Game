using UnityEditor;
using UnityEngine;

namespace AtlasEvents
{
    [CustomEditor(typeof(AtlasEvents.Event))]
    public class EventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            AtlasEvents.Event e = target as AtlasEvents.Event;
            if (GUILayout.Button("Raise"))
                e.Raise();
        }
    }
}