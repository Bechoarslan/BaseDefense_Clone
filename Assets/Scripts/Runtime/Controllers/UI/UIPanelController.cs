using System;
using System.Collections.Generic;
using Runtime.Enums.UI;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Controllers.UI
{
    public class UIPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<Transform> panels;

        #endregion

        #region Private Variables

        

        #endregion

        #endregion


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreUISignals.Instance.onOpenPanel += OnOpenPanel;
            CoreUISignals.Instance.onClosePanel += OnClosePanel;
            CoreUISignals.Instance.onDestroyAllPanels += OnDestroyAllPanels;
        }

        
        [Button("OnDestroyAllPanels")]
        private void OnDestroyAllPanels()
        {
            foreach (var panel in panels)
            {
                for (int i = 0; i < panel.childCount; i++)
                {
                    Destroy(panel.GetChild(i).gameObject);
                    
                }
                
            }
        }

        
        [Button("ClosePanel")]
        private void OnClosePanel(UIPanelTypes panelType)
        {
            if (panels.Count <= 0) return;
            for (int i = 0; i < panels[(int)panelType].childCount; i++)
            {
                Destroy(panels[(int)panelType].GetChild(i).gameObject);
                    
            }
        }

        [Button("OnOpenPanel")]
        private void OnOpenPanel(UIPanelTypes panelType)
        {
            CoreUISignals.Instance.onClosePanel?.Invoke(panelType);
            Instantiate(Resources.Load<GameObject>($"UIPanelDatas/{panelType.ToString()}"),
                panels[(int)panelType].transform);

        }

        private void UnSubscribeEvents()
        {
            CoreUISignals.Instance.onOpenPanel -= OnOpenPanel;
            CoreUISignals.Instance.onClosePanel -= OnClosePanel;
            CoreUISignals.Instance.onDestroyAllPanels -= OnDestroyAllPanels;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}