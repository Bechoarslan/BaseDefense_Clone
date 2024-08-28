using System;
using Cinemachine;
using Runtime.Enums.UI;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineStateDrivenCamera cinemachineStateDrivenCamera;
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreUISignals.Instance.onOpenPanel += OnOpenPanel;
        }

        private void OnOpenPanel(UIPanelTypes panelTypes)
        {
            if(panelTypes == UIPanelTypes.GamePanel)
            {
                var player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    cinemachineStateDrivenCamera.Follow = player.transform;
                    cinemachineStateDrivenCamera.LookAt = player.transform;
                    Debug.LogWarning("<color=green>Camera is following the player</color>");
                }
                else
                {
                    Debug.LogWarning("<color=red>Player is not found</color>");
                }
            }
            else
            {
                cinemachineStateDrivenCamera.Follow =null;
                cinemachineStateDrivenCamera.LookAt = null;
            }
        }

        private void UnSubscribeEvents()
        {
            CoreUISignals.Instance.onOpenPanel -= OnOpenPanel;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}