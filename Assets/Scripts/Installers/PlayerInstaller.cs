using System.Collections.Generic;
using Input.Readers;
using PlayerSystem;
using PlayerSystem.AbilitySystem;
using PlayerSystem.AbilitySystem.Abilities;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [Header("Object Interaction Settings")] 
        [SerializeField] private Player _player;
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private float _attackRange = 1f;
        [SerializeField] private float _searchRange = 1f;
        [SerializeField] private LayerMask _interactiveLayerMask;
        [SerializeField] private LayerMask _hiddenWallsLayerMask;
        [SerializeField] private float _cooldownTime = 1f;
        [SerializeField] private float _gravityScale = 1f;
        [SerializeField] private float _velocityScale = 1f;

        public override void InstallBindings()
        {
            Container.Bind<PlayerData>().FromInstance(_playerData).AsSingle();
            Container.Bind<Player>().FromInstance(_player).AsSingle();

            Container.Bind<BaseAbility>().To<DoubleJumpAbility>().AsTransient();
            Container.Bind<BaseAbility>().To<PropellerTailAbility>()
                .AsTransient()
                .WithArguments(_gravityScale, _velocityScale);
            Container.Bind<BaseAbility>().To<LocatorAbility>()
                .AsTransient()
                .WithArguments(_cooldownTime, _searchRange, _hiddenWallsLayerMask);
            Container.Bind<BaseAbility>().To<ObjectInteractionAbility>()
                .AsTransient()
                .WithArguments(_cooldownTime, _attackRange, _interactiveLayerMask);

            Container.Bind<AbilityManager>().AsSingle();
        }
    }
}