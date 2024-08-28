using System;
using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Extentions;
using UnityEngine;

namespace Runtime.Signals
{
    public class NPCSignals : MonoSingleton<NPCSignals>
    {
        public Func<NPCTypes, List<Transform>> onSendSpawnPosToNPC = delegate { return null; };
        public Func<NPCTypes,List<Transform>> onSendWalkTransformToNpc  = delegate { return null; };

    }
}