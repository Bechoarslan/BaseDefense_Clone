using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Abstract;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using Runtime.Enums;
using Runtime.Enums.NPC;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Managers
{
    public class NpcManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<TransformValues> npcTransforms;
        [SerializeField] private Transform npcHolder;

        #endregion

        #region Private Variables
        
         private NPCData _npcData;
        #endregion

        #endregion

        private void Awake()
        {
            _npcData = OnGetNPCData();
        }

        private NPCData OnGetNPCData() => Resources.Load<CD_NPCData>("Data/CD_NPCData").data;

        private void OnEnable()
        {
            SubscribeEvents();
            OnSpawnNpcs();
        }

        private void SubscribeEvents()
        {
            NPCSignals.Instance.onSendWalkTransformToNpc += OnSendWalkTransformOfNpc;
            CoreGameSignals.Instance.onSpawnNpcs += OnSpawnNpcs;
        }

        private void OnSpawnNpcs()
        {
            StartCoroutine(SpawnEnemyNpcs());
        }


        private IEnumerator SpawnEnemyNpcs()
        {
            Debug.LogWarning("Started to Spawn Enemies");
            yield return new WaitForSeconds(_npcData.enemySpawnTime);
            var enemyObj = PoolSignals.Instance.onGetPoolObject?.Invoke(_npcData.enemySpawnCount,PoolTypes.Enemy,transform);
            foreach (var enemy in enemyObj)
            {
                var randomTransform = npcTransforms[(int)NPCTypes.Enemy].spawnTransforms[Random.Range(0, npcTransforms[(int)NPCTypes.Enemy].spawnTransforms.Count)];
                var newPos = new Vector3(randomTransform.localPosition.x, enemy.transform.position.y, randomTransform.localPosition.z);
                enemy.transform.localPosition = newPos;
                enemy.SetActive(true);
            }
            
        }

        private List<Transform> OnSendWalkTransformOfNpc(NPCTypes type)
        {
            return npcTransforms[(int)type].walkTransforms;
        }

        

        private void UnSubscribeEvents()
        {
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