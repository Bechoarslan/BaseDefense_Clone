using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Enums;
using Runtime.Enums.Player;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Turret
{
    public class TurretController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private SpriteRenderer laserBeam;
        [SerializeField] private Transform bulletArea;
        [SerializeField] private Transform bulletStartArea;

        #endregion

        #region Private Variables

        private bool _turretStopCoroutine;

        #endregion

        #endregion


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
           CoreGameSignals.Instance.onStartTurretFire += OnStartTurretFire;
           CoreGameSignals.Instance.onStopTurretFire += OnStopTurretFire;
        }

        private void OnStartTurretFire()
        {
            _turretStopCoroutine = false;
            laserBeam.gameObject.SetActive(true);
            StartCoroutine(StartTurretFire());
        }

        private void OnStopTurretFire()
        {
            laserBeam.gameObject.SetActive(false);
            _turretStopCoroutine = true;
        }
        
        private IEnumerator StartTurretFire()
        {
           
            var countOfBullet = bulletArea.childCount * 4;
            for (int i = 0; i < countOfBullet; i++)
            {
                if(_turretStopCoroutine | countOfBullet <= 0) yield break;
                Debug.LogWarning("Start Fire Bullets");
                if (i % 4 == 0)
                {
                    PoolSignals.Instance.onSendPoolObject?.Invoke(bulletArea.GetChild(bulletArea.childCount -1).gameObject,PoolTypes.DepositBullet);
                }
                var bulletList = PoolSignals.Instance.onGetPoolObject?.Invoke(countOfBullet,PoolTypes.Bullet,bulletStartArea);
                var bullet = bulletList[i];
                bullet.transform.position = bulletStartArea.position;
                bullet.transform.rotation = Quaternion.Euler(-90,0,0);
                bullet.SetActive(true);
                bullet.transform.DOLocalMove(Vector3.forward * 10f, 0.3f).SetEase(Ease.Flash).onComplete += () =>
                {
                    PoolSignals.Instance.onSendPoolObject?.Invoke(bullet,PoolTypes.Bullet);
                };
                Debug.LogWarning("Start Fire Bullets");
                yield return new WaitForSeconds(1f);
            }
            
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onStartTurretFire -= OnStartTurretFire;
            CoreGameSignals.Instance.onStopTurretFire -= OnStopTurretFire;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}