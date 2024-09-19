using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input.Readers
{
    public class UIInputReader : BaseInputReader
    {
        [Header("Action Name References")]
        [SerializeField] private InputActionReference _navigateActionReference;
        [SerializeField] private InputActionReference _submitActionReference;
        [SerializeField] private InputActionReference _cancelActionReference;
        [SerializeField] private InputActionReference _pointActionReference;
        [SerializeField] private InputActionReference _clickActionReference;
        [SerializeField] private InputActionReference _scrollWheelActionReference;
        [SerializeField] private InputActionReference _middleClickActionReference;
        [SerializeField] private InputActionReference _rightClickActionReference;
        [SerializeField] private InputActionReference _trackedDevicePositionActionReference;
        [SerializeField] private InputActionReference _trackedDeviceOrientationActionReference;

        private InputAction _navigateAction;
        private InputAction _submitAction;
        private InputAction _cancelAction;
        private InputAction _pointAction;
        private InputAction _clickAction;
        private InputAction _scrollWheelAction;
        private InputAction _middleClickAction;
        private InputAction _rightClickAction;
        private InputAction _trackedDevicePositionAction;
        private InputAction _trackedDeviceOrientationAction;
        
        private Action<InputAction.CallbackContext> _navigateActionDelegate;
        private Action<InputAction.CallbackContext> _submitActionDelegate;
        private Action<InputAction.CallbackContext> _cancelActionDelegate;
        private Action<InputAction.CallbackContext> _pointActionDelegate;
        private Action<InputAction.CallbackContext> _clickActionDelegate;
        private Action<InputAction.CallbackContext> _scrollWheelActionDelegate;
        private Action<InputAction.CallbackContext> _middleClickActionDelegate;
        private Action<InputAction.CallbackContext> _rightClickActionDelegate;
        private Action<InputAction.CallbackContext> _trackedDevicePositionActionDelegate;
        private Action<InputAction.CallbackContext> _trackedDeviceOrientationActionDelegate;
        
        private void Awake()
        {
            _navigateAction = _navigateActionReference;
            _submitAction = _submitActionReference;
            _cancelAction = _cancelActionReference;
            _pointAction = _pointActionReference;
            _clickAction = _clickActionReference;
            _scrollWheelAction = _scrollWheelActionReference;
            _middleClickAction = _middleClickActionReference;
            _rightClickAction = _rightClickActionReference;
            _trackedDevicePositionAction = _trackedDevicePositionActionReference;
            _trackedDeviceOrientationAction = _trackedDeviceOrientationActionReference;
        }

        private void OnEnable()
        {
            _navigateAction.Enable();
            _submitAction.Enable();
            _cancelAction.Enable();
            _pointAction.Enable();
            _clickAction.Enable();
            _scrollWheelAction.Enable();
            _middleClickAction.Enable();
            _rightClickAction.Enable();
            _trackedDevicePositionAction.Enable();
            _trackedDeviceOrientationAction.Enable();
        }
        
        private void OnDisable()
        {
            _navigateAction.Disable();
            _submitAction.Disable();
            _cancelAction.Disable();
            _pointAction.Disable();
            _clickAction.Disable();
            _scrollWheelAction.Disable();
            _middleClickAction.Disable();
            _rightClickAction.Disable();
            _trackedDevicePositionAction.Disable();
            _trackedDeviceOrientationAction.Disable();
        }
    }
}