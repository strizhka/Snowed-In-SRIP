using Input.Readers;
using UnityEngine;
using Zenject;

namespace PlayerSystem
{
    public class ObjectInteractionAbility : BaseAbility
    {
        public override Ability Ability => Ability.ObjectInteraction;

        private readonly float _cooldownTime;
        private readonly float _attackRange;
        private readonly LayerMask _interactiveLayerMask;
        private readonly GameplayInputReader _inputReader;

        private float _lastAttack = -Mathf.Infinity;

        [Inject]
        public ObjectInteractionAbility(Player player, GameplayInputReader inputReader, float cooldownTime, float attackRange, LayerMask interactiveLayerMask)
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
            _inputReader.OnObjectInteractionTriggered += ObjectInteraction;
        }

        public override void Disable()
        {
            base.Disable();
            _inputReader.OnObjectInteractionTriggered -= ObjectInteraction;
        }

        private void ObjectInteraction()
        {
            if (Time.time - _lastAttack >= _cooldownTime)
            {
                _lastAttack = Time.time;

                Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(_player.transform.position, _attackRange, _interactiveLayerMask);

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