using Input.Readers;
using UnityEngine;
using Zenject;

namespace PlayerSystem.AbilitySystem.Abilities
{
    // TODO: Refactor this class with ObjectInteractionAbility
    public class SharpenedTeethAbility : BaseAbility
    {
        public override Ability Ability => Ability.SharpenedTeeth;

        private readonly float _cooldownTime;
        private readonly float _attackRange;
        private readonly LayerMask _interactiveLayerMask;
        private readonly GameplayInputReader _inputReader;

        private float _lastAttack = -Mathf.Infinity;

        [Inject]
        public SharpenedTeethAbility(Player player, GameplayInputReader inputReader, float cooldownTime, float attackRange, LayerMask interactiveLayerMask)
        {
            _player = player;
            _inputReader = inputReader;
            _cooldownTime = cooldownTime;
            _attackRange = attackRange;
            _interactiveLayerMask = interactiveLayerMask;
        }

        public override void Enable()
        {
            base.Enable();
            _inputReader.OnSharpenedTeethTriggered += ObjectInteraction;
        }

        public override void Disable()
        {
            base.Disable();
            _inputReader.OnSharpenedTeethTriggered -= ObjectInteraction;
        }

        private void ObjectInteraction()
        {
            if (Time.time - _lastAttack >= _cooldownTime)
            {
                _lastAttack = Time.time;

                Collider2D[] objectsInRange =
                    Physics2D.OverlapCircleAll(_player.transform.position, _attackRange, _interactiveLayerMask);

                foreach (Collider2D col in objectsInRange)
                {
                    Object.Destroy(col.gameObject);
                }
            }
        }

        public override void DrawGizmos()
        {
            base.DrawGizmos();

            Gizmos.DrawWireSphere(_player.transform.position, _attackRange);
        }
    }
}