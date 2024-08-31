using System;
using UnityEngine;

namespace Runtime.Data.ValueObject
{
    [Serializable]
    public struct NPCData
    {
        [Header("Enemy")]
        public int enemySpawnCount;
        public float enemySpawnTime;


        [Header("Hostile")] 
        public int hostileSpawnCount;


    }
}