using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    [CreateAssetMenu(fileName = "InputActions", menuName = "Input/InputActions")]
    public class InputActionsSO : ScriptableObject
    {
        [field: SerializeField] public InputActionAsset InputActionAsset { get; private set;}

        [field: Header("Gameplay Actions")]
        [field: SerializeField] public InputActionReference MoveAction { get; private set;}
        [field: SerializeField] public InputActionReference JumpAction { get; private set;}
        [field: SerializeField] public InputActionReference EnvironmentInteraction { get; private set;}
        [field: SerializeField] public InputActionReference ObjectInteraction { get; private set; }
        [field: SerializeField] public InputActionReference PropellerTail { get; private set; }
        [field: SerializeField] public InputActionReference Locator { get; private set; }

        [field: Header("UI Actions")]
        [field: SerializeField] public InputActionReference NavigateAction { get; private set;}
        [field: SerializeField] public InputActionReference SubmitAction { get; private set;}
        [field: SerializeField] public InputActionReference CancelAction { get; private set;}
        [field: SerializeField] public InputActionReference PointAction { get; private set;}
        [field: SerializeField] public InputActionReference ClickAction { get; private set;}
        [field: SerializeField] public InputActionReference ScrollWheelAction { get; private set;}
        [field: SerializeField] public InputActionReference MiddleClickAction { get; private set;}
        [field: SerializeField] public InputActionReference RightClickAction { get; private set;}
        [field: SerializeField] public InputActionReference TrackedDevicePositionAction { get; private set;}
        [field: SerializeField] public InputActionReference TrackedDeviceOrientationAction { get; private set;}
    }
}