using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class Collisionscript : MonoBehaviour
    {
        public GameObject DialogueContainer2;
        void OnTriggerEnter(Collider other)  
        {
            if (other.gameObject.tag == "Tutorialbox")
            {
                DialogueContainer2.SetActive(true);
                print("Enter");
            }
            
        }

        void OnTriggerStay(Collider other) {
            
             if (other.gameObject.tag == "Tutorialbox")
            {
                print("Stay");
            }
        }

        void OnTriggerExit(Collider other) {
             if (other.gameObject.tag == "Tutorialbox")
            {
                DialogueContainer2.SetActive(false);
                print("Exit");
            }
        }


    }
}
