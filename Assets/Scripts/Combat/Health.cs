using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat {
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;

        bool isDead = false;

        public bool IsDead(){
            return isDead;
        }

        public void TakeDamage(float damage) {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            Debug.Log(healthPoints);

            if (healthPoints <= 0) {
                Die();
            }
        }

        void Die(){
            if (isDead) { return; }

            isDead = true;

            GetComponent<Animator>().SetTrigger("die");
        }
    }
}
