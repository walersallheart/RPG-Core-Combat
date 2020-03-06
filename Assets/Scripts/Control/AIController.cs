using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;

namespace RPG.Control {
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        Fighter fighter;
        GameObject player;

        private void Start() {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
        }

        private void Update() {
            if (InAttackRange() && fighter.CanAttack(player)){
                Debug.Log(gameObject.name + "Should chast");
                fighter.Attack(player);
            } else {
                fighter.Cancel();
            }
        }

        bool InAttackRange() {
            float distanceToPlayer =  Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }
    }
}