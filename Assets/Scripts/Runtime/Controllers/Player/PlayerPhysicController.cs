using System;
using DG.Tweening;
using Runtime.Abstract;
using Runtime.Enums;
using Runtime.Enums.NPC;
using Runtime.Enums.Player;
using Runtime.Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Controllers.Player
{
    public class PlayerPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        

        #endregion

        #region Private Variables

       [SerializeField] private bool _isPlayerInSafeArea = true;

        #endregion

        #endregion


        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("BuyBullet"))
            {
                PlayerSignals.Instance.onPlayerInteractedWithBuyBulletArea?.Invoke(other.gameObject.transform);
                Debug.LogWarning("<color=green>Player interacted with buy bullet area</color>");
            }
            if (other.gameObject.CompareTag("PutBullet"))
            {
                PlayerSignals.Instance.onPlayerInteractedWithPutBulletArea?.Invoke(other.gameObject.transform);
            }

            if (other.gameObject.CompareTag("Turret"))
            {
                PlayerSignals.Instance.onChangePlayerMovementState?.Invoke(PlayerMovementState.Turret);
                PlayerSignals.Instance.onPlayerInteractedWithGunArea?.Invoke(other.gameObject.transform);
            }

            if (other.gameObject.CompareTag("Door"))
            {

                var door = other.gameObject.transform.GetChild(0);
                door.transform.DOLocalRotate(new Vector3(0, 0, 90), 1);
            }

            if (other.gameObject.CompareTag("BaseOut"))
            {
                _isPlayerInSafeArea = false;
                PlayerSignals.Instance.onIsPlayerInSafeArea?.Invoke(false);
            }

            if (other.gameObject.CompareTag("BaseIn"))
            {
                _isPlayerInSafeArea = true;
                PlayerSignals.Instance.onIsPlayerInSafeArea?.Invoke(true);
            }
            
            

            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("PutBullet"))
            {
                PlayerSignals.Instance.onPlayerExitFromPutBulletArea?.Invoke();
            }

            if (other.gameObject.CompareTag("Door"))
            {
                var door = other.gameObject.transform.GetChild(0);
                door.transform.DOLocalRotate(new Vector3(0, 0, 0), 1);
            }
        }

        
    }
}