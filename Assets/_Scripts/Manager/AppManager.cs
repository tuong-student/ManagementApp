using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using NOOD;
using NOOD.Extension;
using ES3Internal;

namespace ManagementApp
{
    public class DestinationClass
    {
        public string _name;
        public Sprite _icon;
    }
    
    public class AppManager : MonoBehaviorInstance<AppManager>
    {
        public Action onDataChange;

        [SerializeField] Button _plusBtn;

        private Dictionary<string, List<Item>> _placeAndItemDic = new Dictionary<string, List<Item>>();
        private Dictionary<DestinationClass, Dictionary<string, List<Item>>> _destinationDic = new Dictionary<DestinationClass, Dictionary<string, List<Item>>>();
        private string _currentDestination;

        [SerializeField] Sprite _icon;
        private Action _plusAction;

        void Awake()
        {
            UIManager.Instance.onSwitchScene += SetPlusBtn;
            _plusBtn.onClick.AddListener(() => _plusAction?.Invoke());
            LoadData();
            UpdateUI();
        }

        void Start()
        {
        }

        private void SetPlusBtn(Window windowType)
        {
            switch(windowType)
            {
                case Window.DestinationManager:
                    _plusAction = StartAddNewDestination;
                    break;
                case Window.DetailDestination:
                    _plusAction = StartAddNewItemWindow;
                    break;
            }
        }

        #region Destination Zone
        public void StartAddNewDestination()
        {
            UIManager.Instance.ActiveWindow(Window.AddNewDestination, true);
        }

        public void AddNewDestination(string destinationName, Sprite icon)
        {
            if(_destinationDic.Keys.Any(x => x._name == destinationName))
            {
                UIManager.Instance.ActiveWindow(Window.YesNoWindow, true);
                Action closeWindow = () => UIManager.Instance.ActiveWindow(Window.YesNoWindow, false);
                YesNoWindow.Instance.SetMessage("Already exists " + destinationName);
                // YesNoWindow.Instance.SetYesNoAction(closeWindow, closeWindow);
            }
            else    
            {
                _destinationDic.Add(new DestinationClass
                {
                    _name = destinationName,
                    _icon = icon
                }, new Dictionary<string, List<Item>>());
            }
            UpdateUI();
        }

        public void UpdateDestinationUI()
        {
            Destination.Instance.UpdateUI(_destinationDic);
        }

        public void OpenDestination(string destinationName)
        {
            foreach(var destinationPair in _destinationDic)
            {
                if(destinationPair.Key._name == destinationName)
                {
                    _placeAndItemDic = destinationPair.Value;
                }
            }
            UIManager.Instance.ActiveWindow(Window.DetailDestination, true);
            DetailDestination.Instance.SetName(destinationName);
            UpdateUI();
        }
        #endregion

        public void UpdateUI()
        {
            DetailDestination.Instance.UpdateUI(_placeAndItemDic);
            Destination.Instance.UpdateUI(_destinationDic);
            onDataChange?.Invoke();
            SaveData();
        }

        private void StartAddNewItemWindow()
        {
            UIManager.Instance.ActiveWindow(Window.AddNewItem, true);
        }

        public void AddNewItemRequest(string placeName,  Item item)
        {
            if(_placeAndItemDic.ContainsKey(placeName))
            {
                Item duplicate = _placeAndItemDic[placeName].FirstOrDefault(x => x._name == item._name);
                if(duplicate != default)
                {
                    duplicate._quantity += item._quantity;
                }
                else
                    _placeAndItemDic[placeName].Add(item);
            }
            UpdateUI();
        }
        public void DeleteItem(string place, Item item)
        {
            _placeAndItemDic[place].Remove(item);
            UpdateUI();
        }

        public void AddPlaceRequest(string placeName)
        {
            if(_placeAndItemDic.ContainsKey(placeName))
            {
                // Already contains
                UIManager.Instance.ActiveWindow(Window.YesNoWindow, true);
                YesNoWindow.Instance.SetMessage("Already contains " + placeName);
                Action closeWindow = () => UIManager.Instance.ActiveWindow(Window.YesNoWindow, false);
                // YesNoWindow.Instance.SetYesNoAction(closeWindow, closeWindow);
            }
            else    
            {
                _placeAndItemDic.Add(placeName, new List<Item>());
            }
            SaveData();
        }

        public void ChangePlaceName(string oldPlace, string newPlace)
        {
            if(_placeAndItemDic.ContainsKey(oldPlace))
            {
                _placeAndItemDic.RenameKey(oldPlace, newPlace);
            }
            UpdateUI();
        }
        public void DeletePlace(string placeName)
        {
            if(_placeAndItemDic.ContainsKey(placeName))
            {
                _placeAndItemDic.Remove(placeName);
            }
            else
            {
                UIManager.Instance.ActiveWindow(Window.YesNoWindow, true);
                YesNoWindow.Instance.SetMessage("No place name");
                // YesNoWindow.Instance.SetYesNoAction(() => UIManager.Instance.ActiveWindow(Window.YesNoWindow, false), () => 
                // UIManager.Instance.ActiveWindow(Window.YesNoWindow, false));
            }
            UpdateUI();
        }

        public List<string> GetPlaceNames()
        {
            return _placeAndItemDic.Keys.ToList();
        }

        public void SaveData()
        {
            onDataChange?.Invoke();
            ES3.Save("_destinationDic", _destinationDic);
        }
        public void LoadData()
        {
            if(ES3.KeyExists("_destinationDic"))
                _destinationDic = ES3.Load<Dictionary<DestinationClass, Dictionary<string, List<Item>>>>("_destinationDic");
        }
    }
}
