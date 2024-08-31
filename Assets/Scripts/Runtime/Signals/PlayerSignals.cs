using System;
using Runtime.Enums.Player;
using Runtime.Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        
        public UnityAction<Transform> onPlayerInteractedWithBuyBulletArea = delegate { };
        public UnityAction<Transform> onPlayerInteractedWithPutBulletArea = delegate { };
        public UnityAction<Transform> onPlayerInteractedWithGunArea = delegate { };
        public UnityAction onPlayerExitFromPutBulletArea = delegate { };
        public UnityAction<PlayerMovementState> onChangePlayerMovementState = delegate { };
        public UnityAction<float> onSendAnimationSpeed = delegate { };
        public UnityAction<PlayerAnimationEnum,bool> onSetBoolAnimation = delegate { };
        public UnityAction<bool> onIsPlayerInSafeArea = delegate { };
        public Func<bool> onCheckIsPlayerInSafeArea = delegate { return false; };
        
    }
}