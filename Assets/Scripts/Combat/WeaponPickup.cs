using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

namespace RPG.Combat {
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] Weapon weapon = null;
        [SerializeField] float respawnTime = 5f;

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.tag == "Player")
            {
                Pickup(other.GetComponent<Fighter>());
            }
        }

        private void Pickup(Fighter fighter)
        {
            fighter.EquipWeapon(weapon);
            StartCoroutine(HideForSeconds(respawnTime));
        }

        private IEnumerator HideForSeconds(float seconds) {
            ShowPickup(false);

            yield return new WaitForSeconds(seconds);

            ShowPickup(true);
        }

        void ShowPickup(bool shouldShow) {
            GetComponent<Collider>().enabled = shouldShow;

            foreach(Transform child in transform){
                child.gameObject.SetActive(shouldShow);
            }
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0)) {
                Pickup(callingController.GetComponent<Fighter>());
                return true;
            }

            return false;
        }
    }
}
