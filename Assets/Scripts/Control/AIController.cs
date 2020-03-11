using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control {
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 3f;

        Fighter fighter;
        Health health;
        Mover mover;
        GameObject player;

        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;

        private void Start() {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            player = GameObject.FindWithTag("Player");

            guardPosition = transform.position;
        }

        private void Update() {
            if (health.IsDead()) { return; }

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player)){
                timeSinceLastSawPlayer = 0;
                AttackBehavior();
            } else if (timeSinceLastSawPlayer < suspicionTime) {
                SuspicionBehavior();
            } else {
                GuardBehavior();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        void GuardBehavior(){
            mover.StartMoveAction(guardPosition);
        }

        void SuspicionBehavior(){
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        void AttackBehavior(){
            fighter.Attack(player);
        }

        bool InAttackRangeOfPlayer() {
            float distanceToPlayer =  Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position,chaseDistance);
        }
    }
}