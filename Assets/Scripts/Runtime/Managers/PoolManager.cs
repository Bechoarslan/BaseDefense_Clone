using System;
using System.Collections.Generic;
using Runtime.Data.UnityObject;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class PoolManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Transform poolHolder;

        #endregion

        #region Private Variables
        
        private CD_PoolData _poolData;

        #endregion

        #endregion


        public void Awake()
        {
           _poolData = GetPoolData();
            CreatePoolObjects();
        }

        private CD_PoolData GetPoolData() => Resources.Load<CD_PoolData>("Data/CD_PoolData");
        
        private void CreatePoolObjects()
        {
            foreach (var poolObj in _poolData.Data)
            {
                GameObject poolParent = new GameObject(poolObj.poolType.ToString());
                poolParent.transform.parent = poolHolder;
                for (int i = 0; i < poolObj.poolSize; i++)
                {
                    if(poolObj.prefabs == null) continue;
                    var obj = Instantiate(poolObj.prefabs, Vector3.zero,Quaternion.identity,poolParent.transform);
                    obj.SetActive(false);
                    obj.transform.parent = poolParent.transform;
                    
                }
                
            }
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PoolSignals.Instance.onGetPoolObject += OnGetPoolObject;
            PoolSignals.Instance.onSendPoolObject += OnSendPoolObject;
        }

        private void OnSendPoolObject(GameObject poolObj,PoolTypes poolType)
        {
            poolObj.transform.parent = poolHolder.GetChild((int)poolType);
            poolObj.SetActive(false);
            
        }

        private List<GameObject> OnGetPoolObject(int objCount, PoolTypes poolType,Transform newParent)
        {
            var poolParent = poolHolder.GetChild((int)poolType);
            var newPoolList = new List<GameObject>();
            
            
                
                for (int i = 0; i < objCount; i++)
                {
                    if (poolParent.childCount == 0)
                    {
                        
                        var obj = Instantiate(_poolData.Data[(int)poolType].prefabs, Vector3.zero,Quaternion.identity,poolParent);
                        obj.SetActive(false);
                        obj.transform.parent = newParent;
                        newPoolList.Add(obj);
                    }
                    else
                    {
                        var obj = poolParent.GetChild(0).gameObject;
                        obj.transform.parent = newParent;
                        newPoolList.Add(obj);
                    }
                    
                    
                }
            
            

            return newPoolList;
        }

        
        

        private void UnSubscribeEvents()
        {
            PoolSignals.Instance.onGetPoolObject -= OnGetPoolObject;
            PoolSignals.Instance.onSendPoolObject -= OnSendPoolObject;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}