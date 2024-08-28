using System;
using Runtime.Enums.UI;
using Runtime.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Handlers
{
    public class UIEventSubscriber : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField] private UIButtonTypes buttonType;
        [SerializeField] private Button button;
        
        #endregion

        #region Private Variables

        private CanvasManager _canvasManager;

        #endregion

        #endregion

        private void Awake()
        {
            _canvasManager = FindObjectOfType<CanvasManager>();
        }


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            switch (buttonType)
            {
                case UIButtonTypes.StartButton:
                    button.onClick.AddListener(() =>
                    {
                        _canvasManager.OnStartGame();
                    });
                    break;
                
            }
            
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        
        private void UnSubscribeEvents()
        {
            switch (buttonType)
            {
                case UIButtonTypes.StartButton:
                    button.onClick.RemoveListener(() =>
                    {
                        _canvasManager.OnStartGame();
                    });
                    break;
                
            }
        }
    }
}