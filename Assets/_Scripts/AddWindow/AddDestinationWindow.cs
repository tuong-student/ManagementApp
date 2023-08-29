using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NOOD;

namespace ManagementApp
{
    public class AddDestinationWindow : WindowBase<AddDestinationWindow>
    {
        [SerializeField] RectTransform _panel;
        [SerializeField] Image _iconImage;
        [SerializeField] InputField _destinationName;
        [SerializeField] Button _iconPickerBtn, _confirmBtn, _cancelBtn;

        protected override RectTransform Panel => _panel;

        protected override void Awake() 
        {
            base.Awake();
            _confirmBtn.onClick.AddListener(CreateDestination);
            _cancelBtn.onClick.AddListener(HideWindow);
            _iconPickerBtn.onClick.AddListener(() =>
            {
                UIManager.Instance.ActiveWindow(Window.IconPicker, true);
                IconPicker.Instance.SetIconTo(_iconImage);
            });
        }

        public override void UpdateUI()
        {

        }

        private void CreateDestination()
        {
            AppManager.Instance.AddNewDestination(_destinationName.text, _iconImage.sprite);
        }
    }
}
