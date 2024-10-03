using UnityEngine;

namespace PlayerSystem.AbilitySystem
{
    [CreateAssetMenu(fileName = "AbilitiesData", menuName = "ScriptableObjects/AbilitiesData", order = 1)]
    public class AbilitiesData : ScriptableObject
    {
        [field: Header("Locator Ability")]
        [field: SerializeField] public float SearchRange { get; private set; } = 1f;
        [field: SerializeField] public LayerMask HiddenWallsLayerMask { get; private set; }

        [field: Header("Object Interaction Ability")]
        [field: SerializeField] public float AttackRange { get; private set; } = 1f;
        [field: SerializeField] public LayerMask InteractiveLayerMask { get; private set; }
        [field: SerializeField] public float CooldownTime { get; private set; } = 1f;

        [field: Header("Propeller Tail Ability")]
        [field: SerializeField] public float GravityScale { get; private set; } = 1f;
        [field: SerializeField] public float TargetYVelocity { get; private set; } = -2f;
        [field: SerializeField] public float TimeToReachTarget { get; private set; } = 0.2f;
    }
}