using System.Collections.Generic;
using Input.Readers;
using PlayerSystem;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [Header("Object Interaction Settings")] 
        [SerializeField] private Player _player;
        [SerializeField] private float _attackRange = 1f;
        [SerializeField] private LayerMask _interactiveLayerMask;
        [SerializeField] private float _cooldownTime = 1f;
        
        public override void InstallBindings()
        {
            Container.Bind<Player>().FromInstance(_player).AsSingle();

            Container.Bind<BaseAbility>().To<DoubleJumpAbility>().AsTransient();
            Container.Bind<BaseAbility>().To<ObjectInteractionAbility>()
                .AsTransient()
                .WithArguments(_cooldownTime, _attackRange, _interactiveLayerMask);

            Container.Bind<AbilityManager>().AsSingle();
        }
    }
}