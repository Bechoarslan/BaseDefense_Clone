using Runtime.Extentions;
using Runtime.Keys;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class InputSignals : MonoSingleton<InputSignals>
    {
        public UnityAction<InputParams> onSendInputParams;
       public UnityAction<bool> onChangeVisibilityOfJoustick = delegate { };
       public UnityAction<bool> onInputFindJoystick = delegate { };
    }
}