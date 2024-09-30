using System;
using Input.Readers;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace PlayerSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [Header("Movement Settings")] [SerializeField]
        private float _speed = 5f;

        [field: SerializeField] public float JumpForce { get; private set; } = 10f;
        
        [SerializeField] private float _groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Transform _groundCheck;

        private GameplayInputReader _gameplayInputReader;
        private GameStateMachine _gameStateMachine;
        private AbilityManager _abilityManager;

        public Rigidbody2D Rb { get; private set; }
        public bool IsGrounded { get; private set; }

        [Inject]
        private void Construct(GameplayInputReader inputReader, GameStateMachine gameStateMachine,
            AbilityManager abilityManager)
        {
            _gameplayInputReader = inputReader;
            _gameStateMachine = gameStateMachine;
            _abilityManager = abilityManager;
        }

        private void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
        }

        public void Initialize()
        {
            _gameStateMachine.ChangeState(GameState.Gameplay);
        }

        private void OnEnable()
        {
            _gameplayInputReader.OnJumpTriggered += Jump;
            
            _abilityManager.EnableAbility(Ability.ObjectInteraction);
            _abilityManager.EnableAbility(Ability.DoubleJump);
        }

        private void OnDisable()
        {
            _gameplayInputReader.OnJumpTriggered -= Jump;
            
            _abilityManager.DisableAbility(Ability.ObjectInteraction);
            _abilityManager.DisableAbility(Ability.DoubleJump);
        }

        private void Update()
        {
            IsGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);

            _abilityManager.UpdateAbilities();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            var moveInput = _gameplayInputReader.MoveInput;
            var moveDirection = new Vector2(moveInput.x, 0f);

            Rb.velocity = new Vector2(moveDirection.x * _speed, Rb.velocity.y);
        }

        private void Jump()
        {
            if (IsGrounded)
            {
                Rb.velocity = new Vector2(Rb.velocity.x, JumpForce);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (Application.isPlaying)
                _abilityManager.DrawGizmos();
        }
    }
}