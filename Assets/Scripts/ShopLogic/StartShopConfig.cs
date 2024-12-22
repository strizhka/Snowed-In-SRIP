using UnityEngine;

namespace ShopLogic
{
    [CreateAssetMenu(fileName = "StartShopConfig", menuName = "ScriptableObjects/StartShopConfig")]
    public class StartShopConfig : ScriptableObject
    {
        [SerializeField] private ShopItem[] _shopItems;

        public ShopItem[] ShopItems => _shopItems;
    }
}