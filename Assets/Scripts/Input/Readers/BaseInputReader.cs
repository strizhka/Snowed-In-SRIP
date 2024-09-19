using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input.Readers
{
    public abstract class BaseInputReader : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _inputActionAsset;
        [SerializeField] private string _actionMapName;
        [SerializeField] protected InputHandlerType _inputHandler;
        
        public InputHandlerType InputHandler => _inputHandler;
        
        protected virtual void Awake()
        {
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