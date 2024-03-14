using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{

    public class Collisionscript : MonoBehaviour
    {
        public GameObject DialogueContainer2;
       // public GameObject Block;

        private GameManager gameManager;
         private bool hasEntered = false;

        private void Start()
        {
            gameManager = GameManager.Instance;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Tutorialbox")
            {
             //   Block.SetActive(true);
                if(hasEntered == false)
                {
                    
                    DialogueContainer2.SetActive(true);
                    gameManager.SetGameState(GameState.RunDialog);
                    print("Enter");
                    hasEntered = true;
                }
              
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
                //Block.SetActive(false);
            }
        }


    }
}
