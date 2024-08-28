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
            Move();
            
            
            
        }

        public void Move()
        {
            var walkTransform = NPCSignals.Instance.onSendWalkTransformToNpc?.Invoke(NPCTypes.Enemy);
            var randomTransform =
                Random.Range(walkTransform[0].transform.localPosition.x, walkTransform[1].transform.localPosition.x);
            navMeshAgent.SetDestination(new Vector3(randomTransform, transform.position.y, walkTransform[0].localPosition.z));

          
            
        }
    }
}