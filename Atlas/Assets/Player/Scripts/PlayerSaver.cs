using UnityEngine;
using System;
using Game;

namespace Player {
    public class PlayerSaver : MonoBehaviour {
        [Tooltip("Save the player data every X seconds")]
        public int _SaveFrequency = 5;


        private float _LastSavedTime = 0f;

        public void Save()
        {
            GameControl.Control.GameData.TransformData.SetFromTransform(transform);
        }

        private void Awake()
        {
            transform.position = GameControl.Control.GameData.TransformData.Position.Value;
            transform.rotation = GameControl.Control.GameData.TransformData.Rotation.Value;
            transform.localScale = GameControl.Control.GameData.TransformData.Scale.Value;
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