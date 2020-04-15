using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat{
    public class Projectile : MonoBehaviour
    {
        Health target;
        [SerializeField] float speed = 1f;

        public void SetTarget(Health target) {
            this.target = target;
        }

        void Update()
        {
            if (transform == null) return;

            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private Vector3 GetAimLocation(){
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null) {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }
    }
}
