using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_NPCData", menuName = "BaseDefense/CD_NPCData", order = 0)]
    public class CD_NPCData : ScriptableObject
    {
        public NPCData data;
    }
}