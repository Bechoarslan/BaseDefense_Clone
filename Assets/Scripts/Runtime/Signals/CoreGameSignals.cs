using Runtime.Extentions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction onLoadLevel = delegate { };
        
        public UnityAction onStopTurretFire = delegate { };
        public UnityAction onStartTurretFire = delegate { };
        
    }
}