using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KaChow.AByteSizedMuseum
{
    public class Dialogmanager_Maingame : MonoBehaviour
    {

        public TMP_Text nameText;
        public TMP_Text dialogueText;
        public GameObject Input_manager;
        public GameObject DialogueContainer;
        public Button button;
        private int count = 0;
        public int counter;
        public Animator animator;
        public Queue<string> sentences = new Queue<string>();
        public int sentences_count;

        private GameManager gameManager;

        int index;

        public void Start()
        {
            gameManager = GameManager.Instance;
            DialogueContainer.SetActive(true);
            sentences_count = sentences.Count + 1;


        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {

                if (sentences.Count != 0)
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

            }

        }

        void DelayedCode()
        {
            DialogueContainer.SetActive(true);
        }

        public void Next()
        {
            index += 1;
           
        }

        public void StartDialogue(Dialogue dialogue)
        {
            animator.SetBool("IsOpen", false);
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
            AudioManager.Instance.sfxSource.Stop();
            AudioManager.Instance.PlaySFX("HelperBot");
            gameManager.SetGameState(GameState.RunDialog);

            if (count == counter)
            {
                Input_manager.SetActive(true);
            }
            count++;

            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

        IEnumerator TypeSentence(String sentence)
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
            AudioManager.Instance.sfxSource.Stop();
            // firstrun = false;
            ResetData();
            animator.SetBool("IsOpen", false);
            //DialogueContainer.SetActive(false);

            gameManager.SetGameState(GameState.Playing);
            Debug.Log("End of conversation");


        }
        public void ResetData()
        {
            
            sentences.Clear();
            ResetUIElements();
            DialogueContainer.SetActive(false);
        }

        // Method to reset UI elements
        private void ResetUIElements()
        {
            // Reset UI elements to their initial state
            index = 0;
            nameText.text = "";
            dialogueText.text = "";
            //Input_manager.SetActive(true);
        }




    }
}
