using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputManagement;

namespace Player
{
    public class TPCamera : MonoBehaviour
    {
        [Tooltip("The target that the camera will be controlling")]
        public GameObject _Target;

        [Header("Camera Inputs")]
        public InputAxis _Horizontal;
        public InputAxis _Vertical;
        public InputAxis _CameraZoom;
        public InputKey _CameraLock;

        [Header("Range")]
        public float _MaximumRange = 20f;
        public float _MinimunRange = 0.6f;
        public float _DefaultRange = 10f;

        [Header("Speed")]
        [Range(0.6f, 20f)]
        public float _HorizontalSpeed = 200f;
        [Range(0.6f, 20f)]
        public float _VerticalSpeed = 200f;
        public int _ZoomSpeed = 40;
        [Tooltip("Automatic rotation speed")]
        public float _RotationDampening = 5f;
        [Tooltip("Automatic zoom speed")]
        public float _ZoomDampening = 5f;

        [Header("Limites")]
        [Tooltip("At which distance from the wall the camera is staying")]
        public float _WallOffset = 0.1f;
        [Range(-360, 360)]
        public int _MaximumVerticalLimit = 80;
        [Range(-360, 360)]
        public int _MinimumVerticalLimit = -80;
        [Tooltip("The camera will enter in collision with those layers")]
        public LayerMask _CollisionLayers;
        public bool _LockCamera = false;
        public bool _AllowHorizontalMouseInput = true;
        public bool _AllowVerticalMouseInput = true;

        private Vector3 _Offset;
        private Vector2 _Angles;
        private float _CurrentRange;
        private float _DesiredRange;
        private float _CorrectedRange;
        private bool _RotateBehind = false;


        // Use this for initialization
        void Start()
        {
            Rigidbody rigidbody = _Target.GetComponent<Rigidbody>();
            Vector3 angles = transform.eulerAngles;
            _Angles.Set(angles.x, angles.y);
            _CorrectedRange = _DefaultRange;
            _DesiredRange = _DefaultRange;
            _CorrectedRange = _DefaultRange;

            if (rigidbody)
            {
                rigidbody.freezeRotation = true;
            }

            if (_LockCamera)
            {
                _RotateBehind = true;
            }
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            if (!_Target)
            {
                return;
            }

            // Right Mouse button being held
            if (_CameraLock.Get())
            {
                if (_AllowHorizontalMouseInput) {
                    _Angles.x += _Horizontal.Get() * _HorizontalSpeed * 0.02f;
                }
                else
                {
                    ResetCameraPosition();
                }

                if (_AllowVerticalMouseInput)
                {
                    _Angles.y -= _Vertical.Get() * _VerticalSpeed * 0.02f;
                }

                // Interrupt the automatic rotation
                if (!_LockCamera)
                {
                    _RotateBehind = false;
                }
            }
            else if (_Horizontal.Get() != 0 || _Vertical.Get() != 0 || _RotateBehind)
            {
                ResetCameraPosition();
            }

            _Angles.y = ClampAngle(_Angles.y, _MinimumVerticalLimit, _MaximumVerticalLimit);
            
            // Camera rotation
            Quaternion rotation = Quaternion.Euler(_Angles.y, _Angles.x, 0f);

            // Desired Range
            _DesiredRange -= _CameraZoom.Get() * Time.deltaTime * _ZoomSpeed * Mathf.Abs(_DesiredRange);
            _DesiredRange = Mathf.Clamp(_DesiredRange, _MinimunRange, _MaximumRange);
            _CorrectedRange = _DesiredRange;

            // Desired Camera position
            float height = 2f;
            Vector3 offset = new Vector3(0, -height, 0);
            Vector3 position = _Target.transform.position - (rotation * Vector3.forward * _DesiredRange + offset);

            // Check for collision and correct the camera
            RaycastHit collisionHit;
            Vector3 trueTargetPosition = new Vector3(_Target.transform.position.x, _Target.transform.position.y + height, _Target.transform.position.z);

            bool isCorrected = false;
            if (Physics.Linecast(trueTargetPosition, position, out collisionHit, _CollisionLayers))
            {
                _CorrectedRange = Vector3.Distance(trueTargetPosition, collisionHit.point) - _WallOffset;
                isCorrected = true;
            }

            // Smoothing camera movement, lerp distance only if either Range wasn't corrected, or _CorrectRange is more than _CurrentRange
            _CurrentRange = !isCorrected || _CorrectedRange > _CurrentRange ? Mathf.Lerp(_CurrentRange, _CorrectedRange, Time.deltaTime * _ZoomDampening) : _CorrectedRange;

            // Applying limits
            _CurrentRange = Mathf.Clamp(_CurrentRange, _MinimunRange, _MaximumRange);

            // Recalcultating positions
            position = _Target.transform.position - (rotation * Vector3.forward * _CurrentRange + offset);

            // Set final Rotation & Position
            transform.position = position;
            transform.rotation = rotation;
        }

        private void ResetCameraPosition()
        {
            float targetRotationAngle = _Target.transform.eulerAngles.y;
            float currentRotationAngle = transform.eulerAngles.y;

            _Angles.x = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, _RotationDampening * Time.deltaTime);

            // Stop the rotation
            if (targetRotationAngle == currentRotationAngle)
            {
                if (!_LockCamera)
                {
                    _RotateBehind = false;
                }
            }
            else
            {
                _RotateBehind = true;
            }
        }

        private float ClampAngle(float angle, float min, float max)
        {
            return Mathf.Clamp(angle < -360 ? angle += 360 : angle > 360 ? angle -= 360 : angle, min, max);
        }
    }
}