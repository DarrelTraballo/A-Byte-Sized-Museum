using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KaChow.AByteSizedMuseum
{
    public class dialogmanager : MonoBehaviour
    {

        public TMP_Text nameText;
        public TMP_Text dialogueText;
        public GameObject Input_manager;
        public GameObject DialogueContainer;
        private int count = 0;
        public int counter;
        public Animator animator;
        public Queue<string> sentences = new Queue<string>();
        


        // Start is called before the first frame update
        public void Start()
        {
            DialogueContainer.SetActive(true);
        }

        public void StartDialogue (Dialogue dialogue)
        {

            animator.SetBool("IsOpen", true);
            
            nameText.text = dialogue.name;
            //Debug.Log("Starting conversation with " + dialogue.name);
        
            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            
            DisplayNextSentence();


        }

        public void DisplayNextSentence()
        {
            if (count == counter)
            {
                Input_manager.SetActive(true);
            }
            Debug.Log(count);
            count++;
            
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }
            string sentence = sentences.Dequeue();
           // Debug.Log(sentence);
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

        IEnumerator TypeSentence (String sentence)
        {
            dialogueText.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return null;
            }

        }
        void EndDialogue()
        {
            count = 0;
            animator.SetBool("IsOpen", false);
            //DialogueContainer.SetActive(false);
            ResetData();
            Debug.Log("End of conversation");

        }
        public void ResetData()
        {
            // Reset relevant variables to their initial values
            count = 0;
            sentences.Clear();

            // You may want to reset other variables as needed

            // Additional reset logic...

            // Call the method to reset the UI elements
            ResetUIElements();
        }

        // Method to reset UI elements
        private void ResetUIElements()
        {
            // Reset UI elements to their initial state
            nameText.text = "";
            dialogueText.text = "";
            //Input_manager.SetActive(false);
        }




    }
}
