using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NOOD;

namespace ManagementApp
{
    public class EditItemWindow : WindowBase<EditItemWindow>
    {
        [SerializeField] RectTransform _panel;
        [SerializeField] Image _icon;
        [SerializeField] InputField _nameText, _quantityText;
        [SerializeField] TMP_Dropdown _placePicker;
        [SerializeField] Button _addPlacePickerBtn, _confirmChangeBtn, _deleteBtn;
        [SerializeField] Button _increaseBtn, _decreaseBtn;
        [SerializeField] Button _iconPickerBtn;
        private Item _item;

        protected override RectTransform Panel => _panel;

        protected override void Awake()
        {
            base.Awake();
            _placePicker.ClearOptions();
            _placePicker.AddOptions(AppManager.Instance.GetPlaceNames());
            _confirmChangeBtn.onClick.AddListener(ConfirmChange);
            _deleteBtn.onClick.AddListener(DeleteItem);
            _addPlacePickerBtn.onClick.AddListener(() => UIManager.Instance.ActiveWindow(Window.AddNewPlace, true));
            _increaseBtn.onClick.AddListener(() => ModifyQuantity(1));
            _decreaseBtn.onClick.AddListener(() => ModifyQuantity(-1));
            _iconPickerBtn.onClick.AddListener(() =>
            {
                UIManager.Instance.ActiveWindow(Window.IconPicker, true);
                IconPicker.Instance.SetIconTo(_icon);
            });
        }

        void Start() 
        {
            AppManager.Instance.onDataChange += UpdateData;
        }

        private void ModifyQuantity(int modifier)
        {
            int quantity = int.Parse(_quantityText.text.Trim());
            quantity += modifier;
            _quantityText.text = quantity.ToString();
        }

        private void UpdateData()
        {
            _placePicker.ClearOptions();
            _placePicker.AddOptions(AppManager.Instance.GetPlaceNames());
        }

        public void SetItem(Item item)
        {
            _item = item;
            _icon.sprite = item._icon;
            _nameText.text = item._name;
            _quantityText.text = item._quantity.ToString();
            int index = GetPlacePickerValue(item._placeHolder);
            if(index > -1)
                _placePicker.value = index;
    }

        public void ConfirmChange()
        {
            _item._name = _nameText.text.Trim();
            _item._icon = _icon.sprite;
            _item._placeHolder = _placePicker.options[_placePicker.value].text;
            _item._quantity = int.Parse(_quantityText.text.Trim());
            AppManager.Instance.UpdateUI();
        }

        public void DeleteItem()
        {
            UIManager.Instance.ActiveWindow(Window.YesNoWindow, true);
            YesNoWindow.Instance.SetMessage($"Xác nhận xóa {_item._name}");
            YesNoWindow.Instance.SetYesNoAction(() => AppManager.Instance.DeleteItem(_item._placeHolder, _item));
        }

        public int GetPlacePickerValue(string placeName)
        {
            for (int i = 0; i < _placePicker.options.Count; i++)
            {
                if(_placePicker.options[i].text == placeName) return i;
            }
            return -1;
        }

        public override void UpdateUI()
        {
            UpdateData();
        }
    }

}
