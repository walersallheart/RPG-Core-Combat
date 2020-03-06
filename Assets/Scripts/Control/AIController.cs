using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control {
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        private void Update() {
            if (DistanceToPlayer() < chaseDistance){
                Debug.Log(gameObject.name + "Should chast");
            }
        }

        float DistanceToPlayer() {
            GameObject player = GameObject.FindWithTag("Player");
            return Vector3.Distance(player.transform.position, transform.position);
        }
    }
}