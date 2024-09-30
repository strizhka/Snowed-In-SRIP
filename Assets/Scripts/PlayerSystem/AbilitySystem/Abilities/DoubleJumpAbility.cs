using Input.Readers;
using UnityEngine;
using Zenject;

namespace PlayerSystem
{
    public class DoubleJumpAbility : BaseAbility
    {
        public override Ability Ability => Ability.DoubleJump;

        private bool _isDoubleJumpAvailable;
        private readonly GameplayInputReader _inputReader;

        [Inject]
        public DoubleJumpAbility(Player player, GameplayInputReader inputReader)
        {
            _player = player;
            _inputReader = inputReader;
        }

        public override void Enable()
        {
            base.Enable();
            _inputReader.OnJumpTriggered += PerformDoubleJump;
        }

        public override void Disable()
        {
            base.Disable();
            _inputReader.OnJumpTriggered -= PerformDoubleJump;
        }

        public override void Execute()
        {
            base.Execute();

            if (_player.IsGrounded)
            {
                _isDoubleJumpAvailable = true;
            }
        }

        private void PerformDoubleJump()
        {
            if (_isDoubleJumpAvailable && !_player.IsGrounded)
            {
                _player.Rb.velocity = new Vector2(_player.Rb.velocity.x, _player.JumpForce);
                _isDoubleJumpAvailable = false;
            }
        }
    }
}