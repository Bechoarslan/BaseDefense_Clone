using System;
using Runtime.Enums.Player;
using Runtime.Enums.UI;
using Runtime.Keys;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.Events;
using Vector2 = System.Numerics.Vector2;

namespace Runtime.Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        

        #endregion


        #region Private Variables

        private FloatingJoystick _joystick;
        #endregion

        #endregion
        
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreUISignals.Instance.onOpenPanel += OnOpenPanel;
            InputSignals.Instance.onChangeVisibilityOfJoustick += OnChangeVisibilityOfJoustick;
            CoreGameSignals.Instance.onSpawnNpcs += OnSpawnNpcs;
        }

        private void OnSpawnNpcs()
        {
            _joystick = FindObjectOfType<FloatingJoystick>();
        }


        private void OnChangeVisibilityOfJoustick(bool condition)
        {
            _joystick.transform.GetChild(0).gameObject.SetActive(condition);
        }
        
        private void OnOpenPanel(UIPanelTypes panelType)
        {
            
            if (panelType == UIPanelTypes.StartPanel)
            {

                _joystick = null;
            }
           
        }
        

        private void UnSubscribeEvents()
        {
            CoreUISignals.Instance.onOpenPanel -= OnOpenPanel;
            InputSignals.Instance.onChangeVisibilityOfJoustick -= OnChangeVisibilityOfJoustick;
        }

        

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void Update()
        {
            if (_joystick == null) return;
            
            InputSignals.Instance.onSendInputParams?.Invoke(new InputParams
            {
                        
                MoveParams = new UnityEngine.Vector2(_joystick.Horizontal,_joystick.Vertical)
            });

                    
                
                
            
           
           
            
        }
    }
}
