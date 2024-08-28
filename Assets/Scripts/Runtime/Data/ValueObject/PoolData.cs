using System;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Data.ValueObject
{
    [Serializable]
    public struct PoolData
    {
        public PoolTypes poolType;
        public GameObject prefabs;
        public int poolSize;
    }
}