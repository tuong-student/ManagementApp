using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NOOD;

namespace ManagementApp
{
    public abstract class WindowBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] protected Button _closeBtn;

        protected abstract RectTransform Panel{ get;}

        static object lockObj = new object();

        private static T instance;
        public static T Instance
        {
            get
            {
                lock(lockObj)
                {
                    if(instance == null)
                    {
                        instance = (T)FindObjectOfType(typeof(T));
                    }

                    if (instance == null)
                    {
                        Debug.Log("Errorrrrr: " + typeof(T) + " not exit");
                    }
                    return instance;
                }
            }
        }

        protected virtual void Awake() 
        {
            AppManager.Instance.onDataChange += UpdateUI;
            if(_closeBtn != null)
                _closeBtn.onClick.AddListener(HideWindow);
        }

        public abstract void UpdateUI();

        public virtual void ShowWindow()
        {
            UIManager.Instance.MoveUp(Panel);
        }

        public virtual void HideWindow()
        {
            UIManager.Instance.MoveDown(Panel, () => this.gameObject.SetActive(false));
        }
    }
}
