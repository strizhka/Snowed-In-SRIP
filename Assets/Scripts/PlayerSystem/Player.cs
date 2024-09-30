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
        [Header("Movement Settings")]
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _jumpForce = 10f;
        [SerializeField] private float _groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Transform _groundCheck;

        [Header("Object Interaction Settings")]
        [SerializeField] private float _attackRange = 1f;
        [SerializeField] private LayerMask _interactiveLayerMask;
        [SerializeField] private float _cooldownTime = 1f;


        private GameplayInputReader _gameplayInputReader;
        private GameStateMachine _gameStateMachine;

        private Rigidbody2D _rb;

        private float _lastAttack = -Mathf.Infinity;

        private bool _isGrounded;
        private bool _isDoubleJumpAvailable;
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

            _gameplayInputReader.OnObjectInteractionTriggered += ObjectInteraction;
        }

        private void OnDisable()
        {
            _gameplayInputReader.OnJumpTriggered -= Jump;

            _gameplayInputReader.OnObjectInteractionTriggered -= ObjectInteraction;
        }

        private void Update()
        {
            _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);

            if (_isGrounded)
            {
                _isDoubleJumpAvailable = true;
            }
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
            else if (_isDoubleJumpAvailable)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
                _isDoubleJumpAvailable = false;
            }
        }

        private void ObjectInteraction()
        {
            if (Time.time - _lastAttack >= _cooldownTime)
            {
                _lastAttack = Time.time;

                Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, _attackRange, _interactiveLayerMask);

                foreach (Collider2D col in objectsInRange)
                {
                    Destroy(col.gameObject);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }
    }
}