using System;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums.Player;
using Runtime.Keys;
using Runtime.Signals;
using UnityEngine;


namespace Runtime.Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Rigidbody playerRb;
        

        #endregion


        #region Private Variables

        private Vector2 _movementParams;
        private PlayerData _playerData;
        private PlayerMovementState _playerMovementState;

        [Header("Player Interactable Objects")]
        private Transform _turretObj;

        #endregion

        #endregion
        public void OnMovement(InputParams inputParams) => _movementParams = inputParams.MoveParams;
        public void GetPlayerData(CD_PlayerData playerData) => _playerData = playerData.Data;
        
        public void OnPlayerInteractedWithGunArea(Transform turretTransform)
        {
            _turretObj = turretTransform;
            PlayerSignals.Instance.onSetBoolAnimation?.Invoke(PlayerAnimationEnum.Hold,true);
            CoreGameSignals.Instance.onStartTurretFire?.Invoke();
            InputSignals.Instance.onChangeVisibilityOfJoustick?.Invoke(false);
        }

        public void OnChangePlayerMovementState(PlayerMovementState state)
        {
            playerRb.velocity = Vector3.zero;
            _playerMovementState = state;
        }


        private void FixedUpdate()
        {
            switch (_playerMovementState)
            {
                case PlayerMovementState.Move:
                    PlayerMove();
                    break;
                case PlayerMovementState.Turret:
                    PlayerTurretMove();
                    break;
                
            }
            
        }

        private void PlayerTurretMove()
        {
            _movementParams.Normalize();
            if(_movementParams.magnitude < 0.1f) return;
            if (_movementParams.y < -0.85f)
            {
                PlayerSignals.Instance.onChangePlayerMovementState?.Invoke(PlayerMovementState.Move);
                PlayerSignals.Instance.onSetBoolAnimation?.Invoke(PlayerAnimationEnum.Hold,false);
                CoreGameSignals.Instance.onStopTurretFire?.Invoke();
                InputSignals.Instance.onChangeVisibilityOfJoustick?.Invoke(true);
                return;
            }

            var newPosPlayer = new Vector3(_turretObj.GetChild(0).transform.position.x,transform.position.y,_turretObj.GetChild(0).transform.position.z);
            transform.position = newPosPlayer;
            var targetRotation = Quaternion.Euler(0, _movementParams.x * 40, 0);
            _turretObj.rotation = Quaternion.Slerp(_turretObj.rotation, targetRotation, _playerData.RotateSpeed);
            transform.rotation = Quaternion.LookRotation(_turretObj.rotation * Vector3.forward);
        }

        private void PlayerMove()
        {
            if (_movementParams.magnitude > 0.1f)
            {
                _movementParams.Normalize();
                var direction = Vector3.forward * _movementParams.y + Vector3.right * _movementParams.x;

                playerRb.velocity = direction * _playerData.MoveSpeed;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                playerRb.rotation = Quaternion.Slerp(playerRb.rotation, targetRotation, _playerData.RotateSpeed);
                PlayerSignals.Instance.onSendAnimationSpeed?.Invoke(playerRb.velocity.magnitude);
                
            }
            else
            {
                playerRb.velocity = Vector3.zero;
                PlayerSignals.Instance.onSendAnimationSpeed?.Invoke(playerRb.velocity.magnitude);
            }
        }


        
    }
}