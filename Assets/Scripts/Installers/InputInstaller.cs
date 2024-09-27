using System.Collections.Generic;
using Input;
using Input.Readers;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Installers
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField] private InputActionsSO inputActionSoAsset;

        public override void InstallBindings()
        {
            Container.Bind<InputActionAsset>().FromInstance(inputActionSoAsset.InputActionAsset).AsSingle();
            
            BindGameplayActions();
            BindUIActions();
            BindInputReaders();
            BindInputReadersList();

            Container.BindInterfacesAndSelfTo<InputReaderSwitcher>().AsSingle();
        }

        private void BindInputReadersList()
        {
            List<BaseInputReader> inputHandlers = new List<BaseInputReader>
            {
                Container.Resolve<GameplayInputReader>(),
                Container.Resolve<UIInputReader>()
            };

            Container.Bind<List<BaseInputReader>>().FromInstance(inputHandlers).AsSingle();
        }

        private void BindInputReaders()
        {
            Container.BindInterfacesAndSelfTo<GameplayInputReader>().AsSingle()
                .WithArguments(inputActionSoAsset.InputActionAsset, "Gameplay", InputHandlerType.Gameplay);

            Container.BindInterfacesAndSelfTo<UIInputReader>().AsSingle()
                .WithArguments(inputActionSoAsset.InputActionAsset, "UI", InputHandlerType.UI);
        }

        private void BindGameplayActions()
        {
            Container.Bind<InputActionReference>().WithId("Move").FromInstance(inputActionSoAsset.MoveAction).AsCached();
            Container.Bind<InputActionReference>().WithId("Jump").FromInstance(inputActionSoAsset.JumpAction).AsCached();
            Container.Bind<InputActionReference>().WithId("Interaction").FromInstance(inputActionSoAsset.InteractionAction).AsCached();
        }
        
        private void BindUIActions()
        {
            Container.Bind<InputActionReference>().WithId("Navigate").FromInstance(inputActionSoAsset.NavigateAction).AsCached();
            Container.Bind<InputActionReference>().WithId("Submit").FromInstance(inputActionSoAsset.SubmitAction).AsCached();
            Container.Bind<InputActionReference>().WithId("Cancel").FromInstance(inputActionSoAsset.CancelAction).AsCached();
            Container.Bind<InputActionReference>().WithId("Point").FromInstance(inputActionSoAsset.PointAction).AsCached();
            Container.Bind<InputActionReference>().WithId("Click").FromInstance(inputActionSoAsset.ClickAction).AsCached();
            Container.Bind<InputActionReference>().WithId("ScrollWheel").FromInstance(inputActionSoAsset.ScrollWheelAction).AsCached();
            Container.Bind<InputActionReference>().WithId("MiddleClick").FromInstance(inputActionSoAsset.MiddleClickAction).AsCached();
            Container.Bind<InputActionReference>().WithId("RightClick").FromInstance(inputActionSoAsset.RightClickAction).AsCached();
            Container.Bind<InputActionReference>().WithId("TrackedDevicePosition").FromInstance(inputActionSoAsset.TrackedDevicePositionAction).AsCached();
            Container.Bind<InputActionReference>().WithId("TrackedDeviceOrientation").FromInstance(inputActionSoAsset.TrackedDeviceOrientationAction).AsCached();
        }
    }
}