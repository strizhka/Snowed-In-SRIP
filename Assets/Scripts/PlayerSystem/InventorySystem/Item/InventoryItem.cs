using UnityEngine;

namespace PlayerSystem.InventorySystem.Item
{
    public class InventoryItem
    {
        public ItemType ItemType { get; private set; }
        public int Amount { get; private set; }
        public Sprite Sprite { get; private set; }
        public bool IsStackable { get; private set; }

        public void Initialize(ItemObject itemObject, int amount)
        {
            ItemType = itemObject.ItemType;
            Amount = amount;
            Sprite = itemObject.Sprite;
            IsStackable = itemObject.IsStackable;
        }

        public void AddAmount(int amount)
        {
            Amount += amount;
        }

        public void RemoveAmount(int amount)
        {
            Amount -= amount;
        }
    }
}