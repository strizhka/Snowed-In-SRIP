using UnityEngine;
using Zenject;

namespace PlayerSystem.InventorySystem.Item
{
    public class SceneItem : MonoBehaviour
    {
        [SerializeField] private ItemType _itemType;
        [SerializeField] private int _amount;

        private InventoryController _inventoryController;
        private bool _isPickedUp;

        [Inject]
        public void Construct(InventoryController inventoryController)
        {
            _inventoryController = inventoryController;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (!_isPickedUp)
                {
                    _inventoryController.AddItem(_itemType, _amount);
                    _isPickedUp = true;
                }
                Destroy(gameObject);
            }
        }
    }
}