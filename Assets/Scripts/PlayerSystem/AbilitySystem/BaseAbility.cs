using Zenject;

namespace PlayerSystem
{
    public abstract class BaseAbility
    {
        public abstract Ability Ability { get; }

        protected Player _player;
        private bool _isEnabled;

        [Inject]
        public void Construct(Player player)
        {
            _player = player;
        }

        public virtual void Enable()
        {
            _isEnabled = true;
        }

        public virtual void Disable()
        {
            _isEnabled = false;
        }

        public virtual void Execute()
        {
            if (!_isEnabled) return;
        }

        public virtual void DrawGizmos()
        {
            if (!_isEnabled) return;
        }
    }
}