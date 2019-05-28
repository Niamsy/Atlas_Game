using UnityEngine;
using System;
using Game;

namespace Player {
    public class PlayerSaver : MonoBehaviour {
        [Tooltip("Save the player data every X seconds")]
        public int _SaveFrequency = 5;
        public bool _ResetPosition = false;


        private float _LastSavedTime = 0f;

        public void Save()
        {
            if (!_ResetPosition)
                GameControl.Instance.GameData.TransformData.SetFromTransform(transform);
        }

        private void Awake()
        {
            transform.position = GameControl.Instance.GameData.TransformData.Position.Value;
            transform.rotation = GameControl.Instance.GameData.TransformData.Rotation.Value;
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