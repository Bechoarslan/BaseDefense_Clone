using System;
using Runtime.Enums.UI;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class CanvasManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        

        #endregion

        #region Private Variables

        

        #endregion

        #endregion


        private void Awake()
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.StartPanel);
        }

        public void OnStartGame()
        {
            CoreGameSignals.Instance.onLoadLevel?.Invoke();
        }
    }
}