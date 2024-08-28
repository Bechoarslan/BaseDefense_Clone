using System;
using System.Collections.Generic;
using Runtime.Abstract;
using Runtime.Enums;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Managers
{
    public class NPCManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<TransformValues> npcTransforms;
        [SerializeField] private Transform npcHolder;

        #endregion

        #region Private Variables

        

        #endregion

        #endregion

        [Button("Spawn Enemy")]
        private void OnSpawnEnemy()
        {
            var enemyObj = PoolSignals.Instance.onGetPoolObject?.Invoke(1,PoolTypes.Enemy,transform);
            enemyObj[0].gameObject.SetActive(true);
            enemyObj[0].GetComponent<IStateMachine>().Spawn();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            NPCSignals.Instance.onSendSpawnPosToNPC += OnSendTransformOfNpc;
            NPCSignals.Instance.onSendWalkTransformToNpc += OnSendWalkTransformOfNpc;
        }

        private List<Transform> OnSendWalkTransformOfNpc(NPCTypes type)
        {
            return npcTransforms[(int)type].walkTransforms;
        }

        private List<Transform> OnSendTransformOfNpc(NPCTypes type)
        {
            return npcTransforms[(int)type].spawnTransforms;
        }

        private void UnSubscribeEvents()
        {
            NPCSignals.Instance.onSendSpawnPosToNPC -= OnSendTransformOfNpc;
            NPCSignals.Instance.onSendWalkTransformToNpc -= OnSendWalkTransformOfNpc;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }

    [Serializable]
    public struct TransformValues
    {
        public List<Transform> spawnTransforms;
        public List<Transform> walkTransforms;
        public NPCTypes type;
    }
}