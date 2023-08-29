using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ManagementApp
{
    public class DetailDestinationElementRow : MonoBehaviour
    {
        [SerializeField] Image _verticalLine;
        [SerializeField] Image _itemIcon;
        [SerializeField] TextMeshProUGUI _itemName;
        [SerializeField] TextMeshProUGUI _quantity;
        [SerializeField] Button _rowBtn;
        private Item _item;

        void Awake()
        {
            _rowBtn.onClick.AddListener(() => 
            {
                UIManager.Instance.ActiveWindow(Window.EditItem, true);
                EditItemWindow.Instance.SetItem(_item);
            });
        }

        public void ActiveVerticalLine(bool value)
        {
            _verticalLine.gameObject.SetActive(value);
        }

        public void SetItem(Item item)
        {
            _item = item;
        }

        public void SetItemIcon(Sprite icon)
        {
            _itemIcon.sprite = icon;
        }

        public void SetItemName(string name)
        {
            _itemName.text = name;
        }

        public void SetQuantity(int quantity)
        {
            _quantity.text = quantity.ToString("0");
        }
    }
}
