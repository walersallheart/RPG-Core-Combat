﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat {
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;
        [SerializeField] float respawnTime = 5f;

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.tag == "Player") {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                StartCoroutine(HideForSeconds(respawnTime));
            }
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
    }
}
