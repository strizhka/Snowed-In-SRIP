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
        private InputActionReference _propellerTailReference;
        private InputActionReference _locatorReference;
        private InputActionReference _sharpenedTeethReference;

        private InputAction _moveAction;
        private InputAction _jumpAction;
        private InputAction _interactionAction;
        private InputAction _objectInteractionAction;
        private InputAction _propellerTailAction;
        private InputAction _locatorAction;
        private InputAction _sharpenedTeethAction;

        private Action<InputAction.CallbackContext> _moveActionDelegate;
        private Action<InputAction.CallbackContext> _jumpStartedActionDelegate;
        private Action<InputAction.CallbackContext> _jumpCancelledActionDelegate;
        private Action<InputAction.CallbackContext> _interactionActionDelegate;
        private Action<InputAction.CallbackContext> _objectInteractionActionDelegate;
        private Action<InputAction.CallbackContext> _propellerTailStartedActionDelegate;
        private Action<InputAction.CallbackContext> _propellerTailCanceledActionDelegate;
        private Action<InputAction.CallbackContext> _locatorActionDelegate;
        private Action<InputAction.CallbackContext> _sharpenedTeethActionDelegate;

        public Vector2 MoveInput { get; private set; }

        public Action OnJumpStarted;
        public Action OnJumpCancelled;
 
        public Action OnInteractionTriggered;

        public Action OnObjectInteractionTriggered;

        public Action OnPropellerTailStarted;
        public Action OnPropellerTailCanceled;

        public Action OnLocatorTriggered;

        public Action OnSharpenedTeethTriggered;

        [Inject]
        public void Construct(
            [Inject (Id = "Move")] InputActionReference moveActionReference,
            [Inject (Id = "Jump")] InputActionReference jumpActionReference,
            [Inject (Id = "Interaction")] InputActionReference interactionActionReference,
            [Inject (Id = "ObjectInteraction")] InputActionReference objectInteractionActionReference,
            [Inject (Id = "PropellerTail")] InputActionReference propellerTailActionReference,
            [Inject (Id = "Locator")] InputActionReference locatorActionReference,
            [Inject (Id = "SharpenedTeeth")] InputActionReference sharpenedTeethActionReference)
        {
            _moveAction = moveActionReference.action;
            _jumpAction = jumpActionReference.action;
            _interactionAction = interactionActionReference.action;
            _objectInteractionAction = objectInteractionActionReference.action;
            _propellerTailAction = propellerTailActionReference.action;
            _locatorAction = locatorActionReference.action;
            _sharpenedTeethAction = sharpenedTeethActionReference.action;
        }

        private void EnableDefaultInput()
        {
            _moveAction.Enable();
            _jumpAction.Enable();
            _interactionAction.Enable();
            _objectInteractionAction.Enable();
            _propellerTailAction.Enable();
            _locatorAction.Enable();
            _sharpenedTeethAction.Enable();
        }

        private void RegisterInputActions()
        {
            _moveActionDelegate = x => MoveInput = x.ReadValue<Vector2>();
            _moveAction.performed += _moveActionDelegate;
            _moveActionDelegate = _ => MoveInput = Vector2.zero;
            _moveAction.canceled += _moveActionDelegate;

            _jumpStartedActionDelegate = _ => OnJumpStarted?.Invoke();
            _jumpAction.started += _jumpStartedActionDelegate;
            
            _jumpCancelledActionDelegate = _ => OnJumpCancelled?.Invoke();
            _jumpAction.canceled += _jumpCancelledActionDelegate;

            _interactionActionDelegate = _ => OnInteractionTriggered?.Invoke();
            _interactionAction.performed += _interactionActionDelegate;

            _objectInteractionActionDelegate = _ => OnObjectInteractionTriggered?.Invoke();
            _objectInteractionAction.performed += _objectInteractionActionDelegate;

            _propellerTailStartedActionDelegate = _ => OnPropellerTailStarted?.Invoke();
            _propellerTailAction.started += _propellerTailStartedActionDelegate;

            _propellerTailCanceledActionDelegate = _ => OnPropellerTailCanceled?.Invoke();
            _propellerTailAction.canceled += _propellerTailCanceledActionDelegate;

            _locatorActionDelegate = _ => OnLocatorTriggered?.Invoke();
            _locatorAction.performed += _locatorActionDelegate;

            _sharpenedTeethActionDelegate = _ => OnSharpenedTeethTriggered?.Invoke();
            _sharpenedTeethAction.performed += _sharpenedTeethActionDelegate;
        }

        private void UnregisterInputActions()
        {
            _moveAction.performed -= _moveActionDelegate;
            _moveAction.canceled -= _moveActionDelegate;
            _jumpAction.started -= _jumpStartedActionDelegate;
            _jumpAction.canceled -= _jumpCancelledActionDelegate;
            _interactionAction.performed -= _interactionActionDelegate;
            _objectInteractionAction.performed -= _objectInteractionActionDelegate;
            _propellerTailAction.started -= _propellerTailStartedActionDelegate;
            _propellerTailAction.canceled -= _propellerTailCanceledActionDelegate;
            _locatorAction.performed -= _locatorActionDelegate;
            _sharpenedTeethAction.performed -= _sharpenedTeethActionDelegate;
        }

        private void DisableDefaultInput()
        {
            _moveAction.Disable();
            _jumpAction.Disable();
            _interactionAction.Disable();
            _objectInteractionAction.Disable();
            _propellerTailAction.Disable();
            _locatorAction.Disable();
            _sharpenedTeethAction.Disable();
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