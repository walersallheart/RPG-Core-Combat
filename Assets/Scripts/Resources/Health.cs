using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using System;

namespace RPG.Resources {
    public class Health : MonoBehaviour, ISaveable
    {
        float healthPoints = -1f;

        bool isDead = false;

        private void Start() {
            if (healthPoints < 0) {
                healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
        }
        public bool IsDead(){
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage) {
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            if (healthPoints <= 0) {
                Die();
                AwardExpereince(instigator);
            }
        }

        public float GetPercentage(){
            return 100 * (healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health));
        }

        void Die(){
            if (isDead) { return; }

            isDead = true;

            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AwardExpereince(GameObject instigator){
            Experience experience = instigator.GetComponent<Experience>();

            if (experience == null) { return; }

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float) state;

            if (healthPoints <= 0) {
                Die();
            }
        }
    }
}
