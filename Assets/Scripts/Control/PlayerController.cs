﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Resources;

namespace RPG.Control {
    public class PlayerController : MonoBehaviour
    {
        Health health;

        private void Awake() {
            health = GetComponent<Health>();
        }
        private void Update() {
            if (health.IsDead()) { return; }
            if (InteractWithCombat()) { return; }
            if (InteractWithMovement()) { return; }
            Debug.Log("Nothing to do");
        }

        bool InteractWithCombat(){
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                if (target == null) { continue; }

                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) {
                    continue;
                }

                if (Input.GetMouseButton(0)) {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }

            return false;
        }

        bool InteractWithMovement(){
            Ray ray = GetMouseRay();
            RaycastHit hit;

            bool hasHit = Physics.Raycast(ray, out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0)) {
                    GetComponent<Mover>().StartMoveAction(hit.point, 1f);
                }

                return true;
            }

            return false;
        }

        private Ray GetMouseRay(){
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
