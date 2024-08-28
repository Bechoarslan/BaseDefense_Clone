using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_PlayerData", menuName = "BaseDefense/CD_PlayerData", order = 0)]
    public class CD_PlayerData : ScriptableObject
    {
        public PlayerData Data;
    }
}