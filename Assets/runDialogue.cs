using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaChow.AByteSizedMuseum;

namespace KaChow.AByteSizedMuseum
{
    public class runDialogue : MonoBehaviour
    {
        public Dialogue dialogue;
        public GameObject Input_Manager;
        // Start is called before the first frame update

        void Start()
        {
                TriggerDialogue();
                Input_Manager.SetActive(false);
       

        }

        public void TriggerDialogue()
        {
            FindObjectOfType<dialogmanager>().Start();
            FindObjectOfType<dialogmanager>().StartDialogue(dialogue);

        }


        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
