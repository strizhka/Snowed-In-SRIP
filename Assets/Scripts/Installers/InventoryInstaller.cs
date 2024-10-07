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
        [SerializeField] private Inventory _inventory;

        public override void InstallBindings()
        {
            Container.BindInstance(_itemObjects).AsSingle();
            Container.BindInstance(_inventoryUI).AsSingle();
            Container.BindInstance(_inventory).AsSingle();

            Container.BindInterfacesAndSelfTo<InventoryController>().AsSingle();
        }
    }
}