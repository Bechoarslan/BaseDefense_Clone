using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Abstract
{
    public class ListOfColliderForNPC : MonoBehaviour
    {
        [SerializeField] private List<Collider> colliders;


        private void OnTriggerEnter(Collider other)
        {
            if (colliders.Contains(other))
            {
                Debug.LogWarning(other.gameObject.name);
                
            }
        }
    }
}