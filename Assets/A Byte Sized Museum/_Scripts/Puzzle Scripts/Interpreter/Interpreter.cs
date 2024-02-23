using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KaChow.AByteSizedMuseum
{
    /* TODO: Interpreter UI: may line numbers sa left side, similar sa mga code editors (like vscode)
<- these things

       TODO: make for loop block expandable or something
             like if you try to put another code block inside the for loop, it expands (take up the bottom 1-2 InterpreterLines)
             OR


       TODO: also start working on the easy code blocks or something
             both UI and functionality
                - variable block
                    > fields for variable name and value
                    > when interpreter reads, just Debug.Log() name and value for now

                - Pickup Block
                    > picks up object directly in front of it
                        ```
                        if bot is not holding an object:
                            use raycast to check if object in front of it is pickup-able
                            if raycast.hit == pickup-able object:
                                pickup object
                            else:
                                do nothing
                        else:
                            do nothing
                        ```

                - Drop Block
                    > drops held object directly in front of it
                        ```
                        if bot is holding an object:
                            raycast to check if object in front is occupied
                            if raycast.hit == true:
                                do nothing
                            else:
                                drop object
                        else:
                            do nothing
                        ```

                - Function block
                    > Lets players define a series of commands as a function.
                    > should have another UI for telling what's inside the function block
                    > when clicked, a part of inventory side gets covered by the function block UI
                    > functionality should be very similar to how interpreter reads lines.

                - For Loop Block
                    > very similar functionality to Interpreter.ExecuteAllLines();
    */
    #region MoveBlock Done
    /*
                [Require(objects for it to control)]
                - move block
                    > no parameters
                    > simply moves the puzzle object in the room a set amount of units
                        > should be only tied to that object in the current room, it should not alter objects outside the current room

                - rotate block
                    > one parameter (clockwise / counterclockwise)
                        > drop-down maybe?
                    > simply rotates the puzzle object clock/counterclockwise

                - Lightbot puzzle
                    > establish what type of object is being controlled
                        > figure out how to make interpreter and controlled object communicate with each other

    */
    #endregion

    /* TODO: pickup-able items
                > allow players to pick up certain items and store them in their inventory
                >
       TODO:
             reset button for interpreter
    */

    public class Interpreter : MonoBehaviour
    {
        [Header("Interpreter Lines")]
        public List<InterpreterLine> interpreterLines;

        [Header("Game Events")]
        public GameEvent onInterpreterClose;

        [Header("Bottom Right Panel")]
        [SerializeField] private GameObject codeBlockDetailsPanel;
        [SerializeField] private GameObject puzzleCameraFeed;

        [SerializeField] private GameObject closeIcon;

        [Header("Buttons")]
        [SerializeField] private Button executeButton;
        [SerializeField] private Button clearAllButton;

        [SerializeField] private int interpreterID;

        // [Space]
        // [Header("Debugging")]

        /*
            if execute is pressed:
                close interpreter
                execute blocks after a delay?
                    (use Coroutines?)
        */

        public void ExecuteLines()
        {
            codeBlockDetailsPanel.SetActive(false);
            StartCoroutine(ExecuteAllLines());
        }

        private IEnumerator ExecuteAllLines()
        {
            DisableButton(executeButton);
            DisableButton(clearAllButton);
            foreach (var interpreterLine in interpreterLines)
            {
                interpreterLine.EnableHighlight();

                CodeBlock codeBlock = interpreterLine.GetComponentInChildren<CodeBlock>();

                if (codeBlock == null)
                {
                    yield return new WaitForSeconds(0.20f);
                    interpreterLine.DisableHighlight();
                    continue;
                }

                yield return StartCoroutine(codeBlock.ExecuteBlock(interpreterID));

                interpreterLine.DisableHighlight();
            }
            EnableButton(executeButton);
            EnableButton(clearAllButton);
        }

        public IEnumerator ClearInterpreterLines()
        {
            DisableButton(executeButton);
            DisableButton(clearAllButton);

            foreach (var interpreterLine in interpreterLines)
            {
                interpreterLine.EnableHighlight();

                foreach (Transform child in interpreterLine.transform)
                {
                    Destroy(child.gameObject);
                    // yield return new WaitForSeconds(0.20f);
                }

                yield return new WaitForSeconds(0.20f);

                interpreterLine.DisableHighlight();
            }

            EnableButton(executeButton);
            EnableButton(clearAllButton);
        }

        public void CloseInterpreter()
        {
            // currently does not reset code block execution
            onInterpreterClose.Raise(this, name);

            foreach (var interpreterLine in interpreterLines)
            {
                interpreterLine.DisableHighlight();
            }

            EnableButton(executeButton);
        }

        public void DisableButton(Button button) => button.interactable = false;

        public void EnableButton(Button button) => button.interactable = true;

        public void ShowCodeBlockDetails(Component sender, object data)
        {
            if (data is CodeBlock codeBlock)
            {
                codeBlockDetailsPanel.SetActive(true);
                var codeBlockName = codeBlockDetailsPanel.transform.Find("Code Block Name").GetComponent<TextMeshProUGUI>();
                var codeBlockDescription = codeBlockDetailsPanel.transform.Find("Code Block Description").GetComponent<TextMeshProUGUI>();

                codeBlockName.text = codeBlock.codeBlockName;
                codeBlockDescription.text = codeBlock.codeBlockDescription;

            }
        }

        public void HideCodeBlockDetails()
        {
            codeBlockDetailsPanel.SetActive(false);
        }

        public void ClearInterpreter()
        {
            StartCoroutine(ClearInterpreterLines());
        }




        public void SetInterpreterID(int interpreterID)
        {
            this.interpreterID = interpreterID;
        }
    }
}
