using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ManagementApp
{
    
    public class DetailDestinationElement : MonoBehaviour
    {
        List<Item> _items = new List<Item>();
        [SerializeField] TextMeshProUGUI _placeNameText;
        [SerializeField] DetailDestinationElementRow _rowPref;
        [SerializeField] Transform _rowParentTransform;
        [SerializeField] Button _placeNameBtn;
        List<DetailDestinationElementRow> _itemsRows = new List<DetailDestinationElementRow>();
        string _placeName;

        void Awake()
        {
            _rowPref.gameObject.SetActive(false);
        }

        // Start is called before the first frame update
        void Start()
        {
            _placeNameBtn.onClick.AddListener(() =>
            {
                UIManager.Instance.ActiveWindow(Window.EditPlace, true);
                EditPlaceWindow.Instance.SetPlaceName(_placeName);
            });
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void OnDisable()
        {
            foreach(var row in _itemsRows)
            {
                row.gameObject.SetActive(false);
            }
        }

        public string GetPlaceName()
        {
            return _placeName;
        }

        public void UpdateUI()
        {
            int i = 0;
            foreach(var item in _items)
            {
                Debug.Log("_itemCount: " + _items.Count);
                DetailDestinationElementRow row;
                if(i >= _itemsRows.Count)
                {
                    // Create new ItemRow
                    row = CreateNewItemRow(item);
                }
                else
                {
                    _itemsRows[i].gameObject.SetActive(true);
                    _itemsRows[i].SetItemName(item._name);
                    _itemsRows[i].SetItemIcon(item._icon);
                    _itemsRows[i].SetQuantity(item._quantity);
                    _itemsRows[i].SetItem(item);
                    row = _itemsRows[i];
                }
                if(i == _items.Count - 1)
                {
                    // The last row
                    row.ActiveVerticalLine(false);
                }
                else
                {
                    row.ActiveVerticalLine(true);
                }
                i++;
            }
        }

        public DetailDestinationElementRow CreateNewItemRow(Item item)
        {
            DetailDestinationElementRow newRow = Instantiate(_rowPref, this.transform);
            newRow.gameObject.SetActive(true);
            newRow.SetItemIcon(item._icon);
            newRow.SetItemName(item._name);
            newRow.SetQuantity(item._quantity);
            newRow.SetItem(item);
            _itemsRows.Add(newRow);
            return newRow;
        }

        public void SetPlaceName(string name)
        {
            _placeNameText.text = name;
            _placeName = name;
        }
        public void SetItemList(List<Item> items)
        {
            _items = items;
        }
    }

}
