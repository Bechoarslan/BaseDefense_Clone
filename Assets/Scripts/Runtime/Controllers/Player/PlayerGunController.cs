using Runtime.Data.UnityObject;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerGunController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject gunObject;

        #endregion

        #region Private Variables

        private CD_PlayerData _playerData;
        

        #endregion


        #endregion

        public void GetPlayerData(CD_PlayerData playerData)
        {
            _playerData = playerData;
        }
    }
}