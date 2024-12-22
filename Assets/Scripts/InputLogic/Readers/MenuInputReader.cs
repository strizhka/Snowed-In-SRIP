using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace InputLogic.Readers
{
    public class MenuInputReader : BaseInputReader, IInitializable, IDisposable
    {
        [Header("Action Map References")] 
        private InputActionReference _quitActionReference;

        private InputAction _quitAction;

        private Action<InputAction.CallbackContext> _quitActionDelegate;

        public Action OnQuitTriggered;

        [Inject]
        public void Construct([Inject (Id = "Quit")] InputActionReference quitActionReference)
        {
            _quitAction = quitActionReference.action;
        }

        private void EnableDefaultInput()
        {
            _quitAction.Enable();
        }

        private void RegisterInputActions()
        {
            _quitActionDelegate = _ => OnQuitTriggered?.Invoke();
            _quitAction.performed += _quitActionDelegate;
        }

        private void UnregisterInputActions()
        {
            _quitAction.performed -= _quitActionDelegate;
        }

        private void DisableDefaultInput()
        {
            _quitAction.Disable();
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