using Input.Readers;
using UnityEngine;
using Zenject;

namespace PlayerSystem
{
    public class PropellerTailAbility : BaseAbility
    {
        public override Ability Ability => Ability.PropellerTail;

        private float _gravityScale = 0.5f;

        private bool _isPropellerTailAvailable;
        private readonly GameplayInputReader _inputReader;

        [Inject]
        public PropellerTailAbility(Player player, GameplayInputReader inputReader)
        {
            _player = player;
            _inputReader = inputReader;
        }

        public override void Enable()
        {
            base.Enable();
            _inputReader.OnPropellerTailTriggered += PerformPropellerTail;
        }

        public override void Disable()
        {
            base.Disable();
            _inputReader.OnPropellerTailTriggered -= PerformPropellerTail;
        }

        public override void Execute()
        {
            base.Execute();

            if (!_player.IsGrounded)
            {
                _isPropellerTailAvailable = true;
            }
            else
            {
                _isPropellerTailAvailable = false;
                _player.Rb.gravityScale = 1;
            }
        }

        private void PerformPropellerTail()
        {
            if (_isPropellerTailAvailable && !_player.IsGrounded)       // temp solution while there is no hold button
            {
                _player.Rb.gravityScale = _gravityScale;
            }
        }
    }
}
