using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using NOOD;

namespace ManagementApp
{
    public class Item
    {
        public string _name;
        public string _placeHolder;
        public Sprite _icon;
        public int _quantity;
    }
    
    public class DetailDestination : MonoBehaviorInstance<DetailDestination>
    {
        Dictionary<string, List<Item>> _detailDestinationsDic = new Dictionary<string, List<Item>>();
        List<DetailDestinationElement> _detailDestinationElements = new List<DetailDestinationElement>();
        [SerializeField] TMPro.TextMeshProUGUI _title;
        [SerializeField] Button _backButton;
        [SerializeField] DetailDestinationElement _elementPref;
        [SerializeField] RectTransform _elementParentTransform;
        

        // Start is called before the first frame update
        void Start()
        {
            _elementPref.gameObject.SetActive(false);
            _backButton.onClick.AddListener(() => UIManager.Instance.MoveRight(this.GetComponent<RectTransform>()));
            _elementPref.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void UpdateUI(Dictionary<string, List<Item>> placeAndItemDic)
        {
            foreach(var detailDestinationElement in _detailDestinationElements)
            {
                detailDestinationElement.gameObject.SetActive(false);
            }

            int i = 0;
            foreach(var detailDestinationElement in placeAndItemDic)
            {
                if(detailDestinationElement.Value.Count == 0)
                {
                    continue;
                }

                if(i >= _detailDestinationElements.Count)
                {
                    // Create new Element 
                    CreateNewElement(detailDestinationElement.Key, detailDestinationElement.Value);
                    i++;
                    continue;
                }

                DetailDestinationElement element = _detailDestinationElements[i];
                element.gameObject.SetActive(true);
                element.SetPlaceName(detailDestinationElement.Key);
                element.SetItemList(detailDestinationElement.Value);
                element.UpdateUI();
                i++;
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(_elementParentTransform);
        }

        public void SetName(string name)
        {
            _title.text = name;
        }

        public void ShowScene()
        {
            UIManager.Instance.MoveLeft(this.GetComponent<RectTransform>());
        }
        public void HideScene()
        {
            UIManager.Instance.MoveRight(this.GetComponent<RectTransform>());
        }

        public void CreateNewElement(string placeName, List<Item> items)
        {
            DetailDestinationElement element = Instantiate(_elementPref, _elementParentTransform);
            element.gameObject.SetActive(true);
            element.SetPlaceName(placeName);
            element.SetItemList(items);
            element.UpdateUI();
            _detailDestinationElements.Add(element);
        }
    }
    
}
