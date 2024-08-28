using System;
using System.Collections.Generic;

using UnityEngine;

namespace Runtime.Data.ValueObject
{
    [Serializable]
    public struct PlayerData
    {
        [Header("Player Data")]
        public float MoveSpeed;
        public float RotateSpeed;

        [Header("Stack Data")] 
        public int BulletMaxCount;
        public int MoneyMaxCount;
        public List<Vector3> BulletPositions;

    }
}