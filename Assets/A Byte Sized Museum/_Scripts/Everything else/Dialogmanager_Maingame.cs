using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

namespace KaChow.AByteSizedMuseum
{
    public class Dialogmanager_Maingame : MonoBehaviour
    {
        public static Dialogmanager_Maingame Instance { get; private set; }
        private Dialogmanager_Maingame() { }

        public TMP_Text nameText;
        public TMP_Text dialogueText;
        public GameObject DialogueContainer;
        public Button button;
        public Animator animator;
        public Queue<string> sentences = new Queue<string>();
        public int sentences_count;

        private GameManager gameManager;
        private InputManager inputManager;

        int index;

        public bool isDialogFinished = false;

        private Dialogue previousDialogue;
        [SerializeField] private DialogueSO[] dialogues;

        private void Awake()
        {
            if (Instance != this && Instance != null)
                Destroy(this);
            else
                Instance = this;
        }

        public void Start()
        {
            gameManager = GameManager.Instance;
            inputManager = InputManager.Instance;


        }

        public void LoadDialogues()
        {
            if (dialogues != null)
            {
                foreach (var dialogue in dialogues)
                {
                    dialogue.LoadDialogueFromFile();
                }
            }
        }

        void Update()
        {
            if (gameManager.currentState == GameState.Playing && Input.GetKeyDown(KeyCode.L))
            {
                if (previousDialogue == null) return;
                StartDialogue(previousDialogue);
            }

            if (gameManager.currentState != GameState.RunDialog) return;

            if (inputManager.PlayerInteractedThisFrame())
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

        public void RunDialog(DialogueSO dialogueSO)
        {
            isDialogFinished = false;
            // int selectedIndex = Random.Range(0, dialogueSO.Length);
            // dialogueSO.LoadDialogueFromFile();
            DialogueContainer.SetActive(true);
            previousDialogue = dialogueSO.dialogue;
            StartDialogue(dialogueSO.dialogue);
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
            gameManager.SetGameState(GameState.RunDialog);
            DialogueContainer.SetActive(true);
            // sentences_count = sentences.Count + 1;

            animator.SetBool("IsOpen", false);
            animator.SetBool("IsOpen", true);

            nameText.text = dialogue.name;

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
            yield return new WaitForSeconds(0.001f);
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(0.001f);
            }

        }
        void EndDialogue()
        {
            isDialogFinished = true;
            AudioManager.Instance.sfxSource.Stop();
            // firstrun = false;
            ResetData();
            animator.SetBool("IsOpen", false);
            //DialogueContainer.SetActive(false);

            gameManager.SetGameState(GameState.Playing);
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
