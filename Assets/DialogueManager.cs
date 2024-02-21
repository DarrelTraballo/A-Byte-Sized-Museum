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
        private int count = 0;
        public int counter;
        public Animator animator;
        public Queue<string> sentences = new Queue<string>();
        


        // Start is called before the first frame update
        public void Start()
        {
            
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
            animator.SetBool("IsOpen", false);
            Debug.Log("End of conversation");
        }



    }
}
