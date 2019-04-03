using UnityEngine;
using System;

namespace Player {
    public class PlayerSaver : MonoBehaviour {
        [Tooltip("Save the player data every X seconds")]
        public int _SaveFrequency = 5;

        [Serializable]
        public struct SaveVector
        {
            public float x;
            public float y;
            public float z;
        }

        [Serializable]
        public struct SaveQuaternion
        {
            public float w;
            public float x;
            public float y;
            public float z;
        }

        [Serializable]
        public struct PlayerData
        {
            public SaveVector       position;
            public SaveQuaternion   rotation;
            public SaveVector       scale;

            public void SetFromTransform(Transform transform)
            {
                // Positiion;
                position.x = transform.position.x;
                position.y = transform.position.y;
                position.z = transform.position.z;

                // Rotation
                rotation.w = transform.rotation.w;
                rotation.x = transform.rotation.x;
                rotation.y = transform.rotation.y;
                rotation.z = transform.rotation.z;

                //Scale
                scale.x = transform.localScale.x;
                scale.y = transform.localScale.y;
                scale.z = transform.localScale.z;
            }

            public Vector3 GetPosition()
            {
                return new Vector3(position.x, position.y, position.z); ;
            }

            public Quaternion GetRotation()
            {
                return new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w);
            }

            public Vector3 GetScale()
            {
                return new Vector3(scale.x, scale.y, scale.z);
            }
        }

        private float _LastSavedTime = 0f;

        public void Save()
        {
            GameControl.control.gameData.PlayerData.SetFromTransform(transform);
        }

        private void Awake()
        {
            transform.position = GameControl.control.gameData.PlayerData.GetPosition();
            transform.rotation = GameControl.control.gameData.PlayerData.GetRotation();
            transform.localScale = GameControl.control.gameData.PlayerData.GetScale();
            _LastSavedTime = Time.time;
        }

        // Update is called once per frame
        void Update() {
            if (Time.time - _LastSavedTime > _SaveFrequency)
            {
                Save();
                _LastSavedTime = Time.time;
            }
        }
    }
}