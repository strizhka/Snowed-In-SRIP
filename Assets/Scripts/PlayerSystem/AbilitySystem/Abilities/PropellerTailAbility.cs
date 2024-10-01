using Input.Readers;
using UnityEngine;
using Zenject;

namespace PlayerSystem
{
    public class PropellerTailAbility : BaseAbility
    {
        public override Ability Ability => Ability.PropellerTail;

        [SerializeField] private float _gravityScale;

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
            }
        }

        private void PerformPropellerTail()
        {
            while (_isPropellerTailAvailable)
            {
                _player.Rb.gravityScale = _gravityScale;
            }
        }
    }
}
