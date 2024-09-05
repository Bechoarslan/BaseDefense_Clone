using System;
using UnityEngine;

namespace Runtime.Controllers.NPC.Enemy
{
    
    public class EnemyPhysicController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player is in the enemy's area");
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player is out of the enemy's area");
            }
        }
    }
}