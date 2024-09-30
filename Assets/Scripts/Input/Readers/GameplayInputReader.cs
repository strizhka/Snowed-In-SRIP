using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Input.Readers
{
    public class GameplayInputReader : BaseInputReader, IInitializable, IDisposable
    {
        [Header("Action Map References")] private InputActionReference _moveActionReference;
        private InputActionReference _jumpActionReference;
        private InputActionReference _interactionReference;

        private InputAction _moveAction;
        private InputAction _jumpAction;
        private InputAction _interactionAction;

        private Action<InputAction.CallbackContext> _moveActionDelegate;
        private Action<InputAction.CallbackContext> _jumpActionDelegate;
        private Action<InputAction.CallbackContext> _interactionActionDelegate;

        public Vector2 MoveInput { get; private set; }
        public Action OnJumpTriggered;
 
        public Action OnInteractionTriggered;

        [Inject]
        public void Construct(
            [Inject (Id = "Move")] InputActionReference moveActionReference,
            [Inject (Id = "Jump")] InputActionReference jumpActionReference,
            [Inject (Id = "Interaction")] InputActionReference interactionActionReference)
        {
            _moveAction = moveActionReference.action;
            _jumpAction = jumpActionReference.action;
            _interactionAction = interactionActionReference.action;
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

        public void Initialize()
        {
            RegisterInputActions();
            EnableDefaultInput();
        }

        public void Dispose()
        {
            UnregisterInputActions();
            DisableDefaultInput();
        }
    }
}