using Runtime.Enums.NPC;
using UnityEngine;

namespace Runtime.Abstract
{
    public interface IStateMachine
    {
        public void Move(Vector3 targetPosition);
        public void ChangeAnimationState(NPCAnimType npcAnimType,bool condition);
    }
}