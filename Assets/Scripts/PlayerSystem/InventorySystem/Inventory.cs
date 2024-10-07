using System.Collections.Generic;
using PlayerSystem.InventorySystem.Item;
using UnityEngine;

namespace PlayerSystem.InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        private readonly List<InventoryItem> _items = new();

        public void Initialize(List<InventoryItem> items)
        {
            _items.Clear();
            _items.AddRange(items);
        }

        public void AddItem(ItemType itemType, int amount)
        {
            Debug.Log($"Add item: {itemType} x{amount}");
            var item = _items.Find(i => i.ItemType == itemType);
            item?.AddAmount(amount);
        }

        public List<InventoryItem> GetItems()
        {
            return _items;
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}