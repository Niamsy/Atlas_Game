using UnityEngine;
using System;
using Game;
using Game.SavingSystem;

namespace Player {
    public class PlayerSaver : MonoBehaviour {
        [Tooltip("Save the player data every X seconds")]
        public int _SaveFrequency = 5;
        public bool _ResetPosition = false;

        private float _LastSavedTime = 0f;

        public void Save()
        {
            if (!_ResetPosition)
                SaveManager.Instance.MapData.TransformData.SetFromTransform(transform);
        }

        private void Awake()
        {
            SaveManager.Instance.
            transform.position = SaveManager.Instance.MapData.TransformData.Position.Value;
            transform.rotation = SaveManager.Instance.MapData.TransformData.Rotation.Value;
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