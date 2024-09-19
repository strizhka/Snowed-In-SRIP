using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input.Readers
{
    public class GameplayInputReader : BaseInputReader
    {
        [Header("Action Map References")] [SerializeField]
        private InputActionReference _moveActionReference;

        [SerializeField] private InputActionReference _jumpActionReference;
        [SerializeField] private InputActionReference _interactctionReference;

        private InputAction _moveAction;
        private InputAction _jumpAction;
        private InputAction _interactionAction;

        private Action<InputAction.CallbackContext> _moveActionDelegate;
        private Action<InputAction.CallbackContext> _jumpActionDelegate;
        private Action<InputAction.CallbackContext> _interactionActionDelegate;

        public Vector2 MoveInput { get; private set; }
        public Action OnJumpTriggered;
        public Action OnInteractionTriggered;

        private void Awake()
        {
            _moveAction = _moveActionReference;
            _jumpAction = _jumpActionReference;
            _interactionAction = _interactctionReference;
        }

        private void EnableDefaultInput()
        {
            _moveAction.Enable();
            _jumpAction.Enable();
            _interactionAction.Enable();
        }

        private void RegisterInputActions()
        {
            _moveActionDelegate = x => MoveInput = x.ReadValue<Vector2>();
            _moveAction.performed += _moveActionDelegate;
            _moveActionDelegate = _ => MoveInput = Vector2.zero;
            _moveAction.canceled += _moveActionDelegate;

            _jumpActionDelegate = _ => OnJumpTriggered?.Invoke();
            _jumpAction.performed += _jumpActionDelegate;

            _interactionActionDelegate = _ => OnInteractionTriggered?.Invoke();
            _interactionAction.performed += _interactionActionDelegate;
        }

        private void UnregisterInputActions()
        {
            _moveAction.performed -= _moveActionDelegate;
            _moveAction.canceled -= _moveActionDelegate;
            _jumpAction.performed -= _jumpActionDelegate;
            _interactionAction.performed -= _interactionActionDelegate;
        }

        private void DisableDefaultInput()
        {
            _moveAction.Disable();
            _jumpAction.Disable();
            _interactionAction.Disable();
        }

        private void OnEnable()
        {
            RegisterInputActions();
            EnableDefaultInput();
        }

        private void OnDisable()
        {
            UnregisterInputActions();
            DisableDefaultInput();
        }
    }
}