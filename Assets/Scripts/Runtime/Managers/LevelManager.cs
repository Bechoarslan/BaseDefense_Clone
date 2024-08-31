using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Enums.UI;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Runtime.Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Transform levelHolder;

        

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
            CoreGameSignals.Instance.onLoadLevel += OnLoadLevel;
        }

        private void OnLoadLevel()
        {
            StartCoroutine(StartLoadingLevel());

        }

        private IEnumerator StartLoadingLevel()
        {
            var result = Addressables.LoadAssetAsync<GameObject>
                ("Prefabs/Level/Level0");
            yield return result;
            if (result.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.LogWarning("<color=green>The Level is Ready to Load</color>");
                Instantiate(result.Result, Vector3.zero, Quaternion.identity,levelHolder);
                CoreUISignals.Instance.onClosePanel?.Invoke(UIPanelTypes.StartPanel);
                CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.GamePanel);
                CoreGameSignals.Instance.onSpawnNpcs?.Invoke();
            }
            else
            {
                Debug.LogWarning("<color=red> The Level is not Ready To Load </color>");
            }
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onLoadLevel -= OnLoadLevel;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}