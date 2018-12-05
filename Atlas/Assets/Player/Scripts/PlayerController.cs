using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputManagement;
using Atlas_Physics;
using Game.Inventory;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BodyController))]
    [RequireComponent(typeof(AtlasGravity))]
    public class PlayerController : MonoBehaviour
    {
        #region public variables
        [Header("Movement")]
        public float _BaseSpeed = 5f;
        [Range(1f, 3f)]
        [Tooltip("Multiply the Movement speed by this scale to obtain the sprint speed")]
        public float _SprintScale = 1.3f;
        [Range(0f, 1f)]
        public float _CrouchScale = .4f;
        [Range(0f, 1f)]
        public float _ProneScale = 0f;
        public float _Acceleration = 100f;
        public float _Deceleration = 100f;
        public float _RotationSpeed = 150f;
        [Header("Jump")]
        [Range(0f, 4f)]
        public float _JumpHeight = 0f;
        [Tooltip("Every object with those layers will be used as a ground for various movement computations")]
        public LayerMask _GroundLayers;
        [Tooltip("Define at which distance from the ground the player is consisered \"Grounded\"")]
        public float _GroundDistance = 2f;
        public Vector3 _GroundCheckerPosition;
        #endregion

        #region accessible properties
        public Rigidbody Body
        {
            get { return _Body; }
        }
        public bool IsGrounded
        {
            get { return _Animator.GetBool(_HashGrounded); }
            set {
                _Animator.SetBool(_HashGrounded, value);
                _Gravity.SetScale(value ? 1f : 10f);
            }
        }
        public bool IsSprinting
        {
            get { return _Animator.GetBool(_HashSprinting);  }
            set {
                _Animator.SetBool(_HashSprinting, value);
                _CurrentSpeed = value && !(IsCrouched || IsProned) ? _BaseSpeed * _SprintScale : _CurrentSpeed;
                _CurrentSpeed = !value && !(IsCrouched || IsProned) ? _BaseSpeed : _CurrentSpeed;
                _CurrentAcceleratedSpeed = value && !(IsCrouched || IsProned) ? _CurrentAcceleratedSpeed * _SprintScale : _CurrentAcceleratedSpeed;
                _CurrentAcceleratedSpeed = !value && !(IsCrouched || IsProned) ? _CurrentAcceleratedSpeed * .5f : _CurrentAcceleratedSpeed;
            }
        }
        public bool IsCrouched
        {
            get { return _Animator.GetBool(_HashCrouched); }
            set
            {
                _Animator.SetBool(_HashCrouched, value);
                if (value && IsProned)
                {
                    IsProned = false;
                }
            }
        }
        public bool IsProned
        {
            get { return _Animator.GetBool(_HashProned); }
            set
            {
                _Animator.SetBool(_HashProned, value);
                if (value && IsCrouched)
                {
                    IsCrouched = false;
                }
            }
        }
        #endregion

        #region private variables
        private Vector3 _Input;
        private Vector3 _Move;
        private Vector3 _CurrentDirection;
        private Camera _Camera;
        private float _CurrentSpeed;
        private float _CurrentAcceleratedSpeed;
        private Rigidbody _Body;
        private Animator _Animator;
        private BodyController _BodyController;
        private GameObject _GroundChecker;
        private AtlasGravity _Gravity;
        private float _PickRange;
        #endregion

        #region animator variables hashes
        private readonly int _HashIdle = Animator.StringToHash("Idle");
        private readonly int _HashJumpSpeed = Animator.StringToHash("JumpSpeed");
        private readonly int _HashHorizontalSpeed = Animator.StringToHash("HorizontalSpeed");
        private readonly int _HashVerticalSpeed = Animator.StringToHash("VerticalSpeed");
        private readonly int _HashGrounded = Animator.StringToHash("Grounded");
        private readonly int _HashSprinting = Animator.StringToHash("Sprinting");
        private readonly int _HashCrouched = Animator.StringToHash("Crouched");
        private readonly int _HashProned = Animator.StringToHash("Proned");
        #endregion

        #region Initialization
        // Use this for initialization
        private void Awake()
        {
            _Camera = Camera.main;
            _Body = GetComponent<Rigidbody>();
            _Animator = GetComponent<Animator>();
            _BodyController = GetComponent<BodyController>();
            _GroundChecker = new GameObject("GroundChecker");
            _GroundChecker.transform.SetParent(transform);
            _GroundChecker.transform.localPosition = _GroundCheckerPosition;
            _Gravity = GetComponent<AtlasGravity>();
            _CurrentSpeed = _BaseSpeed;
            _CurrentAcceleratedSpeed = 0f;
            _PickRange = 10f;
    }

    private void Start()
        {
            StateMachine.State<PlayerController>.Initialise(_Animator, this);
        }
        #endregion

        /// <summary>
        /// Transform the player input so the correct animation is played relative to the player rotation
        /// </summary>
        private void TransformInputRelativelyToCamera()
        {
            Vector3 CameraPos = _Camera.transform.position;
            Vector3 localPos = transform.TransformPoint(CameraPos).normalized;
            localPos.y = 0;
            // Get the angle we should rotate the input. This angle is equal to zero wen the player is facing top
            float refAngle = Vector3.SignedAngle(Vector3.back, localPos, Vector3.up);
            //Multiply the input vector by the refAngle 
            Vector3 newInput = Quaternion.Euler(0, refAngle, 0) * _Input;
            _Animator.SetFloat(_HashHorizontalSpeed, newInput.x);
            _Animator.SetFloat(_HashVerticalSpeed, newInput.z);
        }

        private void Update()
        {
            if (cInput.GetKey(InputManager.CAMERA_LOCK))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        private void UpdateScaledInputs()
        {
            if (_Input.z > 0 && _CurrentAcceleratedSpeed < _CurrentSpeed)
            {
                _CurrentAcceleratedSpeed += _Acceleration * Time.fixedDeltaTime;
            }
            else if (_Input.z < 0 && -_CurrentAcceleratedSpeed > -_CurrentSpeed)
            {
                _CurrentAcceleratedSpeed += _Acceleration * Time.fixedDeltaTime;
            }
            else if (_Input.z == 0)
            {
                if (_CurrentAcceleratedSpeed > _Deceleration * Time.fixedDeltaTime)
                {
                    _CurrentAcceleratedSpeed -= _Deceleration * Time.fixedDeltaTime;
                }
                else if (_CurrentAcceleratedSpeed < -_Deceleration * Time.fixedDeltaTime)
                {
                    _CurrentAcceleratedSpeed += _Deceleration + Time.fixedDeltaTime;
                }
                else 
                {
                    _CurrentAcceleratedSpeed = 0;
                }
            }
            _Animator.SetFloat(_HashVerticalSpeed, _CurrentAcceleratedSpeed / _CurrentSpeed);
        }

        private void FixedUpdate()
        {
            UpdateScaledInputs();
            _Animator.SetFloat(_HashHorizontalSpeed, _Input.x);
            _Move = new Vector3((cInput.GetKey(InputManager.CAMERA_LOCK) ? cInput.GetAxis(InputManager.AXIS_HORIZONTAL): 0), 0, cInput.GetAxis(InputManager.AXIS_VERTICAL));

            if (cInput.GetKey(InputManager.CAMERA_LOCK) &&
                (cInput.GetAxis(InputManager.AXIS_HORIZONTAL) > 0f || 
                cInput.GetAxis(InputManager.AXIS_VERTICAL) > 0f))
            {
                _Move *= 0.7f;
            }

            _Move = transform.TransformDirection(_Move) * _CurrentAcceleratedSpeed;
            _BodyController.Move(_Move * Time.fixedDeltaTime);
        }

        /// <summary>
        /// Check if the player is on the ground
        /// </summary>
        public void CheckForGrounded()
        {
            IsGrounded = Physics.CheckSphere(_GroundChecker.transform.position, _GroundDistance, _GroundLayers);
        }

        /// <summary>
        /// Check if the player is idle
        /// </summary>
        /// <returns></returns>
        public bool CheckForIdle()
        {
            return _Input.x == 0 && _Input.z == 0;
        }

        public void GoToIdleState(bool state)
        {
            if (state)
            {
                _Animator.SetTrigger(_HashIdle);
            }
            else
            {
                _Animator.ResetTrigger(_HashIdle);
            }
        }

        /// <summary>
        /// input based on Horizontal(q, d, <, >) and Vertical(z, s, ^, v) keys
        /// </summary>
        public void GetInput()
        {
            _Input.x = cInput.GetAxisRaw(InputManager.AXIS_HORIZONTAL);
            _Input.y = 0;
            _Input.z = cInput.GetAxisRaw(InputManager.AXIS_VERTICAL);
        }

        public void SetSpeedScale(float Scale)
        {
            _CurrentSpeed = _BaseSpeed * Scale;
            _CurrentAcceleratedSpeed = _CurrentAcceleratedSpeed * 1f / Scale;
        }

        public void Walk()
        {
            GroundedHorizontalMovement(true, IsSprinting ? _SprintScale : 1f);
        }

        public void Crouch()
        {
            GroundedHorizontalMovement(true, _CrouchScale);
        }

        public void Prone()
        {
            GroundedHorizontalMovement(true, _ProneScale);
        }

        private void GroundedHorizontalMovement(bool useInput, float speedScale = 1f)
        {
            Vector3 input = _Input.normalized;
            float desiredSpeedH = useInput ? input.x * _BaseSpeed * speedScale : 0f;
            float desiredSpeedV = useInput ? input.z * _BaseSpeed * speedScale : 0f;
            float accelerationH = useInput && input.x != 0 ? _Acceleration : _Deceleration;
            float accelerationV = useInput && input.z != 0 ? _Acceleration : _Deceleration;
            _Move.x = Mathf.MoveTowards(_Move.x, desiredSpeedH, accelerationH * Time.deltaTime);
            _Move.z = Mathf.MoveTowards(_Move.z, desiredSpeedV, accelerationV * Time.deltaTime);
        }

        // Public functions - called mostly by StateMachineBehaviours in the character's Animator Controller but also by Events.
        public void SetMoveVector(Vector3 newMoveVector)
        {
            _Move = newMoveVector;
        }

        public void SetHorizontalMovement(float newHorizontalMovement)
        {
            _Move.x = newHorizontalMovement;
        }

        public void SetVerticalMovement(float newVerticalMovement)
        {
            _Move.z = newVerticalMovement;
        }

        public void IncrementMovement(Vector3 additionalMovement)
        {
            _Move += additionalMovement;
        }

        public void IncrementHorizontalMovement(float additionalHorizontalMovement)
        {
            _Move.x += additionalHorizontalMovement;
        }

        public void IncrementVerticalMovement(float additionalVerticalMovement)
        {
            _Move.z += additionalVerticalMovement;
        }

        /// <summary>
        /// Check for jump input 
        /// </summary>
        /// <returns></returns>
        public bool CheckForJumpInput()
        { 
            return IsGrounded && cInput.GetButton(InputManager.JUMP);
        }

        /// <summary>
        /// The player jump
        /// </summary>
        public void Jump()
        {
            _Body.AddForce(Vector3.up * Mathf.Sqrt(_JumpHeight * -2f * Physics.gravity.y), ForceMode.Impulse);
            _Animator.SetFloat(_HashJumpSpeed, Body.velocity.y);
        }

        /// <summary>
        /// Check for Sprint input 
        /// </summary>
        /// <returns></returns>
        public bool CheckForSprintInput()
        {
            if (cInput.GetKeyDown(InputManager.SPRINT))
            {
                IsSprinting = true;
            }
            if (cInput.GetKeyUp(InputManager.SPRINT))
            {
                IsSprinting = false;
            }
            return IsGrounded && IsSprinting;
        }

        public bool CheckForCrouchedInput()
        {
            return IsGrounded && cInput.GetKeyDown(InputManager.CROUCH);
        }

        public bool CheckForPronedInput()
        {
            return IsGrounded && cInput.GetKeyDown(InputManager.PRONE);
        }

        public bool CheckForPickInput()
        {
            return IsGrounded && cInput.GetKeyDown(InputManager.PICK);
        }

        public void Pick()
        {
            Ray ray = _Camera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            Debug.Log("Pick pick");

            if (Physics.Raycast(ray, out hit, _PickRange))
            {
                if (hit.collider.gameObject.GetComponent<ItemStackBehaviour>() != null)
                {
                    Debug.Log("Pick something");
                }
            }
        }

        public void ToggleCrouchedState()
        {
            IsCrouched = !IsCrouched;
        }

        public void TogglePronedState()
        {
            IsProned = !IsProned;
        }

        /// <summary>
        /// Rotate player with mouse
        /// </summary>
        private void MouseAim()
        {
            Ray cameraRay = _Camera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 lookAtPoint = cameraRay.GetPoint(rayLength);
                Debug.DrawLine(cameraRay.origin, lookAtPoint, Color.blue);

                transform.LookAt(new Vector3(lookAtPoint.x, transform.position.y, lookAtPoint.z));
            }
        }

        /// <summary>
        /// Rotate player with mouse
        /// </summary>
        private void CameraAim()
        {
            if (cInput.GetKey(InputManager.CAMERA_LOCK))
            {
                transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
            }
            else
            {
                transform.Rotate(0, cInput.GetAxis(InputManager.AXIS_HORIZONTAL) * _RotationSpeed * Time.deltaTime, 0);
            }
        }

        /// <summary>
        /// Rotate with controller right joystick
        /// </summary>
        private void RJoystickAim()
        {
            //Vector3 direction = Vector3.right * cInput.GetAxisRaw(InputManager.R_AXIS_HORIZONTAL) + Vector3.forward * cInput.GetAxisRaw(InputManager.R_AXIS_VERTICAL);
            //if (direction.sqrMagnitude > 0f)
            //{
            //    transform.rotation = Quaternion.LookRotation(direction);
            //}
        }

        /// <summary>
        /// Choose the way the layer will rotate according to controls
        /// </summary>
        public void RotateAim()
        {
            if (!InputManager.IsJoystickConnected())
                CameraAim();
            else
                RJoystickAim();
        }

        /// <summary>
        /// Rotate the player in the choosen direction
        /// </summary>
        /// <param name="aimDirection"></param>
        public void RotateAim(Vector3 aimDirection)
        {
            transform.rotation = Quaternion.LookRotation(aimDirection);
        }
    }
}