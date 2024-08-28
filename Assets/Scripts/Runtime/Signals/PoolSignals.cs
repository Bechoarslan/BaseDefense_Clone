using System;
using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class PoolSignals : MonoSingleton<PoolSignals>
    {
        public Func<int,PoolTypes,Transform,List<GameObject>> onGetPoolObject =  delegate { return null; };
        public UnityAction<GameObject,PoolTypes> onSendPoolObject = delegate {  };
        
    }
}