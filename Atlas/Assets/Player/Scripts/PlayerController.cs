using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputManagement;
using Atlas_Physics;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BodyController))]
    [RequireComponent(typeof(AtlasGravity))]
    public class PlayerController : MonoBehaviour
    {
        #region public variables
        [Header("Movement")]
        public float _WalkSpeed = 5f;
        [Range(1f, 3f)]
        [Tooltip("Multiply the Movement speed by this scale to obtain the sprint speed")]
        public float _SprintScale = 1.3f;
        [Range(0f, 1f)]
        public float _CrouchScale = .4f;
        [Range(0f, 1f)]
        public float _ProneScale = 0f;
        public float _Acceleration = 100f;
        public float _Deceleration = 100f;
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
            set { _Animator.SetBool(_HashSprinting, value); }
        }
        public bool IsCrouched
        {
            get { return _Animator.GetBool(_HashCrouched); }
            set {
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
        private Rigidbody _Body;
        private Animator _Animator;
        private BodyController _BodyController;
        private GameObject _GroundChecker;
        private AtlasGravity _Gravity;
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

        #region Initialisation
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
        }

        private void Start()
        {
            StateMachine.State<PlayerController>.Initialise(_Animator, this);
        }
        #endregion

        /// <summary>
        /// Transform the player input so the correct animation is played relative to the player rotation
        /// </summary>
        private void TransformInputRelativelyToMouse()
        {
            Vector3 mousePos = _Camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 localPos = transform.InverseTransformPoint(mousePos).normalized;
            localPos.y = 0;
            // Get the angle we should rotate the input. This angle is equal to zero wen the player is facing top
            float refAngle = Vector3.SignedAngle(Vector3.back, localPos, Vector3.up);
            //Multiply the input vector by the refAngle 
            Vector3 newInput = Quaternion.Euler(0, refAngle, 0) * _Input;
            _Animator.SetFloat(_HashHorizontalSpeed, newInput.x);
            _Animator.SetFloat(_HashVerticalSpeed, newInput.z);
        }

        private void FixedUpdate()
        {
            //if (useEightDirectionMovement)
            //    TransformInputRelativelyToMouse();
            //else
            //{
            _Animator.SetFloat(_HashHorizontalSpeed, _Input.x);
            _Animator.SetFloat(_HashVerticalSpeed, _Input.z);
            //}
            if (_Input.normalized != _CurrentDirection && CheckForIdle() == false)
            {
                _CurrentDirection = _Input.normalized;
                transform.LookAt(new Vector3(_CurrentDirection.x * 180, _CurrentDirection.y, _CurrentDirection.z * 180));
            }
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
            float desiredSpeedH = useInput ? input.x * _WalkSpeed * speedScale : 0f;
            float desiredSpeedV = useInput ? input.z * _WalkSpeed * speedScale : 0f;
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
        /// Rotate with controller right joystick
        /// </summary>
        private void RJoystickAim()
        {
            Vector3 direction = Vector3.right * cInput.GetAxisRaw(InputManager.R_AXIS_HORIZONTAL) + Vector3.forward * cInput.GetAxisRaw(InputManager.R_AXIS_VERTICAL);
            if (direction.sqrMagnitude > 0f)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }

        /// <summary>
        /// Choose the way the layer will rotate according to controls
        /// </summary>
        public void RotateAim()
        {
            if (!InputManager.IsJoystickConnected())
                MouseAim();
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