using System;
using UnityEngine.InputSystem;
using Zenject;

namespace Input.Readers
{
    public class UIInputReader : BaseInputReader
    {
        private InputActionReference _navigateActionReference;
        private InputActionReference _submitActionReference;
        private InputActionReference _cancelActionReference;
        private InputActionReference _pointActionReference;
        private InputActionReference _clickActionReference;
        private InputActionReference _scrollWheelActionReference;
        private InputActionReference _middleClickActionReference;
        private InputActionReference _rightClickActionReference;
        private InputActionReference _trackedDevicePositionActionReference;
        private InputActionReference _trackedDeviceOrientationActionReference;

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
        
        [Inject]
        public void Construct(
            [Inject (Id = "Navigate")] InputActionReference navigateActionReference,
            [Inject (Id = "Submit")] InputActionReference submitActionReference,
            [Inject (Id = "Cancel")] InputActionReference cancelActionReference,
            [Inject (Id = "Point")] InputActionReference pointActionReference,
            [Inject (Id = "Click")] InputActionReference clickActionReference,
            [Inject (Id = "ScrollWheel")] InputActionReference scrollWheelActionReference,
            [Inject (Id = "MiddleClick")] InputActionReference middleClickActionReference,
            [Inject (Id = "RightClick")] InputActionReference rightClickActionReference,
            [Inject (Id = "TrackedDevicePosition")] InputActionReference trackedDevicePositionActionReference,
            [Inject (Id = "TrackedDeviceOrientation")] InputActionReference trackedDeviceOrientationActionReference)
        {
            _navigateAction = navigateActionReference.action;
            _submitAction = submitActionReference.action;
            _cancelAction = cancelActionReference.action;
            _pointAction = pointActionReference.action;
            _clickAction = clickActionReference.action;
            _scrollWheelAction = scrollWheelActionReference.action;
            _middleClickAction = middleClickActionReference.action;
            _rightClickAction = rightClickActionReference.action;
            _trackedDevicePositionAction = trackedDevicePositionActionReference.action;
            _trackedDeviceOrientationAction = trackedDeviceOrientationActionReference.action;
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