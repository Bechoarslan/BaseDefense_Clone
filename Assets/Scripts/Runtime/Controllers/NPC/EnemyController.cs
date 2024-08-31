using Runtime.Abstract;
using Runtime.Enums;
using Runtime.Enums.NPC;
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
        [SerializeField] private Animator animator;
 
        

        #endregion


        #region Private Variables
        
        private NPCAnimType _lastAnimType;
        #endregion

        #endregion
        
        public void Move(Vector3 targetPosition)
        { 
            navMeshAgent.SetDestination(targetPosition);
            
        }

        public void ChangeAnimationState(NPCAnimType npcAnimType,bool condition)
        {
            
            animator.SetBool(npcAnimType.ToString(), condition);
        }
    }
}