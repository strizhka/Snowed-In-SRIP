using System;
using InputLogic.Readers;
using PlayerSystem.InventorySystem;
using PlayerSystem.InventorySystem.Item;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ShopLogic
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private StartShopConfig _startShopConfig;
        [SerializeField] private HorizontalLayoutGroup _shopItemsContainer;
        [SerializeField] private ShopItemUI _shopItemPrefab;

        private InventoryController _inventoryController;
        private MenuInputReader _menuInputReader;
        private GameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(InventoryController inventoryController, MenuInputReader menuInputReader, GameStateMachine gameStateMachine)
        {
            _inventoryController = inventoryController;
            _menuInputReader = menuInputReader;
            _gameStateMachine = gameStateMachine;
        }

        private void Start()
        {
            InitializeShop();
        }

        private void OnEnable()
        {
            _gameStateMachine.ChangeState(GameState.Menu);
            _menuInputReader.OnQuitTriggered += CloseShop;
        }

        private void OnDisable()
        {
            _gameStateMachine.ChangeState(GameState.Gameplay);
            _menuInputReader.OnQuitTriggered -= CloseShop;
        }

        private void InitializeShop()
        {
            foreach (var shopItem in _startShopConfig.ShopItems)
            {
                var shopItemUI = Instantiate(_shopItemPrefab, _shopItemsContainer.transform);
                shopItemUI.Initialize(shopItem);
                shopItemUI.Button.onClick.AddListener(() => BuyItem(shopItemUI));
            }
        }

        private void CloseShop()
        {
            gameObject.SetActive(false);
        }

        private void BuyItem(ShopItemUI shopItemUI)
        {
            if (_inventoryController.GetMoney() < shopItemUI.Price)
            {
                Debug.Log("Not enough money");
                return;
            }

            switch (shopItemUI.ShopItemType)
            {
                case ShopItemType.Harmonica:
                    _inventoryController.AddItem(ItemType.Harmonica, 1);
                    break;
                case ShopItemType.Nails:
                    _inventoryController.AddItem(ItemType.Nail, 1);
                    break;
                case ShopItemType.Rope:
                    _inventoryController.AddItem(ItemType.Rope, 1);
                    break;
                case ShopItemType.Fish:
                    Debug.Log("buy fish");
                    break;
                case ShopItemType.MonomahHat:
                    Debug.Log("buy hat");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _inventoryController.SpendMoney(shopItemUI.Price);
            if (shopItemUI.Quantity > 1)
            {
                shopItemUI.ReduceQuantity();
                shopItemUI.UpdateUI();
            }
            else
            {
                Destroy(shopItemUI.gameObject);
            }
        }
    }
}