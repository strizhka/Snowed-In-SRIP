using Input.Readers;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private GameplayInputReader _gameplayInputReader;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameplayInputReader>().FromInstance(_gameplayInputReader).AsSingle();
        }
    }
}