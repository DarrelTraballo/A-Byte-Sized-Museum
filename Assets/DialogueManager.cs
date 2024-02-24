using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KaChow.AByteSizedMuseum
{
    public class dialogmanager : MonoBehaviour
    {

        [SerializeField] public bool helperbot_tutorial = false;
        public TMP_Text nameText;
        public TMP_Text dialogueText;
        public GameObject Input_manager;
        public GameObject DialogueContainer;
        public GameObject Canvas_images;
        public  Button button;
        private int count = 0;
        public int counter;
        public Animator animator;
        public Queue<string> sentences = new Queue<string>();
        public int sentences_count;
        public GameObject[] images;

        int index;


        // Start is called before the first frame update
        public void Start()
        {
            DialogueContainer.SetActive(true);
            sentences_count = sentences.Count +1 ;

            if (helperbot_tutorial == true)
            {
                index = 0;
            }
        }

        void Update()
        {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {

            if (sentences.Count != 0 )
            {
                if (button != null)
                    {
                        button.onClick.Invoke();
                    }
            }
            else 
            {
                EndDialogue();
            }
            // Invoke the button click
           
        }
       
        }

        public void Next()
        {
            index +=1; 
            for(int i = 0; i < images.Length; i++)
            {
                images[i].gameObject.SetActive(false);
                images[index].gameObject.SetActive(true);
                Debug.Log(index);
            }
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
                print(sentences.Count);
            }
            if (helperbot_tutorial == true)
            {
                images[0].gameObject.SetActive(true);
            }
            
            DisplayNextSentence();

        }

        public void DisplayNextSentence()
        {
            if (count == counter)
            {
                Input_manager.SetActive(true);
            }
            
            if (helperbot_tutorial == true)
            {
                Next();
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
            ResetData();
            Canvas_images.SetActive(false);
            animator.SetBool("IsOpen", false);
            //DialogueContainer.SetActive(false);
            
            Debug.Log("End of conversation");

        }
        public void ResetData()
        {
            // Reset relevant variables to their initial values
            //count = 0;
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
            Input_manager.SetActive(true);
        }











    }
}
