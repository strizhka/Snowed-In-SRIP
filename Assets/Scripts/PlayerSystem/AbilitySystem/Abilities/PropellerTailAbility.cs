using Input.Readers;
using UnityEngine;
using Zenject;

namespace PlayerSystem.AbilitySystem.Abilities
{
    public class PropellerTailAbility : BaseAbility
    {
        public override Ability Ability => Ability.PropellerTail;

        public static bool IsFloating = false;
        
        private readonly float _gravityScale;

        private bool _isPropellerTailAvailable;

        private bool _isPropellerTailActive;

        private readonly float _targetYVelocity;
        private readonly float _timeToReachTarget;
        private float _initialYVelocity;
        private float _elapsedTime;

        private readonly GameplayInputReader _inputReader;

        [Inject]
        public PropellerTailAbility(Player player, GameplayInputReader inputReader, float gravityScale, float targetYVelocity, float timeToReachTarget)
        {
            _player = player;
            _inputReader = inputReader;
            _gravityScale = gravityScale;
            _targetYVelocity = targetYVelocity;
            _timeToReachTarget = timeToReachTarget;
        }

        public override void Enable()
        {
            base.Enable();
            _inputReader.OnPropellerTailStarted += OnPropellerInput;
            _inputReader.OnPropellerTailCanceled += OnPropellerUpInput;
        }

        public override void Disable()
        {
            base.Disable();
            _inputReader.OnPropellerTailStarted -= OnPropellerInput;
            _inputReader.OnPropellerTailCanceled -= OnPropellerUpInput;
        }

        private void OnPropellerInput()
        {
            if (_isPropellerTailAvailable && _player.LastOnGroundTime < 0)
            {
                _isPropellerTailActive = true;
                IsFloating = true;

                // Сохраняем начальную скорость по Y для плавного изменения
                _initialYVelocity = _player.Rb.velocity.y;
                _elapsedTime = 0f; // обнуляем таймер для плавного изменения
                _player.Rb.gravityScale = _gravityScale;
                
            }
        }

        private void OnPropellerUpInput()
        {
            _isPropellerTailActive = false;
            IsFloating = false;
            _player.Rb.gravityScale = 1;
        }

        public override void Execute()
        {
            base.Execute();

            if (_player.LastOnGroundTime < 0)
            {
                _isPropellerTailAvailable = true;
            }
            else
            {
                _isPropellerTailAvailable = false;

                _player.Rb.gravityScale = 1;
            }

            if (_isPropellerTailActive)
            {
                _elapsedTime += Time.deltaTime;

                float newYVelocity = Mathf.Lerp(_initialYVelocity, _targetYVelocity, _elapsedTime / _timeToReachTarget);

                if (_elapsedTime >= _timeToReachTarget)
                {
                    newYVelocity = _targetYVelocity;
                }

                _player.Rb.velocity = new Vector2(_player.Rb.velocity.x, newYVelocity);
            }
        }
    }
}