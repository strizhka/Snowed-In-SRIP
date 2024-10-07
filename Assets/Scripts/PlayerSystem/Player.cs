using DebugLogic;
using Input.Readers;
using PlayerSystem.AbilitySystem;
using PlayerSystem.AbilitySystem.Abilities;
using UnityEngine;
using Zenject;

namespace PlayerSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        private static readonly int SpeedMultiplier = Animator.StringToHash("SpeedMultiplier");

        [Header("Ground Check")]
        [SerializeField] private float _groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Transform _groundCheck;

        private GameplayInputReader _gameplayInputReader;
        private GameStateMachine _gameStateMachine;
        private AbilityManager _abilityManager;

        public PlayerData Data { get; private set; }
        public Rigidbody2D Rb { get; private set; }
        public Animator AnimHandler { get; private set; }
        [field: SerializeField, ReadOnly] public bool IsGrounded { get; private set; }

        [field: SerializeField, ReadOnly] public bool IsFacingRight { get; private set; }
        [field: SerializeField, ReadOnly] public bool IsJumping { get; private set; }
        [field: SerializeField, ReadOnly] public float LastOnGroundTime { get; private set; }
        [field: SerializeField, ReadOnly] public float LastPressedJumpTime { get; private set; }

        [field: SerializeField, ReadOnly] private bool _isJumpCut;
        [field: SerializeField, ReadOnly] private bool _isJumpFalling;

        [Inject]
        private void Construct(GameplayInputReader inputReader, GameStateMachine gameStateMachine,
            AbilityManager abilityManager, PlayerData data)
        {
            _gameplayInputReader = inputReader;
            _gameStateMachine = gameStateMachine;
            _abilityManager = abilityManager;
            Data = data;
        }

        #region Unity Callbacks

        private void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
            AnimHandler = GetComponent<Animator>();
        }

        private void Start()
        {
            Debug.Log(Data.gravityScale);
            SetGravityScale(Data.gravityScale);
            _gameStateMachine.ChangeState(GameState.Gameplay);
        }

        private void OnEnable()
        {
            _gameplayInputReader.OnJumpStarted += OnJumpInput;
            _gameplayInputReader.OnJumpCancelled += OnJumpUpInput;

            _abilityManager.EnableAbility(Ability.ObjectInteraction);
            _abilityManager.EnableAbility(Ability.DoubleJump);
            _abilityManager.EnableAbility(Ability.PropellerTail);
            _abilityManager.EnableAbility(Ability.Locator);
        }

        private void OnDisable()
        {
            _gameplayInputReader.OnJumpStarted -= OnJumpInput;
            _gameplayInputReader.OnJumpCancelled -= OnJumpUpInput;

            _abilityManager.DisableAbility(Ability.ObjectInteraction);
            _abilityManager.DisableAbility(Ability.DoubleJump);
            _abilityManager.DisableAbility(Ability.PropellerTail);
            _abilityManager.EnableAbility(Ability.Locator);
        }

        #endregion

        #region Handle Input

        private void OnJumpInput()
        {
            LastPressedJumpTime = Data.jumpInputBufferTime;
        }

        private void OnJumpUpInput()
        {
            if (CanJumpCut())
                _isJumpCut = true;
        }

        #endregion

        private void Update()
        {
            LastOnGroundTime -= Time.deltaTime;
            LastPressedJumpTime -= Time.deltaTime;

            CheckCollision();
            CheckJump();
            ChangeGravity();

            _abilityManager.UpdateAbilities();
        }

        private void CheckCollision()
        {
            if (!IsJumping)
            {
                //Ground Check
                if (Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer)) //checks if set box overlaps with ground
                {
                    // if(LastOnGroundTime < -0.1f)
                    // {
                    //     AnimHandler.justLanded = true;
                    // }

                    LastOnGroundTime = Data.coyoteTime; //if so sets the lastGrounded to coyoteTime
                }
            }
        }

        private void CheckJump()
        {
            if (IsJumping && Rb.velocity.y < 0)
            {
                IsJumping = false;
                _isJumpFalling = true;
            }

            if (LastOnGroundTime > 0 && !IsJumping)
            {
                _isJumpCut = false;

                _isJumpFalling = false;
            }

            if (CanJump() && LastPressedJumpTime > 0)
            {
                
                Jump(Data.jumpForce);

                // AnimHandler.startedJumping = true;
            }
        }

        private void CheckTurn()
        {
            if (_gameplayInputReader.MoveInput.x > 0 && !IsFacingRight)
            {
                Turn();
            }
            else if (_gameplayInputReader.MoveInput.x < 0 && IsFacingRight)
            {
                Turn();
            }
        }

        private void ChangeGravity()
        {
            if (!PropellerTailAbility.IsFloating)
            {
                if (Rb.velocity.y < 0 && _gameplayInputReader.MoveInput.y < 0)
                {
                    SetGravityScale(Data.gravityScale * Data.fastFallGravityMult);
                    Rb.velocity = new Vector2(Rb.velocity.x, Mathf.Max(Rb.velocity.y, -Data.maxFastFallSpeed));
                }
                else if (_isJumpCut)
                {
                    SetGravityScale(Data.gravityScale * Data.jumpCutGravityMult);
                    Rb.velocity = new Vector2(Rb.velocity.x, Mathf.Max(Rb.velocity.y, -Data.maxFallSpeed));
                }
                else if ((IsJumping || _isJumpFalling) && Mathf.Abs(Rb.velocity.y) < Data.jumpHangTimeThreshold)
                {
                    SetGravityScale(Data.gravityScale * Data.jumpHangGravityMult);
                }
                else if (Rb.velocity.y < 0)
                {
                    SetGravityScale(Data.gravityScale * Data.fallGravityMult);
                    Rb.velocity = new Vector2(Rb.velocity.x, Mathf.Max(Rb.velocity.y, -Data.maxFallSpeed));
                }
                else
                {
                    SetGravityScale(Data.gravityScale);
                }
            }
        }

        private void FixedUpdate()
        {
            Move();
            AnimHandler.SetFloat(SpeedMultiplier, Mathf.Abs(Rb.velocity.x) / Data.runMaxSpeed);

            if (_gameplayInputReader.MoveInput.x != 0)
            {
                CheckTurn();
            }
        }

        private void Move()
        {
            float targetSpeed = _gameplayInputReader.MoveInput.x * Data.runMaxSpeed;
            targetSpeed = Mathf.Lerp(Rb.velocity.x, targetSpeed, 1);

            #region Calculate AccelRate

            float accelRate;

            if (LastOnGroundTime > 0)
                accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount : Data.runDeccelAmount;
            else
                accelRate = (Mathf.Abs(targetSpeed) > 0.01f)
                    ? Data.runAccelAmount * Data.accelInAir
                    : Data.runDeccelAmount * Data.deccelInAir;

            #endregion

            #region Add Bonus Jump Apex Acceleration

            if ((IsJumping || _isJumpFalling) && Mathf.Abs(Rb.velocity.y) < Data.jumpHangTimeThreshold)
            {
                accelRate *= Data.jumpHangAccelerationMult;
                targetSpeed *= Data.jumpHangMaxSpeedMult;
            }

            #endregion

            #region Conserve Momentum

            if (Data.doConserveMomentum && Mathf.Abs(Rb.velocity.x) > Mathf.Abs(targetSpeed) &&
                Mathf.Sign(Rb.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f &&
                LastOnGroundTime < 0)
            {
                accelRate = 0;
            }

            #endregion

            float speedDif = targetSpeed - Rb.velocity.x;

            float movement = speedDif * accelRate;

            Rb.AddForce(movement * Vector2.right, ForceMode2D.Force);

            if (Mathf.Abs(Rb.velocity.x) < 0.01f)
            {
                Rb.velocity = new Vector2(0, Rb.velocity.y);
            }
        }

        public void Jump(float jumpForce)
        {
            IsJumping = true;
            _isJumpCut = false;
            _isJumpFalling = false;
            
            LastPressedJumpTime = 0;
            LastOnGroundTime = 0;

            #region Perform Jump

            float force = jumpForce;
            if (Rb.velocity.y < 0)
                force -= Rb.velocity.y;

            Debug.Log(force);

            Rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);

            #endregion
        }

        private void Turn()
        {
            if (IsFacingRight)
            {
                Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
                transform.rotation = Quaternion.Euler(rotator);
                IsFacingRight = !IsFacingRight;
            }

            else
            {
                Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
                transform.rotation = Quaternion.Euler(rotator);
                IsFacingRight = !IsFacingRight;
            }
        }

        private void SetGravityScale(float gravityScale)
        {
            Rb.gravityScale = gravityScale;
        }

        private void OnDrawGizmosSelected()
        {
            if (Application.isPlaying)
            {
                _abilityManager.DrawGizmos();

                Debug.DrawLine(transform.position, transform.position + (Vector3)Rb.velocity, Color.red);
            }

            //draw player collider
            Gizmos.color = Color.green;
            Collider2D collider = GetComponent<Collider2D>();
            Gizmos.DrawWireCube(collider.bounds.center, collider.bounds.size);
        }

        private bool CanJump()
        {
            return LastOnGroundTime > 0 && !IsJumping;
        }

        private bool CanJumpCut()
        {
            return IsJumping && Rb.velocity.y > 0;
        }
    }
}