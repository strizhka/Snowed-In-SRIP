using Input.Readers;
using PlayerSystem.AbilitySystem;
using UnityEngine;
using Zenject;

namespace PlayerSystem
{
    public class PropellerTailAbility : BaseAbility
    {
        public override Ability Ability => Ability.PropellerTail;

        public static bool IsFloating = false;
        
        private readonly float _gravityScale;
        
        private readonly float _velocityScale;

        private bool _isPropellerTailAvailable;

        private readonly GameplayInputReader _inputReader;

        [Inject]
        public PropellerTailAbility(Player player, GameplayInputReader inputReader, float gravityScale, float velocityScale)
        {
            _player = player;
            _inputReader = inputReader;
            _gravityScale = gravityScale;
            _velocityScale = velocityScale;
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
                IsFloating = true;
                Vector2 velocity = _player.Rb.velocity;
                _player.Rb.velocity = new Vector2(velocity.x * 0, velocity.y * _velocityScale);
                _player.Rb.gravityScale = _gravityScale;
                
            }
        }

        private void OnPropellerUpInput()
        {
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
        }
    }
}