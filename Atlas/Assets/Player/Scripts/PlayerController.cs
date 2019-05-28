using System;
using UnityEngine;
using InputManagement;
using Game;
using Game.Item.PlantSeed;
using Game.Player;
using Game.Inventory;
using Player.Scripts;
using System.Collections;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        #region Variables

        [SerializeField] private HandSlots _handSlots;
        #region Public variables

        #region Inputs
        public PlayerInputs _Inputs;
        #endregion

        #region Movement configuration
        [Header("Movement")]
        public float baseSpeed = 10f;
        //[Range(1f, 3f)]
        //[Tooltip("Multiply the Movement speed by this scale to obtain the sprint speed")]
        public float desiredRotationSpeed = 0.1f;
        public float allowPlayerRotation = 0.1f;
        public bool blockRotationPlayer;

        [Header("Jump")]
        public float gravity = -30f;
        public float jumpHeight = 10f;

        [Header("Animation Movement Smoothing")]
        [Range(0, 1f)]
        public float horizontalAnimSmoothTime = 0.2f;
        [Range(0, 1f)]
        public float verticalAnimTime = 0.2f;
        [Range(0, 1f)]
        public float startAnimTime = 0.3f;
        [Range(0, 1f)]
        public float stopAnimTime = 0.15f;

        [Header("Interaction")]
        public float interactDistance = 0.5f;
        public LayerMask interactLayerMask;
        #endregion

        #region Ground checking
        [Header("GroundCheck")]
        [Tooltip("Every object with those layers will be used as a ground for various movement computations")]
        public LayerMask groundLayers;
        [Tooltip("Define at which distance from the ground the player is consisered \"Grounded\"")]
        public float groundDistance = 2f;
        #endregion

        #endregion

        #region FloatVariables
        //[Header("Shared States")]
        //[SerializeField]
        //private FloatVariable _CurrentAcceleratedSpeed;
        #endregion

        #region Sow Variables
        public Vector3 plantPosition = Vector3.zero;
        private bool _canSow = false;
        private float decay = 0f;
        #endregion

        #region accessible properties
        public bool IsGrounded
        {
            get { return m_Animator.GetBool(_HashGrounded); }
            set { m_Animator.SetBool(_HashGrounded, value); }
        }

        public bool IsSprinting
        {
            get { return m_Animator.GetBool(_HashSprinting); }
            set
            {
                m_Animator.SetBool(_HashSprinting, value);
                //_CurrentSpeed = value ? _BaseSpeed * _SprintScale : _CurrentSpeed;
                //_CurrentSpeed = !value ? _BaseSpeed : _CurrentSpeed;
                //_CurrentAcceleratedSpeed.Value = value ? _CurrentAcceleratedSpeed.Value * _SprintScale : _CurrentAcceleratedSpeed.Value;
                //_CurrentAcceleratedSpeed.Value = !value ? _CurrentAcceleratedSpeed.Value * .5f : _CurrentAcceleratedSpeed.Value;
            }
        }

        public bool IsInteracting { get => InteractValue > 0; }

        public int InteractValue
        {
            get { return m_Animator.GetInteger(_HashInteract); }
            set { m_Animator.SetInteger(_HashInteract, value); }
        }

        public bool IsDead
        {
            get { return m_Animator.GetBool(_HashDead); }
            set { m_Animator.SetBool(_HashDead, value); }
        }

        public bool IsPicking
        {
            get { return m_Animator.GetBool(_HashPicking); }
            set{ m_Animator.SetBool(_HashPicking, value); }
        }

        private bool _isCheckSowing = false;

        private bool _isSowing = false;
        public bool IsSowing
        {
            get { return _isSowing; }
            set
            {
                _isSowing = value;
                if (_isSowing == true)
                    m_Animator.SetTrigger(_HashSowing);
            }
        }

        private bool _isEquippedSlotUsed;
        public bool IsEquippedSlotUsed
        {
            get { return _isEquippedSlotUsed; }
            set { _isEquippedSlotUsed = value; }
        }

        #endregion

        #region private variables
        private Camera m_Camera;
        private Animator m_Animator;
        private GameObject m_GroundChecker;
        private PlayerStats m_PlayerStats;
        private float m_InputX;
        private float m_InputZ;
        private float m_InputMagnitude;
        private Vector3 m_DesiredMoveDirection;
        private Vector3 m_MoveVector;
        private float m_VerticalVelocity;
        private InputControls m_InputControls;
        private CharacterController m_CharacterController;
        #endregion

        #region animator variables hashes
        private readonly int _HashIdle = Animator.StringToHash("Idle");
        private readonly int _HashJump = Animator.StringToHash("Jump");
        private readonly int _HashHorizontalSpeed = Animator.StringToHash("HorizontalSpeed");
        private readonly int _HashVerticalSpeed = Animator.StringToHash("VerticalSpeed");
        private readonly int _HashGrounded = Animator.StringToHash("Grounded");
        private readonly int _HashSprinting = Animator.StringToHash("Sprinting");
        private readonly int _HashPicking = Animator.StringToHash("Picking");
        private readonly int _HashSowing = Animator.StringToHash("Sowing");
        private readonly int _HashDead = Animator.StringToHash("Dead");
        private readonly int _HashInputX = Animator.StringToHash("InputX");
        private readonly int _HashInputZ = Animator.StringToHash("InputZ");
        private readonly int _HashInputMagnitude = Animator.StringToHash("InputMagnitude");
        private readonly int _HashInteract = Animator.StringToHash("Interact");

        #endregion

        #endregion


        #region Initialization
        private void Awake()
        {
            m_Camera = Camera.main;
            m_Animator = GetComponent<Animator>();
            m_GroundChecker = transform.Find("GroundChecker").gameObject;
            m_CharacterController = GetComponent<CharacterController>();
            m_PlayerStats = GetComponentInChildren<PlayerStats>();
            StateMachine.State<PlayerController>.Initialise(m_Animator, this);
        }

        private void OnEnable()
        {
            GameControl.Instance.InputControls.Player.Movement.performed += ctx => GetMovementIput(ctx.ReadValue<Vector2>());
            GameControl.Instance.InputControls.Player.Movement.canceled += ctx => ResetMovementInput(ctx.ReadValue<Vector2>());
            GameControl.Instance.InputControls.Player.Movement.Enable();
            GameControl.Instance.InputControls.Player.Jump.performed += ctx => Jump();
            GameControl.Instance.InputControls.Player.Jump.Enable();
            GameControl.Instance.InputControls.Player.Interact.performed += ctx => Interact();
            GameControl.Instance.InputControls.Player.Interact.Enable();
        }

        private void OnDisable()
        {
            GameControl.Instance.InputControls.Player.Movement.performed -= ctx => GetMovementIput(ctx.ReadValue<Vector2>());
            GameControl.Instance.InputControls.Player.Movement.canceled -= ctx => ResetMovementInput(ctx.ReadValue<Vector2>());
            GameControl.Instance.InputControls.Player.Movement.Disable();
            GameControl.Instance.InputControls.Player.Jump.performed -= ctx => Jump();
            GameControl.Instance.InputControls.Player.Jump.Disable();
            GameControl.Instance.InputControls.Player.Interact.performed -= ctx => Interact();
            GameControl.Instance.InputControls.Player.Interact.Disable();
        }
        #endregion

        public void GetMovementIput(Vector2 input)
        {
            m_InputX = input.x;
            m_InputZ = input.y;
        }

        private void ResetMovementInput(Vector2 vector2)
        {
            m_InputX = 0;
            m_InputZ = 0;
        }

        void GetInputMagnitude()
        {
            m_Animator.SetFloat(_HashInputX, m_InputX, horizontalAnimSmoothTime, Time.deltaTime * 2f);
            m_Animator.SetFloat(_HashInputZ, m_InputZ, verticalAnimTime, Time.deltaTime * 2f);
            m_InputMagnitude = new Vector2(m_InputX, m_InputZ).sqrMagnitude;
            if (m_InputMagnitude > allowPlayerRotation)
            {
                m_Animator.SetFloat(_HashInputMagnitude, m_InputMagnitude, startAnimTime, Time.deltaTime);
                RotatePlayerAndGetMoveDirection();
            }
            else if (m_InputMagnitude < allowPlayerRotation)
            {
                m_Animator.SetFloat(_HashInputMagnitude, m_InputMagnitude, stopAnimTime, Time.deltaTime);
            }
        }

        void Update()
        {
            //CheckForGrounded();
            IsGrounded = m_CharacterController.isGrounded;
            
            if (m_CharacterController.isGrounded
                && !IsInteracting)
            {
                GetInputMagnitude();
            }
            
            m_MoveVector = m_DesiredMoveDirection * baseSpeed * m_InputMagnitude;
            m_VerticalVelocity -= gravity * Time.deltaTime;
            m_MoveVector.y = m_VerticalVelocity;
            m_CharacterController.Move(m_MoveVector * Time.deltaTime);
        }

        void RotatePlayerAndGetMoveDirection()
        {
            var forward = m_Camera.transform.forward;
            var right = m_Camera.transform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            m_DesiredMoveDirection = forward * m_InputZ + right * m_InputX;
            m_MoveVector = m_DesiredMoveDirection;
            if (blockRotationPlayer == false)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(m_DesiredMoveDirection), desiredRotationSpeed);
            }
        }

        /// <summary>
        /// Transform the player input so the correct animation is played relative to the player rotation
        /// </summary>
        //private void TransformInputRelativelyToCamera()
        //{
        //    Vector3 CameraPos = _Camera.transform.position;
        //    Vector3 localPos = transform.TransformPoint(CameraPos).normalized;
        //    localPos.y = 0;
        //    // Get the angle we should rotate the input. This angle is equal to zero wen the player is facing top
        //    float refAngle = Vector3.SignedAngle(Vector3.back, localPos, Vector3.up);
        //    //Multiply the input vector by the refAngle 
        //    Vector3 newInput = Quaternion.Euler(0, refAngle, 0) * m_Input;
        //    _Animator.SetFloat(_HashHorizontalSpeed, newInput.x);
        //    _Animator.SetFloat(_HashVerticalSpeed, newInput.z);
        //}

        public void PlayAnimation(InputKeyStatus status, PlayerAnimationData animationData)
        {
            if (!animationData.Enabled)
                return;

            switch (animationData.Type)
            {
                case PlayerAnimationData.AnimationType.Trigger:
                    if (status == InputKeyStatus.Pressed)
                        m_Animator.SetTrigger(animationData.Hash);
                    break;
                case PlayerAnimationData.AnimationType.Holded:
                    m_Animator.SetBool(animationData.Hash, status == InputKeyStatus.Pressed || status == InputKeyStatus.Holded);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ResetSpeed()
        {
            m_MoveVector = m_DesiredMoveDirection = Vector3.zero;
            m_InputMagnitude = 0;
            m_VerticalVelocity = 0;
            m_Animator.SetFloat(_HashInputMagnitude, m_InputMagnitude, stopAnimTime, Time.deltaTime);
        }

        /// <summary>
        /// Check if the player is on the ground
        /// </summary>
        public void CheckForGrounded()
        {
            IsGrounded = Physics.CheckSphere(m_GroundChecker.transform.position, groundDistance, groundLayers);
        }

        /// <summary>
        /// Check if the player is idle
        /// </summary>
        /// <returns></returns>
        public bool CheckForIdle()
        {
            return m_InputX == 0 && m_InputZ == 0;
        }

        public void GoToIdleState(bool state)
        {
            if (state)
                m_Animator.SetTrigger(_HashIdle);
            else
                m_Animator.ResetTrigger(_HashIdle);
        }


        /// <summary>
        /// The player jump
        /// </summary>
        public void Jump()
        {
            m_VerticalVelocity = jumpHeight;
            m_Animator.SetTrigger(_HashJump);
        }

        /// <summary>
        /// Check for Sprint input 
        /// </summary>
        /// <returns></returns>
        public bool CheckForSprintInput()
        {
            if (_Inputs.Sprint.GetDown())
                IsSprinting = true;
            if (_Inputs.Sprint.GetUp())
                IsSprinting = false;
            return IsGrounded && IsSprinting;
        }

        public InputKeyStatus GetUseInput()
        {
            IsSowing = false;
            return _Inputs.EquippedItemUse.GetStatus();
        }

        public bool CheckToSow()
        {
            if (_isCheckSowing)
            {
                if (_Inputs.Sow.GetDown() && _canSow && decay == 0f)
                {
                    decay = 1f;
                    IsSowing = true;
                }
                if (_Inputs.Sow.GetUp())
                    IsSowing = false;
                if (_Inputs.Skip.GetDown())
                {
                    IsSowing = false;
                    _isCheckSowing = false;
                }
            }
            return IsGrounded && _isCheckSowing && IsSowing;
        }

        private void Interact()
        {
            if (!IsGrounded || IsInteracting)
                return;
            ResetSpeed();
            Vector3 p1 = transform.position + m_CharacterController.center + Vector3.up * -m_CharacterController.height * 0.5F;
            Vector3 p2 = p1 + Vector3.up * m_CharacterController.height;
            // Cast character controller shape forward to see if it is about to hit anything.
            RaycastHit[] hits = Physics.CapsuleCastAll(p1, p2, m_CharacterController.radius, transform.forward, interactDistance, interactLayerMask, QueryTriggerInteraction.Collide);

            foreach (RaycastHit hit in hits)
            {
                Debug.DrawLine(p1, hit.transform.position);
                AInteractable interactable = hit.transform.GetComponent<AInteractable>();
                if (interactable != null)
                    interactable.Interact(this);
            }
        }

        public bool CheckForDeath()
        {
            if (!IsDead && m_PlayerStats._consumer.LinkedStock[Game.ResourcesManagement.Resource.Oxygen].Quantity <= 0)
            {
                IsDead = true;
                _handSlots.Drop();
                var inventory = gameObject.GetComponentInChildren<PlayerInventory>();
                inventory.DropAll();
                ResetSpeed();
                //_CurrentAcceleratedSpeed.Value = 0f;
            }
            else if (IsDead == true && m_PlayerStats._consumer.LinkedStock[Game.ResourcesManagement.Resource.Oxygen].Quantity > 0)
            {
                IsDead = false;
            }
            return IsDead;
        }

        /// <summary>
        /// Rotate player with mouse
        /// </summary>
        private void MouseAim()
        {
            Ray cameraRay = m_Camera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 lookAtPoint = cameraRay.GetPoint(rayLength);
                Debug.DrawLine(cameraRay.origin, lookAtPoint, Color.blue);

                transform.LookAt(new Vector3(lookAtPoint.x, transform.position.y, lookAtPoint.z));
            }
        }

        private void UseItem()
        {
            InputKeyStatus status = GetUseInput();

            if (_handSlots.EquippedItem)
            {
                _handSlots.CheckIfItemUsable();
                if (_handSlots.EquippedItem is Seed)
                {
                    _canSow = _handSlots.ObjectIsUsable;
                    _isCheckSowing = true;
                }
                if (_handSlots.ObjectIsUsable && !m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Sow"))
                    _handSlots.UseItem(status);
                if (!_handSlots.ObjectIsUsable)
                    status = InputKeyStatus.Nothing;
                if (_handSlots.EquippedItem)
                    PlayAnimation(status, _handSlots.EquippedItem.Animation);
            }
            else
                _isCheckSowing = false;
        }
    }
}