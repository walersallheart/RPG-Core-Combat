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
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointDwellTime = 3f;

        Fighter fighter;
        Health health;
        Mover mover;
        GameObject player;

        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        int currentWaypointIndex = 0;

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
                AttackBehavior();
            } else if (timeSinceLastSawPlayer < suspicionTime) {
                SuspicionBehavior();
            } else {
                PatrolBehavior();
            }

            UpdateTimers();
        }

        void UpdateTimers(){
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        void PatrolBehavior(){
            Vector3 nextPosition = guardPosition;

            if (patrolPath != null) {
                if (AtWaypoint()) {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            if (timeSinceArrivedAtWaypoint > waypointDwellTime) {
                mover.StartMoveAction(nextPosition);
            }
        }

        bool AtWaypoint(){
            float distanceToWaypoint =  Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        void CycleWaypoint(){
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        Vector3 GetCurrentWaypoint(){
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        void SuspicionBehavior(){
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        void AttackBehavior(){
            timeSinceLastSawPlayer = 0;
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