using UnityEngine;

namespace Tools
{
    [ExecuteInEditMode]
    public class KeepInRotation : MonoBehaviour
    {
        public bool FreezeX;
        public bool FreezeY;
        public bool FreezeZ;
        
        public Vector3 Rotation;

        public void Update()
        {
            var actual = transform.rotation.eulerAngles;
            var rotation = Rotation;
            if (FreezeX)
                rotation.x = actual.x;
            if (FreezeY)
                rotation.y = actual.y;
            if (FreezeZ)
                rotation.z = actual.z;
            
            transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
