using System;
using Runtime.Controllers.Player;
using Runtime.Data.UnityObject;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private PlayerStackController playerStackController;
        [SerializeField] private PlayerAnimationController playerAnimationController;
        [SerializeField] private PlayerGunController playerGunController;


        #endregion

        #region Private Variables
        
        private CD_PlayerData _playerData;
        private bool _isPlayerInSafeArea = true;

        #endregion



        #endregion


        private void Awake()
        {
            _playerData = GetPlayerData();
            SendDataToListeners();
        }

        private void SendDataToListeners()
        {
            playerMovementController.GetPlayerData(_playerData);
            playerStackController.GetPlayerData(_playerData);
            playerGunController.GetPlayerData(_playerData);
        }

        private CD_PlayerData GetPlayerData() => Resources.Load<CD_PlayerData>("Data/CD_PlayerData");
        

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onSendInputParams += playerMovementController.OnMovement;
            PlayerSignals.Instance.onPlayerInteractedWithBuyBulletArea += playerStackController.OnPlayerInteractedWithBuyBulletArea;
            PlayerSignals.Instance.onPlayerInteractedWithPutBulletArea += playerStackController.OnPlayerInteractedWithPutBulletArea;
            PlayerSignals.Instance.onPlayerExitFromPutBulletArea += playerStackController.OnPlayerExitFromPutBulletArea;
            PlayerSignals.Instance.onPlayerInteractedWithGunArea += playerMovementController.OnPlayerInteractedWithGunArea;
            PlayerSignals.Instance.onChangePlayerMovementState += playerMovementController.OnChangePlayerMovementState;
            PlayerSignals.Instance.onSendAnimationSpeed += playerAnimationController.OnSendAnimationSpeed;
            PlayerSignals.Instance.onSetBoolAnimation += playerAnimationController.OnSetBoolAnimation;
            PlayerSignals.Instance.onIsPlayerInSafeArea += OnIsPlayerInSafeArea;
            PlayerSignals.Instance.onCheckIsPlayerInSafeArea += CheckIsPlayerInSafeArea;
        }

        private bool CheckIsPlayerInSafeArea()
        {
            return _isPlayerInSafeArea;
        }

        private void OnIsPlayerInSafeArea(bool condition)
        {
            _isPlayerInSafeArea = condition;
            playerAnimationController.OnChangeLayer(condition);
            playerGunController.OnChangeGunActive(condition);
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onSendInputParams -= playerMovementController.OnMovement;
            PlayerSignals.Instance.onPlayerInteractedWithBuyBulletArea -= playerStackController.OnPlayerInteractedWithBuyBulletArea;
            PlayerSignals.Instance.onPlayerInteractedWithPutBulletArea -= playerStackController.OnPlayerInteractedWithPutBulletArea;
            PlayerSignals.Instance.onPlayerExitFromPutBulletArea -= playerStackController.OnPlayerExitFromPutBulletArea;
            PlayerSignals.Instance.onPlayerInteractedWithGunArea -= playerMovementController.OnPlayerInteractedWithGunArea;
            PlayerSignals.Instance.onChangePlayerMovementState -= playerMovementController.OnChangePlayerMovementState;
            PlayerSignals.Instance.onSendAnimationSpeed -= playerAnimationController.OnSendAnimationSpeed;
            PlayerSignals.Instance.onSetBoolAnimation -= playerAnimationController.OnSetBoolAnimation;
            PlayerSignals.Instance.onIsPlayerInSafeArea -= OnIsPlayerInSafeArea;
            PlayerSignals.Instance.onCheckIsPlayerInSafeArea -= CheckIsPlayerInSafeArea;
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}