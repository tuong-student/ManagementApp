using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NOOD;

namespace ManagementApp
{
    public class YesNoWindow : WindowBase<YesNoWindow>
    {
        [SerializeField] private RectTransform _panel;
        [SerializeField] TextMeshProUGUI _messageText;
        [SerializeField] Button _confirmBtn, _cancelBtn;
        [SerializeField] Action _yesAction, _noAction;

        protected override RectTransform Panel => _panel;

        protected override void Awake()
        {
            base.Awake();
            _confirmBtn.onClick.AddListener(() => _yesAction?.Invoke());
            _cancelBtn.onClick.AddListener(() => _noAction?.Invoke());
        }

        public void SetMessage(string message)
        {
            _messageText.text = message;
        }

        public void SetYesNoAction(Action yesAction = null, Action noAction = null)
        {
            _yesAction = yesAction;
            _noAction = noAction;
            _yesAction += HideWindow;
            _noAction += HideWindow;
        }

        public override void UpdateUI()
        {
        }
    }
}
