using System.Linq;
using PlayerSystem.InventorySystem.Item;
using UnityEngine;
using Zenject;

namespace PlayerSystem.InventorySystem
{
    public class InventoryController
    {
        private ItemObject[] _items;

        private Inventory _inventory;
        private InventoryUI _inventoryUI;

        [Inject]
        public void Construct(ItemObject[] items, Inventory inventory, InventoryUI inventoryUI)
        {
            _items = items;
            _inventory = inventory;
            _inventoryUI = inventoryUI;

            Initialize();
        }

        private void Initialize()
        {
            var inventoryItems = new InventoryItem[_items.Length];
            for (var i = 0; i < _items.Length; i++)
            {
                inventoryItems[i] = new InventoryItem();
                inventoryItems[i].Initialize(_items[i], 0);
            }

            _inventory.Initialize(inventoryItems.ToList());

            _inventoryUI.Initialize(_inventory);
            _inventoryUI.UpdateUI();
        }

        public void AddItem(ItemType itemType, int amount)
        {
            _inventory.AddItem(itemType, amount);
            _inventoryUI.UpdateUI();
        }
    }
}