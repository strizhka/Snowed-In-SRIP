using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Input.Readers
{
    public class BaseInputReader
    {
        private InputActionAsset _inputActionAsset;
        private string _actionMapName;
        private InputHandlerType _inputHandler;
        
        public InputHandlerType InputHandler => _inputHandler;
        
        [Inject]
        public void Construct(InputActionAsset inputActionAsset, string actionMapName, InputHandlerType inputHandler)
        {
            _inputActionAsset = inputActionAsset;
            _actionMapName = actionMapName;
            _inputHandler = inputHandler;

            if (_actionMapName == "")
                throw new ArgumentException("Action Map Name Not Assigned!");
        }

        public void EnableInput()
        {
            _inputActionAsset.FindActionMap(_actionMapName).Enable();
        }

        public void DisableInput()
        {
            _inputActionAsset.FindActionMap(_actionMapName).Disable();
        }

        public bool IsInputEnabled()
        {
            return _inputActionAsset.FindActionMap(_actionMapName).enabled;
        }
    }
}