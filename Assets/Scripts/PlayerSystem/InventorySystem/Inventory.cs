using System.Collections.Generic;
using PlayerSystem.InventorySystem.Item;
using UnityEngine;

namespace PlayerSystem.InventorySystem
{
    public class Inventory
    {
        private readonly List<InventoryItem> _items = new();
        private int _cheeseMoney;

        public void Initialize(List<InventoryItem> items)
        {
            _items.Clear();
            _items.AddRange(items);
        }

        public void AddItem(ItemType itemType, int amount)
        {
            var item = _items.Find(i => i.ItemType == itemType);
            item?.AddAmount(amount);

            if (itemType == ItemType.Cheese)
            {
                AddMoney(amount);
            }
        }

        private void RemoveItem(ItemType itemType, int amount)
        {
            var item = _items.Find(i => i.ItemType == itemType);
            item?.RemoveAmount(amount);
        }

        public List<InventoryItem> GetItems()
        {
            return _items;
        }

        public int GetMoney()
        {
            return _cheeseMoney;
        }

        public void AddMoney(int amount)
        {
            _cheeseMoney += amount;
        }

        public void SpendMoney(int amount)
        {
            _cheeseMoney -= amount;

            if (_cheeseMoney < 0)
            {
                _cheeseMoney = 0;
            }

            RemoveItem(ItemType.Cheese, amount);
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}