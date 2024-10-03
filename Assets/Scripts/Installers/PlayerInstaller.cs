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
        [SerializeField] private AbilitiesData _abilitiesData;

        public override void InstallBindings()
        {
            Container.Bind<PlayerData>().FromInstance(_playerData).AsSingle();
            Container.Bind<AbilitiesData>().FromInstance(_abilitiesData).AsSingle();
            Container.Bind<Player>().FromInstance(_player).AsSingle();

            InitializeAbilities();

            Container.Bind<AbilityManager>().AsSingle();
        }

        private void InitializeAbilities()
        {
            Container.Bind<BaseAbility>().To<DoubleJumpAbility>().AsTransient();
            Container.Bind<BaseAbility>().To<PropellerTailAbility>()
                .AsTransient()
                .WithArguments(_abilitiesData.GravityScale, _abilitiesData.TargetYVelocity, _abilitiesData.TimeToReachTarget);
            Container.Bind<BaseAbility>().To<LocatorAbility>()
                .AsTransient()
                .WithArguments(_abilitiesData.CooldownTime, _abilitiesData.SearchRange, _abilitiesData.HiddenWallsLayerMask);
            Container.Bind<BaseAbility>().To<ObjectInteractionAbility>()
                .AsTransient()
                .WithArguments(_abilitiesData.CooldownTime, _abilitiesData.AttackRange, _abilitiesData.InteractiveLayerMask);
        }
    }
}