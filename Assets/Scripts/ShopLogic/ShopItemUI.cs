using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ShopLogic
{
    public class ShopItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private ShopItem _shopItem;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _quantityText;
        [SerializeField] private GameObject _scaleObject;

        public ShopItemType ShopItemType => _shopItem.ShopItemType;
        public int Price => _shopItem.Price;
        public int Quantity => _quantity;
        public Image Icon => _icon;

        private int _quantity;
        private Tween _tween;

        public Button Button { get; private set; }

        private void Awake()
        {
            Button = GetComponent<Button>();
        }

        public void Initialize(ShopItem shopItem)
        {
            _shopItem = shopItem;

            _icon.sprite = _shopItem.Icon;
            if (ShopItemType != ShopItemType.MonomahHat)
                _icon.SetNativeSize();

            _priceText.text = _shopItem.Price.ToString();
            _quantity = _shopItem.Quantity;

            if (shopItem.Quantity > 1)
                _quantityText.text = "x" + shopItem.Quantity;
            else
                _quantityText.gameObject.SetActive(false);
        }

        public void UpdateUI()
        {
            if (_quantity > 1)
                _quantityText.text = "x" + _quantity;
            else
                _quantityText.gameObject.SetActive(false);
        }

        public void ReduceQuantity()
        {
            _quantity--;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _tween?.Kill();
            _tween = _scaleObject.transform.DOScale(1.1f, 0.2f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _tween?.Kill();
            _tween = _scaleObject.transform.DOScale(1f, 0.2f);
        }
    }
}