﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] float speed = 1f;

        void Update()
        {
            if (transform == null) return;

            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private Vector3 GetAimLocation(){
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null) {
                return target.position;
            }
            return target.position + Vector3.up * targetCapsule.height / 2;
        }
    }
}
