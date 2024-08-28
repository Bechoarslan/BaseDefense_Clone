using UnityEngine;

namespace Runtime.Abstract
{
    public interface IStateMachine
    {
        public void Spawn();
        public void Move(Vector3 targetPosition);
    }
}