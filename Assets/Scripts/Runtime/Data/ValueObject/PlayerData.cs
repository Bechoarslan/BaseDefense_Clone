using System;
using System.Collections.Generic;
using Runtime.Enums.Player;
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
        
        
        [Header("Gun Data")]
        public List<GunData> GunDatas;
       

    }


    
    [Serializable]
    public struct GunData
    {
        public GunTypes gunType;
        public Mesh GunMeshes;
    }
    
}