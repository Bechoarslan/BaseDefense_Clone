using Runtime.Enums.Player;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator playerAnimator;

        #endregion

        #region Private Variables

        

        #endregion

        #endregion


        public void OnSendAnimationSpeed(float speed)
        {
            playerAnimator.SetFloat("Speed",speed);
        }


        public void OnSetBoolAnimation(PlayerAnimationEnum state,bool condition)
        {
            playerAnimator.SetBool(state.ToString(),condition);
        }

        public void OnChangeLayer(bool condition)
        {
            playerAnimator.SetLayerWeight(1, !condition ? 1 : 0);
        }
    }
}