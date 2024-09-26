using Input.Readers;
using UnityEngine;
using Zenject;

namespace PlayerSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _jumpForce = 10f;
        [SerializeField] private float _groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Transform _groundCheck;
        
        private GameplayInputReader _inputReader;

        private Rigidbody2D _rb;

        private bool _isGrounded;
        private bool _canJump;
        
        [Inject]
        private void Construct(GameplayInputReader inputReader)
        {
            _inputReader = inputReader;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _inputReader.OnJumpTriggered += Jump;
        }

        private void OnDisable()
        {
            _inputReader.OnJumpTriggered -= Jump;
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
            var moveInput = _inputReader.MoveInput;
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