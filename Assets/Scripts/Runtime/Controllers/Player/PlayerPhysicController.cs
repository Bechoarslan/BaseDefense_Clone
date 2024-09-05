using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Runtime.Abstract;
using Runtime.Controllers.NPC;
using Runtime.Controllers.NPC.Enemy;
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

        [SerializeField] private Collider playerCollider;

        #endregion

        #region Private Variables
        
        [SerializeField]private List<GameObject> enemyThatCollided = new List<GameObject>();
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
                foreach (var enemy in enemyThatCollided)
                {
                    enemy.GetComponent<IStateMachine>().ChangeState(EnemyAnimationState.Idle);
                    DOVirtual.DelayedCall(1.5F, () =>
                    {
                        enemy.GetComponent<IStateMachine>().ChangeState(EnemyAnimationState.Walk);
                        enemyThatCollided.Remove(enemy);
                    });
                }

                
                
            }


          
            

            
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("EnemyRadar"))
            {
                if (_isPlayerInSafeArea) return;
                other.gameObject.transform.parent.GetComponent<EnemyController>().ChangeState(EnemyStateType.Run);
                if(enemyThatCollided.Contains(other.gameObject.transform.parent.gameObject)) return;
                enemyThatCollided.Add(other.gameObject.transform.parent.gameObject);
                
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