using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace KaChow.AByteSizedMuseum
{
    public class AutoPlayCutscene : MonoBehaviour
    {
        [SerializeField] private PlayableDirector playableDirector;
        private void OnEnable()
        {
            playableDirector.Play();
        }
    }
}
