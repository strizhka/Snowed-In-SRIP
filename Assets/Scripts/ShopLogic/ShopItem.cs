using UnityEngine;

namespace ShopLogic
{
    [CreateAssetMenu(fileName = "ShopItem", menuName = "ScriptableObjects/ShopItem")]
    public class ShopItem : ScriptableObject
    {
        [SerializeField] private ShopItemType _shopItemType;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _price;
        [SerializeField] private int _quantity;

        public ShopItemType ShopItemType => _shopItemType;
        public Sprite Icon => _icon;
        public int Price => _price;
        public int Quantity => _quantity;
    }
}