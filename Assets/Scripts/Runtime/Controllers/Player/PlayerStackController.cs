using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums;
using Runtime.Signals;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerStackController : MonoBehaviour
    {

        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Transform bulletHolder;

        #endregion
        [SerializeField] private GameObject cubePrefab;


        #region Private Variables

        private PlayerData _playerData;
        private bool _stopCoroutine;

        #endregion

        #endregion

        public void GetPlayerData(CD_PlayerData playerData) => _playerData = playerData.Data;

        public void OnPlayerInteractedWithBuyBulletArea(Transform buyBulletArea)
        {
            if(bulletHolder.childCount >= _playerData.BulletMaxCount) return;
            var countGetBulletCount = _playerData.BulletMaxCount - bulletHolder.childCount;
           
            var listOfPools = PoolSignals.Instance.onGetPoolObject?.Invoke(countGetBulletCount, PoolTypes.DepositBullet,buyBulletArea);
            if (listOfPools == null) return;
            foreach (var bullet in listOfPools)
            {
                
                bullet.SetActive(true);
                var bulletAreaPos = new Vector3(buyBulletArea.position.x, buyBulletArea.position.y, buyBulletArea.position.z +0.9f);
                bullet.transform.position = bulletAreaPos;
                bullet.transform.DOLocalMoveY(2f, 0.2f).SetEase(Ease.OutBounce).onComplete += () =>
                {

                    var newPos = new Vector3(bulletHolder.transform.localPosition.x, bulletHolder.transform.localPosition.y +  (bulletHolder.transform.childCount / 2f), bulletHolder.transform.localPosition.z + 0.678f);
                    bullet.transform.parent = bulletHolder;
                    bullet.transform.localRotation = Quaternion.identity;
                    bullet.transform.DOLocalMove(newPos, 0.5f);
                  


                };
            }

        }

        public void OnPlayerInteractedWithPutBulletArea(Transform putBulletArea)
        {
            if(bulletHolder.childCount <= 0) return;
            _stopCoroutine = false;
            StartCoroutine(PutTheBulletsInBulletArea(putBulletArea));



        }

        private IEnumerator PutTheBulletsInBulletArea(Transform putBulletArea)
        {
            var childCount = bulletHolder.childCount;
            for (int i = 0; i < childCount; i++)
            {
                if(childCount <= 0 || _stopCoroutine) yield break;
                var bullet = bulletHolder.GetChild(bulletHolder.childCount - 1).gameObject;
                var stackPos = _playerData.BulletPositions[putBulletArea.childCount % 4];
                var newPos = new Vector3(stackPos.x, stackPos.y + Mathf.Floor(putBulletArea.childCount / 4f) * 12, stackPos.z);
                bullet.transform.parent = putBulletArea;
                bullet.transform.DOLocalMove(newPos, 0.5f).OnComplete(() =>
                {
                    bullet.transform.rotation = Quaternion.identity;
                });
                yield return new WaitForSeconds(0.1f);

            }
           
        
        
        
        }

        
           
        

        public void OnPlayerExitFromPutBulletArea()
        {
            Debug.LogWarning("<color=red>Player exit from put bullet area</color>");
            _stopCoroutine = true;
        }
        
       
    }
    
}