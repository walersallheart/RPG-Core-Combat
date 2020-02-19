using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control {
    public class PlayerController : MonoBehaviour
    {
        private void Update() {
            InteractWithCombat();
            InteractWithMovement();
        }

        void InteractWithCombat(){
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (Input.GetMouseButtonDown(0)) {
                    GetComponent<Fighter>().Attack(target);
                }
            }
        }

        void InteractWithMovement(){
            if (Input.GetMouseButton(0)) {
                MoveToCursor();
            }
        }

        private Ray GetMouseRay(){
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        private void MoveToCursor(){
            Ray ray = GetMouseRay();
            RaycastHit hit;

            bool hasHit = Physics.Raycast(ray, out hit);

            if (hasHit)
            {
                GetComponent<Mover>().MoveTo(hit.point);
            }
        }
    }
}
