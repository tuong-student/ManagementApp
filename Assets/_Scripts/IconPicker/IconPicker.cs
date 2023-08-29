using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ManagementApp
{
    public class IconPicker : WindowBase<IconPicker>
    {
        [SerializeField] RectTransform _panel;
        [SerializeField] List<Sprite> _iconSprites = new List<Sprite>();
        List<IconElement> _iconElements = new List<IconElement>();
        [SerializeField] IconElement _iconPref;
        [SerializeField] Transform _iconParentTransform;

        protected override RectTransform Panel => _panel;
        private Image _targetImage;

        void OnEnable()
        {
            UpdateUI();
        }

        public override void UpdateUI()
        {
            foreach(var element in _iconElements)
            {
                element.gameObject.SetActive(false);
            }
            int i = 0;
            foreach(var iconSprite in _iconSprites)
            {
                if(i >= _iconElements.Count)
                {
                    CreateNewIcon(iconSprite);
                    i++;
                    continue;
                }

                _iconElements[i].gameObject.SetActive(true);
                _iconElements[i].SetIcon(iconSprite);
                i++;
            }
        }

        public void OnIconChoose(Sprite sprite)
        {
            _targetImage.sprite = sprite;
        }
        public void SetIconTo(Image image)
        {
            _targetImage = image;
        }

        public override void ShowWindow()
        {
            UIManager.Instance.Scale(Panel, Vector3.one);
        }
        public override void HideWindow()
        {
            UIManager.Instance.Scale(Panel, Vector3.zero, () => Panel.gameObject.SetActive(false));
        }

        public void CreateNewIcon(Sprite iconSprite)
        {
            IconElement iconElement = Instantiate(_iconPref, _iconParentTransform);
            iconElement.gameObject.SetActive(true);
            iconElement.SetIcon(iconSprite);
            _iconElements.Add(iconElement);
        }

    }
}
