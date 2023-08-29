using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NOOD;

namespace ManagementApp
{
    public class EditPlaceWindow : WindowBase<EditPlaceWindow>
    {
        [SerializeField] RectTransform _panel;
        [SerializeField] InputField _placeName;
        [SerializeField] Button _confirmBtn, _deleteBtn;
        private string _oldPlaceName;

        protected override RectTransform Panel => _panel;

        protected override void Awake()
        {
            base.Awake();
            _confirmBtn.onClick.AddListener(ConfirmChange);
            _deleteBtn.onClick.AddListener(DeletePlace);
        }

        public void SetPlaceName(string placeName)
        {
            _oldPlaceName = placeName;
            _placeName.text = placeName;
        }

        public void ConfirmChange()
        {
            AppManager.Instance.ChangePlaceName(_oldPlaceName, _placeName.text.Trim());
            _oldPlaceName = _placeName.text.Trim();
        }

        public void DeletePlace()
        {
            UIManager.Instance.ActiveWindow(Window.YesNoWindow, true);
            YesNoWindow.Instance.SetMessage($"Xác nhận xóa {_oldPlaceName}");
            YesNoWindow.Instance.SetYesNoAction(() =>
            {
                AppManager.Instance.DeletePlace(_oldPlaceName);
            });
        }

        public override void UpdateUI()
        {

        }
    }
}