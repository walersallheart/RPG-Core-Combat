using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats stats;

        private void Awake() {
            stats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }

        private void Update() {
            GetComponent<Text>().text = String.Format("{0:0}", stats.GetLevel());
        }
    }
}
