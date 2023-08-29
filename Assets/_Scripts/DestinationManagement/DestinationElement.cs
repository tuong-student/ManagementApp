using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NOOD;

namespace ManagementApp
{
    public class DestinationElement : MonoBehaviorInstance<DestinationElement>
    {
        [SerializeField] TextMeshProUGUI _destinationName;
        [SerializeField] TextMeshProUGUI _itemNumber;
        [SerializeField] Button _elementBtn;

        void Awake() 
        {
            _elementBtn.onClick.AddListener(() =>
            {
                AppManager.Instance.OpenDestination(_destinationName.text);
            });
        }

        public void SetName(string name)
        {
            Debug.Log("desName " + name);
            _destinationName.text = name;
        }
        public void SetItemNumber(int itemNumber)
        {
            _itemNumber.text = itemNumber.ToString();
        }
    }
}
