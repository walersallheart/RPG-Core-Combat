using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics{
    public class CinematicControlRemover : MonoBehaviour
    {
        private void Start() {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }

        void DisableControl(PlayableDirector director){
            Debug.Log("DisableControl()");
        }

        void EnableControl(PlayableDirector director){
            Debug.Log("EnableControl()");
        }
    }
}
