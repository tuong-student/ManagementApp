using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NOOD;


namespace ManagementApp
{
    public class Destination : MonoBehaviorInstance<Destination>
    {
        [SerializeField] DestinationElement _elementPref;
        [SerializeField] RectTransform _elementParentTransform;
        private List<DestinationElement> _elementList = new List<DestinationElement>();

        void Awake() 
        {
            _elementList.Add(_elementPref);
        }

        public void UpdateUI(Dictionary<DestinationClass, Dictionary<string, List<Item>>> destinationDic)
        {
            int i = 0;
            foreach(var element in _elementList)
            {
                element.gameObject.SetActive(false);
            }
            foreach(var pair in destinationDic)
            {
                if(i >= _elementList.Count)
                {
                    CreateNewDestinationElement(pair.Key._name, pair.Value);
                }
                else
                {
                    _elementList[i].gameObject.SetActive(true);
                    _elementList[i].SetName(pair.Key._name);
                    _elementList[i].SetItemNumber(CountItemNumber(pair.Value));
                }
                i++;
            }
            _elementParentTransform.ForceUpdateRectTransforms();
        }

        public void ShowScene()
        {
            UIManager.Instance.MoveLeft(this.GetComponent<RectTransform>());
        }
        public void HideScene()
        {
            UIManager.Instance.MoveRight(this.GetComponent<RectTransform>());
        }

        public void CreateNewDestinationElement(string name, Dictionary<string, List<Item>> data)
        {
            DestinationElement newElement = Instantiate(_elementPref, _elementParentTransform);
            newElement.gameObject.SetActive(true);
            newElement.SetName(name);
            newElement.SetItemNumber(CountItemNumber(data));
            _elementList.Add(newElement);
        }

        private int CountItemNumber(Dictionary<string, List<Item>> itemDic)
        {
            int count = 0;
            foreach (var items in itemDic)
            {
                foreach(var item in items.Value)
                {
                    count += item._quantity;
                }
            }
            return count;
        }
    }

}
