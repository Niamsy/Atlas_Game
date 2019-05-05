using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class SaveData
    {
        [Serializable]
        public struct SaveVector
        {
            public float x;
            public float y;
            public float z;

            public Vector3 Value { get { return (new Vector3(x, y, z)); } }
            public SaveVector(Vector3 value)
            {
                x = value.x;
                y = value.y;
                z = value.z;
            }
        }

        [Serializable]
        public struct SaveQuaternion
        {
            public float w;
            public float x;
            public float y;
            public float z;
            
            public Quaternion Value { get { return (new Quaternion(x, y, z, w)); } }
            public SaveQuaternion(Quaternion quaternion)
            {
                w = quaternion.w;
                x = quaternion.x;
                y = quaternion.y;
                z = quaternion.z;
            }
        }
        
        [Serializable]
        public struct TransformSaveData
        {
            public SaveVector       Position;
            public SaveQuaternion   Rotation;

            public TransformSaveData(Transform transform)
            {
                Position = new SaveVector(transform.position);
                Rotation = new SaveQuaternion(transform.rotation);
            }
            
            public void SetFromTransform(Transform transform)
            {
                Position = new SaveVector(transform.position);
                Rotation = new SaveQuaternion(transform.rotation);
            }
        }
    }
}
