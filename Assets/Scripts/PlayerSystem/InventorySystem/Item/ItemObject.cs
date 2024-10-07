using UnityEngine;

namespace PlayerSystem.InventorySystem.Item
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Item")]
    public class ItemObject : ScriptableObject
    {
        [field: SerializeField] public ItemType ItemType { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public bool IsStackable { get; private set; }
        [field: SerializeField] public string QuestID { get; private set; }
    }
}