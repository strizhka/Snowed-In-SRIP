using DebugLogic;
using PlayerSystem.InventorySystem.Item;
using UnityEngine;

namespace PlayerSystem.InventorySystem
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _inventoryPanel;
        [SerializeField] private GameObject _inventoryItemUIPrefab;

        [SerializeField, ReadOnly] private InventoryItemUI[] _inventoryItemUIs;

        public void Initialize(Inventory inventory)
        {
            var items = inventory.GetItems();

            _inventoryItemUIs = new InventoryItemUI[items.Count];
            for (var i = 0; i < items.Count; i++)
            {
                var inventoryItemUI = Instantiate(_inventoryItemUIPrefab, _inventoryPanel).GetComponent<InventoryItemUI>();
                inventoryItemUI.Initialize(items[i]);
                _inventoryItemUIs[i] = inventoryItemUI;
            }
        }

        public void UpdateUI()
        {
            foreach (var itemUI in _inventoryItemUIs)
            {
                itemUI.UpdateUI();
            }
        }
    }
}