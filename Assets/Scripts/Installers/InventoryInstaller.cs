using PlayerSystem.InventorySystem;
using PlayerSystem.InventorySystem.Item;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class InventoryInstaller : MonoInstaller
    {
        [SerializeField] private ItemObject[] _itemObjects;
        [SerializeField] private InventoryUI _inventoryUI;

        public override void InstallBindings()
        {
            InitializeInstances();

            Container.Bind<Inventory>().AsSingle();
            Container.BindInterfacesAndSelfTo<InventoryController>().AsSingle();
        }

        private void InitializeInstances()
        {
            Container.BindInstance(_itemObjects).AsSingle();
            Container.BindInstance(_inventoryUI).AsSingle();
        }
    }
}