using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{

    public class Collisionscript : MonoBehaviour
    {
        public GameObject DialogueContainer2;
        public GameObject LoadingScreenCanvas;

        private GameManager gameManager;

        private void Start()
        {
            gameManager = GameManager.Instance;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Tutorialbox")
            {
                DialogueContainer2.SetActive(true);
                gameManager.SetGameState(GameState.RunDialog);
                print("Enter");
            }

        }

        void OnTriggerStay(Collider other)
        {

            if (other.gameObject.tag == "Tutorialbox")
            {
                print("Stay");
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Tutorialbox")
            {
                DialogueContainer2.SetActive(false);
                print("Exit");

                FindObjectOfType<LoadingSceneScript>().LoadScene(3);
                LoadingScreenCanvas.SetActive(true);

            }
        }


    }
}
