using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NOOD;

namespace ManagementApp
{
    public enum Window
    {
        DetailDestination,
        DestinationManager,
        AddNewItem,
        AddNewPlace,
        AddNewDestination,
        IconPicker,
        EditItem,
        EditPlace,
        YesNoWindow,
    }


    public class UIManager : MonoBehaviorInstance<UIManager>
   {
        public Action<Window> onSwitchScene;

        [SerializeField] WindowBase<AddItemWindow> _addNewItemWindow;
        [SerializeField] WindowBase<AddPlaceWindow> _addNewPlaceWindow;
        [SerializeField] WindowBase<AddDestinationWindow> _addNewDestinationWindow;
        [SerializeField] WindowBase<EditItemWindow> _editItemWindow;
        [SerializeField] WindowBase<EditPlaceWindow> _editPlaceWindow;
        [SerializeField] WindowBase<IconPicker> _iconPickerWindow;
        [SerializeField] WindowBase<YesNoWindow> _yesNoWindow;
        [SerializeField] RectTransform _windowOutTransform;
        [SerializeField] RectTransform _windowInTransform;
        [SerializeField] RectTransform _sceneOutTransform;
        [SerializeField] RectTransform _sceneInTransform;
        [SerializeField] DetailDestination _detailDestination;

        bool isOpen = false;

        void Awake()
        {
        }

        void Start()
        {
            // ActiveWindow(Window.AddNewItem, false);
            // ActiveWindow(Window.EditItem, false);
            // ActiveWindow(Window.YesNoWindow, false);
            // ActiveWindow(Window.EditPlace, false);
            // ActiveWindow(Window.AddNewPlace, false);
            // ActiveWindow(Window.AddNewDestination, false);
            ActiveWindow(Window.DetailDestination, false);
        }

        void Update()
        {
            // if((Input.GetKeyDown(KeyCode.Space)))
            // {
            //     isOpen = !isOpen;
            //     ActiveWindow(Window.IconPicker, isOpen);
            // }
        }

        public void ActiveWindow(Window window, bool isActive)
        {
            if(isActive)
            {
                switch(window)
                {
                    case Window.DetailDestination:
                        _detailDestination.ShowScene();
                        onSwitchScene?.Invoke(Window.DetailDestination);
                        break;
                    case Window.DestinationManager:
                        break;
                    case Window.AddNewItem:
                        _addNewItemWindow.gameObject.SetActive(true);
                        _addNewItemWindow.ShowWindow();
                        break;
                    case Window.AddNewPlace:
                        _addNewPlaceWindow.gameObject.SetActive(true);
                        _addNewPlaceWindow.ShowWindow();
                        break;
                    case Window.AddNewDestination:
                        _addNewDestinationWindow.gameObject.SetActive(true);
                        _addNewDestinationWindow.ShowWindow();
                        break;
                    case Window.IconPicker:
                        _iconPickerWindow.gameObject.SetActive(true);
                        _iconPickerWindow.ShowWindow();
                        break;
                    case Window.EditItem:
                        _editItemWindow.gameObject.SetActive(true);
                        _editItemWindow.ShowWindow();
                        break;
                    case Window.EditPlace:
                        _editPlaceWindow.gameObject.SetActive(true);
                        _editPlaceWindow.ShowWindow();
                        break;
                    case Window.YesNoWindow:
                        _yesNoWindow.gameObject.SetActive(true);
                        _yesNoWindow.ShowWindow();
                        break;
                }
            }
            else
            {
                switch(window)
                {
                    case Window.DetailDestination:
                        _detailDestination.HideScene();
                        onSwitchScene?.Invoke(Window.DestinationManager);
                        break;
                    case Window.DestinationManager:
                        break;
                    case Window.AddNewItem:
                        _addNewItemWindow.HideWindow();
                        break;
                    case Window.AddNewPlace:
                        _addNewPlaceWindow.HideWindow();
                        break;
                    case Window.AddNewDestination:
                        _addNewDestinationWindow.HideWindow();
                        break;
                    case Window.IconPicker:
                        _iconPickerWindow.HideWindow();
                        break;
                    case Window.EditItem:
                        _editItemWindow.HideWindow();
                        break;
                    case Window.EditPlace:
                        _editPlaceWindow.HideWindow();
                        break;
                    case Window.YesNoWindow:
                        _yesNoWindow.HideWindow();
                        break;
                }
            }
        }

        public void Scale(RectTransform rectTransform, Vector3 target, Action completeAction = null)
        {
            rectTransform.DOScale(target, .5f).SetEase(Ease.InOutExpo).OnComplete(() => completeAction?.Invoke());
        }

        public void MoveLeft(RectTransform rectTransform, Action completeAction = null)
        {
            rectTransform.DOMove(_sceneInTransform.position, 0.5f).SetEase(Ease.InOutExpo).OnComplete(() => completeAction?.Invoke());
        }
        public void MoveRight(RectTransform rectTransform, Action completeAction = null)
        {
            rectTransform.DOMove(_sceneOutTransform.position, 0.5f).SetEase(Ease.InOutExpo).OnComplete(() => completeAction?.Invoke());
        }

        public void MoveUp(RectTransform rectTransform, Action completeAction = null)
        {
            rectTransform.DOMove(_windowInTransform.position, 0.5f).SetEase(Ease.OutCirc).OnComplete(() => completeAction?.Invoke());
        }
        public void MoveDown(RectTransform rectTransform, Action completeAction = null)
        {
            rectTransform.DOMove(_windowOutTransform.position, 0.5f).SetEase(Ease.InCirc).OnComplete(() => rectTransform.gameObject.SetActive(false)).
                OnComplete(() => completeAction?.Invoke());
        }
    }

}
