using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaChow.AByteSizedMuseum;

namespace KaChow.AByteSizedMuseum
{
    public class rundialogue_Main : MonoBehaviour
    {
        public Dialogue dialogue;
        public GameObject Input_Manager;
        private GameManager gameManager;
        // Start is called before the first frame update

        void OnEnable()
        {
            TriggerDialogue();



        }
        void Start()
        {
            gameManager = GameManager.Instance;
    

        }

        public void TriggerDialogue()
        {

            FindObjectOfType<Dialogmanager_Maingame>().Start();
            FindObjectOfType<Dialogmanager_Maingame>().StartDialogue(dialogue);

        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}
