using Runtime.Abstract;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Controllers.NPC
{
    public class EnemyController : MonoBehaviour, IStateMachine
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private NavMeshAgent navMeshAgent;

        #endregion


        #region Private Variables

        

        #endregion

        #endregion
        public void Spawn()
        {
            var spawnTransform = NPCSignals.Instance.onSendSpawnPosToNPC?.Invoke(NPCTypes.Enemy);
            var randomTransform = spawnTransform[Random.Range(0, spawnTransform.Count)];
            navMeshAgent.Warp(randomTransform.position);
            
            
            var walkTransform = NPCSignals.Instance.onSendWalkTransformToNpc?.Invoke(NPCTypes.Enemy);
            var randomPos =
                Random.Range(walkTransform[0].transform.localPosition.x, walkTransform[1].transform.localPosition.x);
            var walkPos = new Vector3(randomPos, transform.position.y, walkTransform[0].transform.localPosition.z);
            
            Move(walkPos);
        }

        public void Move(Vector3 targetPosition)
        {
            navMeshAgent.SetDestination(targetPosition);
        }
        
    }
}