using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Input.Readers
{
    public class GameplayInputReader : BaseInputReader, IInitializable, IDisposable
    {
        [Header("Action Map References")] 
        private InputActionReference _moveActionReference;
        private InputActionReference _jumpActionReference;
        private InputActionReference _interactionReference;
        private InputActionReference _objectInteractionReference;

        private InputAction _moveAction;
        private InputAction _jumpAction;
        private InputAction _interactionAction;
        private InputAction _objectInteractionAction;

        private Action<InputAction.CallbackContext> _moveActionDelegate;
        private Action<InputAction.CallbackContext> _jumpActionDelegate;
        private Action<InputAction.CallbackContext> _interactionActionDelegate;
        private Action<InputAction.CallbackContext> _objectInteractionActionDelegate;

        public Vector2 MoveInput { get; private set; }

        public Action OnJumpTriggered;
 
        public Action OnInteractionTriggered;

        public Action OnObjectInteractionTriggered;

        [Inject]
        public void Construct(
            [Inject (Id = "Move")] InputActionReference moveActionReference,
            [Inject (Id = "Jump")] InputActionReference jumpActionReference,
            [Inject (Id = "Interaction")] InputActionReference interactionActionReference,
            [Inject (Id = "ObjectInteraction")] InputActionReference objectInteractionActionReference)
        {
            _moveAction = moveActionReference.action;
            _jumpAction = jumpActionReference.action;
            _interactionAction = interactionActionReference.action;
            _objectInteractionAction = objectInteractionActionReference.action;
        }

        private void EnableDefaultInput()
        {
            _moveAction.Enable();
            _jumpAction.Enable();
            _interactionAction.Enable();
            _objectInteractionAction.Enable();
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

            _objectInteractionActionDelegate = _ => OnObjectInteractionTriggered?.Invoke();
            _objectInteractionAction.performed += _objectInteractionActionDelegate;
        }

        private void UnregisterInputActions()
        {
            _moveAction.performed -= _moveActionDelegate;
            _moveAction.canceled -= _moveActionDelegate;
            _jumpAction.performed -= _jumpActionDelegate;
            _interactionAction.performed -= _interactionActionDelegate;
            _objectInteractionAction.performed -= _objectInteractionActionDelegate;
        }

        private void DisableDefaultInput()
        {
            _moveAction.Disable();
            _jumpAction.Disable();
            _interactionAction.Disable();
            _objectInteractionAction.Disable();
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