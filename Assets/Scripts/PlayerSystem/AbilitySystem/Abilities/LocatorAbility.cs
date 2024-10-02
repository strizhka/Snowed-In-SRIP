using DG.Tweening;
using Input.Readers;
using PlayerSystem.AbilitySystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace PlayerSystem
{
    public class LocatorAbility : BaseAbility
    {
        public override Ability Ability => Ability.Locator;

        private readonly float _cooldownTime;
        private readonly float _searchRange;
        private readonly LayerMask _hiddenWallsLayerMask;
        private readonly GameplayInputReader _inputReader;

        private float _lastSearch = -Mathf.Infinity;

        [Inject]
        public LocatorAbility(Player player, GameplayInputReader inputReader, float cooldownTime, float attackRange, LayerMask interactiveLayerMask)
        {
            _player = player;
            _inputReader = inputReader;
            _cooldownTime = cooldownTime;
            _searchRange = attackRange;
            _hiddenWallsLayerMask = interactiveLayerMask;
        }

        public override void Enable()
        {
            base.Enable();
            _inputReader.OnLocatorTriggered += Locate;
        }

        public override void Disable()
        {
            base.Disable();
            _inputReader.OnLocatorTriggered -= Locate;
        }

        private void Locate()
        {

            if (Time.time - _lastSearch >= _cooldownTime)
            {
                _lastSearch = Time.time;

                Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(_player.transform.position, _searchRange, _hiddenWallsLayerMask);

                Debug.Log(_hiddenWallsLayerMask.value);
                Debug.Log(_searchRange.ToString());

                foreach (Collider2D col in objectsInRange)
                {
                    SpriteRenderer spriteRenderer = col.GetComponent<SpriteRenderer>();

                    if (spriteRenderer != null)
                    {
                        spriteRenderer.DOFade(0.5f, 0.75f);

                        DOVirtual.DelayedCall(5f, () => spriteRenderer.DOFade(1f, 1f));
                    }
                }
            }
        }

        public override void DrawGizmos()
        {
            base.DrawGizmos();

            Gizmos.DrawWireSphere(_player.transform.position, _searchRange);
        }

    }
}

