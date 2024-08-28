using System;
using Runtime.Enums.Player;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        

        #endregion

        #region Private Variables

        

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

            if (other.gameObject.CompareTag("Gun"))
            {
                PlayerSignals.Instance.onChangePlayerMovementState?.Invoke(PlayerMovementState.Turret);
                PlayerSignals.Instance.onPlayerInteractedWithGunArea?.Invoke(other.gameObject.transform);
                
            }

            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("PutBullet"))
            {
                PlayerSignals.Instance.onPlayerExitFromPutBulletArea?.Invoke();
            }
        }
    }
}