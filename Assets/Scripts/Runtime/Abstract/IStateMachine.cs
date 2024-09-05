using System;
using Runtime.Enums.NPC;
using UnityEngine;

namespace Runtime.Abstract
{
    public interface IStateMachine
    {
        public void UpdateState();

        public void ChangeState(Enum stateType);



    }
}