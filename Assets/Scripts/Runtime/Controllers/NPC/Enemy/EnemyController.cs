using System;
using Runtime.Abstract;
using Runtime.Enums.NPC;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Controllers.NPC.Enemy
{
    public class EnemyController : MonoBehaviour, IStateMachine
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Animator animator;
 
        

        #endregion


        #region Private Variables

        private Vector3 _firstWalkPoint;
        private EnemyStateType _enemyStateType;
        #endregion

        #endregion

        private void OnEnable()
        {
            _firstWalkPoint = (Vector3)NPCSignals.Instance.onSendFirstWalkPointTOEnemy?.Invoke();
            ChangeState(EnemyStateType.Walk);
            
        }

       
       

        public void UpdateState()
        {
            switch (_enemyStateType)
            {
                case EnemyStateType.Walk:
                    if (Vector3.Distance(transform.position, _firstWalkPoint) < 3f)
                    {
                        Debug.LogWarning("Enemy setted to his destination");
                        _enemyStateType = EnemyStateType.Idle;
                        navMeshAgent.isStopped = true;
                        ChangeState(EnemyStateType.Attack);
                        
                    }
                    break;
                case EnemyStateType.Idle:
                    
                    break;
                
                case EnemyStateType.Run:
                    var playerPos = (Vector3)PlayerSignals.Instance.onSendPlayerTransform?.Invoke().position;
                    transform.LookAt(playerPos);
                    navMeshAgent.SetDestination(playerPos);
                    
                    break;
                
            }
        }

        public void ChangeState(Enum stateType)
        {
            _enemyStateType = (EnemyStateType)stateType;
            switch (_enemyStateType)
            {
                case EnemyStateType.Idle:
                    navMeshAgent.isStopped = true;
                    ChangeAnimationState(EnemyAnimationState.Walk,false);
                    break;
                case EnemyStateType.Walk:
                    navMeshAgent.speed = 5f;
                    navMeshAgent.isStopped = false;
                    ChangeAnimationState(EnemyAnimationState.Walk,true);
                    navMeshAgent.SetDestination(_firstWalkPoint);
                    
                    break;
                case EnemyStateType.Run:
                    ChangeAnimationState(EnemyAnimationState.Walk,true);
                    navMeshAgent.isStopped = false;
                    navMeshAgent.speed = 6f;
                    
                    break;
                case EnemyStateType.Attack:
                    navMeshAgent.isStopped = true;
                    SetTriggerAnimation(EnemyAnimationState.Attack);
                    break;
                
            }
        }

        public void ChangeAnimationState(EnemyAnimationState animationState,bool condition)
        {
            animator.SetBool(animationState.ToString(), condition);
            
        }
        
        public void SetTriggerAnimation(EnemyAnimationState animationState)
        {
            animator.SetTrigger(animationState.ToString());
        }

        private void Update()
        {
            UpdateState();
        }
    }
}