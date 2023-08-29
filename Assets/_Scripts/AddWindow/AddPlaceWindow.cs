using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NOOD;

namespace ManagementApp
{
    public class AddPlaceWindow : WindowBase<AddPlaceWindow>
    {
        [SerializeField] RectTransform _panel;
        [SerializeField] InputField _inputNameText;
        [SerializeField] Button _confirmBtn, _cancelBtn;

        protected override RectTransform Panel => _panel;

        void Start() 
        {
            _confirmBtn.onClick.AddListener(AddNewPlace);
            _cancelBtn.onClick.AddListener(() => UIManager.Instance.ActiveWindow(Window.AddNewPlace, false));
        }

        public void AddNewPlace()
        {
            AppManager.Instance.AddPlaceRequest(_inputNameText.text.Trim());
            UIManager.Instance.ActiveWindow(Window.AddNewPlace, false);
        }

        public override void UpdateUI()
        {

        }
    }

}
