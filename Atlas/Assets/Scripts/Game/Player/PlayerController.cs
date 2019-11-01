using UnityEngine;
using Game.Player;
using Game.Inventory;
using Game.Player.Stats;
using Game.SavingSystem;
using Player.Scripts;
using UnityEngine.InputSystem;
using Game.ResourcesManagement;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        #region Variables

        [SerializeField] private HandSlots _handSlots = null;
        #region Public variables
        #region Movement configuration
        [Header("Movement")]
        public float baseSpeed = 10f;
        //[Range(1f, 3f)]
        //[Tooltip("Multiply the Movement speed by this scale to obtain the sprint speed")]
        public float desiredRotationSpeed = 0.1f;
        public float allowPlayerRotation = 0.1f;
        public bool blockRotationPlayer = false;

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

        public int criticalEnergy = 0;
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
        #endregion

        #region accessible properties
        public bool IsGrounded
        {
            get { return m_Animator.GetBool(_HashGrounded); }
            set { m_Animator.SetBool(_HashGrounded, value); }
        }

        public PlayerInventory Inventory => m_Inventory;

        public bool IsSprinting
        {
            get { return m_Animator.GetBool(_HashSprinting); }
            set
            {
                m_Animator.SetBool(_HashSprinting, value);
            }
        }

        public bool IsInteracting => InteractValue != 0;

        public int InteractValue
        {
            get { return m_Animator.GetInteger(_HashInteract); }
            set { m_Animator.SetInteger(_HashInteract, value); }
        }

        public bool IsPicking
        {
            get { return InteractValue == AInteractable.InteractAnim.pick.ToInt(); }
        }

        public bool IsUsingItem => UseItemValue != 0;

        public int UseItemValue
        {
            get { return m_Animator.GetInteger(_HashUseItem); }
            set { m_Animator.SetInteger(_HashUseItem, value); }
        }

        public bool IsSowing
        {
            get { return UseItemValue == PlayerAnimationData.ItemAnim.sow.ToInt(); }
        }

        public bool IsWatering
        {
            get { return UseItemValue == PlayerAnimationData.ItemAnim.useBukect.ToInt(); }
        }

        public bool IsDead
        {
            get { return m_Animator.GetBool(_HashDead); }
            set { m_Animator.SetBool(_HashDead, value); }
        }

        private bool _isEquippedSlotUsed;
        public bool IsEquippedSlotUsed
        {
            get { return _isEquippedSlotUsed; }
            set { _isEquippedSlotUsed = value; }
        }

        public Camera Camera => m_Camera;

        public DeathType _deathType;
        
        #endregion

        #region private variables

        private PlayerInventory m_Inventory;
        private Camera m_Camera;
        private Animator m_Animator;
        private GameObject m_GroundChecker;
        private PlayerStats m_PlayerStats;
        public PlayerStats GetPlayerStats() { return m_PlayerStats; }
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
        private readonly int _HashJump = Animator.StringToHash("Jump");
        private readonly int _HashGrounded = Animator.StringToHash("Grounded");
        private readonly int _HashSprinting = Animator.StringToHash("Sprinting");
        private readonly int _HashDead = Animator.StringToHash("Dead");
        private readonly int _HashInputX = Animator.StringToHash("InputX");
        private readonly int _HashInputZ = Animator.StringToHash("InputZ");
        private readonly int _HashInputMagnitude = Animator.StringToHash("InputMagnitude");
        private readonly int _HashInteract = Animator.StringToHash("Interact");
        private readonly int _HashUseItem = Animator.StringToHash("UseItem");
        #endregion

        #endregion

        #region Initialization
        private void Awake()
        {
            m_Inventory = GetComponentInChildren<PlayerInventory>();
            m_Camera = Camera.main;
            m_Animator = GetComponent<Animator>();
            m_GroundChecker = transform.Find("GroundChecker").gameObject;
            m_CharacterController = GetComponent<CharacterController>();
            m_PlayerStats = GetComponentInChildren<PlayerStats>();
            StateMachine.State<PlayerController>.Initialise(m_Animator, this);
        }

        private void OnEnable()
        {
            SaveManager.Instance.InputControls.Player.Movement.performed += GetMovementIput;
            SaveManager.Instance.InputControls.Player.Movement.canceled += ResetMovementInput;
            SaveManager.Instance.InputControls.Player.Jump.performed += Jump;
            SaveManager.Instance.InputControls.Player.Interact.performed += Interact;
            SaveManager.Instance.InputControls.Player.UseItem.performed += UseItem;
            SaveManager.Instance.InputControls.Player.UseItem.canceled += CancelUseItem;
            
            SaveManager.Instance.InputControls.Player.Movement.Enable();
            SaveManager.Instance.InputControls.Player.Jump.Enable();
            SaveManager.Instance.InputControls.Player.Interact.Enable();
            SaveManager.Instance.InputControls.Player.UseItem.Enable();
        }

        private void OnDisable()
        {
            SaveManager.Instance.InputControls.Player.Movement.performed -= GetMovementIput;
            SaveManager.Instance.InputControls.Player.Movement.canceled -= ResetMovementInput;
            SaveManager.Instance.InputControls.Player.Interact.performed -= Interact;
            SaveManager.Instance.InputControls.Player.UseItem.performed -= UseItem;
            SaveManager.Instance.InputControls.Player.UseItem.canceled -= CancelUseItem;
            SaveManager.Instance.InputControls.Player.Jump.performed -= Jump;
            
            SaveManager.Instance.InputControls.Player.Movement.Disable();
            SaveManager.Instance.InputControls.Player.Jump.Disable();
            SaveManager.Instance.InputControls.Player.Interact.Disable();
            SaveManager.Instance.InputControls.Player.UseItem.Disable();
        }
        #endregion

        public void GetMovementIput(InputAction.CallbackContext ctx)
        {
            Vector2 input = ctx.ReadValue<Vector2>();
            m_InputX = input.x;
            m_InputZ = input.y;
        }

        private void ResetMovementInput(InputAction.CallbackContext ctx)
        {
            m_InputX = 0;
            m_InputZ = 0;
        }

        void GetInputMagnitude()
        {
            var InputX = m_InputX;
            var InputZ = m_InputZ;
            if (m_PlayerStats.Resources[Resource.Energy].Quantity <= criticalEnergy)
            {
                InputX /= 2f;
                InputZ /= 2f;
            }
            m_Animator.SetFloat(_HashInputX, InputX, horizontalAnimSmoothTime, Time.deltaTime * 2f);
            m_Animator.SetFloat(_HashInputZ, InputZ, verticalAnimTime, Time.deltaTime * 2f);

            m_InputMagnitude = new Vector2(InputX, InputZ).sqrMagnitude;
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
            if (CheckForDeath())
                return;
            //CheckForGrounded();
            IsGrounded = m_CharacterController.isGrounded;

            if (m_CharacterController.isGrounded &&     
                !IsInteracting && !IsUsingItem)
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
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(m_DesiredMoveDirection), desiredRotationSpeed);
            }
        }

        private void ResetSpeed()
        {
            m_MoveVector = m_DesiredMoveDirection = Vector3.zero;
            m_InputMagnitude = 0;
            m_VerticalVelocity = 0;
            m_Animator.SetFloat(_HashInputMagnitude, m_InputMagnitude);
        }

        /// <summary>
        /// Check if the player is on the ground
        /// </summary>
        public void CheckForGrounded()
        {
            IsGrounded = Physics.CheckSphere(m_GroundChecker.transform.position, groundDistance, groundLayers);
        }

        /// <summary>
        /// The player jump
        /// </summary>
        public void Jump(InputAction.CallbackContext ctx)
        {
            if (IsGrounded && !IsDead)
            {
                m_VerticalVelocity = jumpHeight;
                m_Animator.SetTrigger(_HashJump);
            }
        }

        private void Interact(InputAction.CallbackContext ctx)
        {
            if (!IsGrounded || IsInteracting || IsDead)
                return;
            ResetSpeed();
            Vector3 p1 = transform.position + m_CharacterController.center + Vector3.up * -m_CharacterController.height * 0.5F;
            Vector3 p2 = p1 + Vector3.up * m_CharacterController.height;
            // Cast character controller shape forward to see if it is about to hit anything.
            RaycastHit[] hits = Physics.CapsuleCastAll(p1, p2, m_CharacterController.radius, transform.forward, interactDistance, interactLayerMask, QueryTriggerInteraction.Collide);

            foreach (RaycastHit hit in hits)
            {
                AInteractable interactable = hit.transform.GetComponent<AInteractable>();
                if (interactable != null)
                    interactable.Interact(this);
            }
        }

        public bool CheckForDeath()
        {
            if (!IsDead && m_PlayerStats.Resources[Resource.Oxygen].Quantity <= 0)
            {
                IsDead = true;
                _handSlots.Drop();
                m_Inventory.DropAll();
                ResetSpeed();
                _deathType = DeathType.Suffocation;
            }
            
            return IsDead;
        }

        public void Respawn()
        {
            m_PlayerStats.Resources[Resource.Oxygen].Quantity = m_PlayerStats.Resources[Resource.Oxygen].Limit;
            IsDead = false;
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

        private void UseItem(InputAction.CallbackContext ctx)
        {
            if (IsGrounded && !IsInteracting && _handSlots.IsObjectUsable && !IsDead)
            {
                ResetSpeed();
                UseItemValue = _handSlots.EquippedItem.Animation.anim.ToInt();
                _handSlots.UseItem();    
            }
        }

        private void CancelUseItem(InputAction.CallbackContext ctx)
        {
            if (IsUsingItem && _handSlots.EquippedItem != null && !IsDead)
            {
                if (_handSlots.CancelUse())
                    UseItemValue = -1;
            }
        }

    }
}