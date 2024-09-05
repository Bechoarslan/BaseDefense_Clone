using System;
using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Enums.NPC;
using Runtime.Extentions;
using UnityEngine;

namespace Runtime.Signals
{
    public class NPCSignals : MonoSingleton<NPCSignals>
    {
        public Func<NPCTypes,List<Transform>> onSendWalkTransformToNpc  = delegate { return null; };
        public Func<Vector3> onSendFirstWalkPointTOEnemy = delegate { return Vector3.zero; }; 

    }
}