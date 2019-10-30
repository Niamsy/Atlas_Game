using SceneManagement;
using UnityEditor;
using UnityEngine;

namespace Networking.Editor
{
    [CustomEditor(typeof(RequestManager))]
    public class RequestManagerEditor : UnityEditor.Editor
    {
        public string Name = "";
        public string Password = "";

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            RequestManager myTarget = (RequestManager) target;

            if (Application.isPlaying)
            {
                if (!myTarget.IsConnected())
                {
                    Name = EditorGUILayout.TextField("Email", Name);
                    Password = EditorGUILayout.PasswordField("Password", Password);
                    if (GUILayout.Button("Connect"))
                    {
                        if (myTarget.Connect(Name, Password))
                        {
                        }
                        else
                        {
                            
                        }
                    }
                }
                else
                {
                    GUILayout.Label("Connected");
                }
            }
        }
    }
}
