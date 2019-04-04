using System;
using System.Collections.Generic;
using Plants.Plant;
using UnityEngine;
using Variables;

namespace Plants
{
    /// <summary>
    /// TODO: To finish
    /// </summary>
    public class PlantSaver : MonoBehaviour
    {
        public PlantModel plant;
        private float _LastSavedTime = 0f;
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
        public struct StockProd
        {
            public List<Game.ResourcesManagement.Resource>      objects;
            public int                  count;
            public int                  limit;
        }

        [Serializable]
        public struct StockCons
        {
            public List<Game.ResourcesManagement.Resource>      objects;
            public int                  count;
            public int                  limit;
        }

        [Serializable]
        public struct SaveProd
        {
            public int              rate;
            public int              quantity;
            public StockProd        stock;
            public bool             starverd;
            public FloatReference   range;
        }

        [Serializable]
        public struct SaveCons
        {
            public int              rate;
            public int              quantity;
            public StockCons        stock;
            public int              starvationTimeLimit;
            public bool             starved;
            public float   range;
        }

        [Serializable]
        public struct PlantData
        {
            public SaveVector       position;
            public SaveQuaternion   rotation;
            public SaveVector       scale;
            public List<SaveProd>   prod;
            public List<SaveCons>   cons;

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

        public void Save()
        {
            GameControl.control.gameData.PlantData.SetFromTransform(plant.transform);
        }

        private void Awake()
        {
            plant.transform.position = GameControl.control.gameData.PlantData.GetPosition();
            plant.transform.rotation = GameControl.control.gameData.PlantData.GetRotation();
            plant.transform.localScale = GameControl.control.gameData.PlantData.GetScale();
            _LastSavedTime = Time.time;
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.time - _LastSavedTime > _SaveFrequency)
            {
                Save();
                _LastSavedTime = Time.time;
            }
        }
    }
}
