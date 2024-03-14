using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{

    public class loadscenecollider : MonoBehaviour
    {
        public GameObject LoadingScreenCanvas;

        private GameManager gameManager;

        private void Start()
        {
            gameManager = GameManager.Instance;
        }

        void OnTriggerEnter(Collider other)
        {
                FindObjectOfType<LoadingSceneScript>().LoadScene(3);
                LoadingScreenCanvas.SetActive(true);
        }

        void OnTriggerStay(Collider other)
        {

        }

        void OnTriggerExit(Collider other)
        {

        }


    }
}
