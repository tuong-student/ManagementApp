using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NOOD;

namespace ManagementApp
{
    public class AddItemWindow : WindowBase<AddItemWindow>
    {
        [SerializeField] RectTransform _panel;

        [SerializeField] Image _itemIcon;
        [SerializeField] InputField _itemNameInput;
        [SerializeField] TMP_Dropdown _placeDropdown;
        [SerializeField] InputField _quantityText;
        [SerializeField] Button _newPlaceBtn;
        [SerializeField] Button _createNewItemBtn;
        [SerializeField] Button _chooseIconBtn;

        private string _currentPlaceName;


        protected override RectTransform Panel { get => _panel;}

        void Start()
        {
            _placeDropdown.ClearOptions();
            _placeDropdown.AddOptions(AppManager.Instance.GetPlaceNames());
            _createNewItemBtn.onClick.AddListener(CreateNewItem);
            _newPlaceBtn.onClick.AddListener(() => UIManager.Instance.ActiveWindow(Window.AddNewPlace, true));
            _chooseIconBtn.onClick.AddListener(() => 
            { 
                UIManager.Instance.ActiveWindow(Window.IconPicker, true);
                IconPicker.Instance.SetIconTo(_itemIcon);
            });

            AppManager.Instance.onDataChange += UpdateData;
        }

        public override void UpdateUI()
        {
            UpdateData();
        }

        private void UpdateData()
        {
            _placeDropdown.ClearOptions();
            _placeDropdown.AddOptions(AppManager.Instance.GetPlaceNames());
        }

        public void SetNewIcon(Sprite icon)
        {
            _itemIcon.sprite = icon;
        }

        public void CreateNewItem()
        {
            _currentPlaceName = _placeDropdown.options[_placeDropdown.value].text;
            int quantity = 1;
            if(_quantityText.text.Length != 0)
            {
                quantity = int.Parse(_quantityText.text.Trim());
            }
            AppManager.Instance.AddNewItemRequest(_currentPlaceName, new Item
            {
                _name = _itemNameInput.text.Trim(),
                _placeHolder = _currentPlaceName,
                _icon = _itemIcon.sprite,
                _quantity = quantity
            });
        }
    }

}
