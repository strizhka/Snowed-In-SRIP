using System;
using DebugLogic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerSystem.InventorySystem.Item
{
    public class InventoryItemUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _amountText;

        private InventoryItem _inventoryItem;
        private bool IsStackable => _inventoryItem.IsStackable;
        private int Amount => _inventoryItem.Amount;

        public void Initialize(InventoryItem inventoryItem)
        {
            _inventoryItem = inventoryItem;
            _image.sprite = inventoryItem.Sprite;

            if (IsStackable)
            {
                _amountText.text = inventoryItem.Amount.ToString();
            }
            else
            {
                _amountText.gameObject.SetActive(false);
            }
        }

        public void UpdateUI()
        {
            if (IsStackable)
            {
                _amountText.text = _inventoryItem.Amount.ToString();
            }

            _image.color = Amount == 0 ? Color.black : Color.white;
        }
    }
}