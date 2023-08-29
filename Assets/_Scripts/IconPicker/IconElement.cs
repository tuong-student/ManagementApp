using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ManagementApp
{
    public class IconElement : MonoBehaviour
    {
        [SerializeField] Image _icon;
        [SerializeField] Button _iconBtn;

        void Awake()
        {
            _iconBtn.onClick.AddListener(() => 
            {
                IconPicker.Instance.OnIconChoose(_icon.sprite);
                UIManager.Instance.ActiveWindow(Window.IconPicker, false);
            });
        }

        public void SetIcon(Sprite iconSprite)
        {
            _icon.sprite = iconSprite;
        }
    }

}
