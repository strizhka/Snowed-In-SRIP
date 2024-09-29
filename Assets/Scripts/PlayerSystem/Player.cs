using System;
using Input.Readers;
using UnityEngine;
using Zenject;

namespace PlayerSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour, IInitializable
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _jumpForce = 10f;
        [SerializeField] private float _groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Transform _groundCheck;
        
        private GameplayInputReader _gameplayInputReader;
        private GameStateMachine _gameStateMachine;

        private Rigidbody2D _rb;

        private bool _isGrounded;
        private bool _canJump;
        
        [Inject]
        private void Construct(GameplayInputReader inputReader, GameStateMachine gameStateMachine)
        {
            _gameplayInputReader = inputReader;
            _gameStateMachine = gameStateMachine;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Initialize()
        {
            _gameStateMachine.ChangeState(GameState.Gameplay);
        }

        private void OnEnable()
        {
            _gameplayInputReader.OnJumpTriggered += Jump;
        }

        private void OnDisable()
        {
            _gameplayInputReader.OnJumpTriggered -= Jump;
        }

        private void Update()
        {
            _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            var moveInput = _gameplayInputReader.MoveInput;
            var moveDirection = new Vector2(moveInput.x, 0f);
            
            _rb.velocity = new Vector2(moveDirection.x * _speed, _rb.velocity.y);
        }

        private void Jump()
        {
            if (_isGrounded)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            }
        }
    }
}