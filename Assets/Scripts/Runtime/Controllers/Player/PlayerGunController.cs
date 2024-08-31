using Runtime.Data.UnityObject;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Controllers.Player
{
    public class PlayerGunController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

       [SerializeField] private GameObject gunObjectMesh;
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

        public void OnChangeGunActive(bool condition)
        {
            gunObject.SetActive(!condition);
        }
    }
}