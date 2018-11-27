using UnityEngine;

namespace Atlas_Physics
{
    [RequireComponent(typeof(Rigidbody))]
    public class AtlasGravity : MonoBehaviour
    {
        private float _GravityScale = 1.0f;

        public float GravityScale {
            get { return _GravityScale; }
            set { }
        }

        // Global Gravity doesn't appear in the inspector. Modify it here in the code
        // (or via scripting) to define a different default gravity for all objects.
        public static float GLOBAL_GRAVITY = -9.81f;

        private Rigidbody _Body;

        public void SetScale(float scale)
        {
            _GravityScale = scale;
        }

        public void IncrementScale(float percent)
        {
            _GravityScale *= percent;
        }

        void OnEnable()
        {
            _Body = GetComponent<Rigidbody>();
            _Body.useGravity = false;
        }

        void FixedUpdate()
        {
            Vector3 gravity = GLOBAL_GRAVITY * _GravityScale * Vector3.up;
            _Body.AddForce(gravity, ForceMode.Acceleration);
        }
    }

}
