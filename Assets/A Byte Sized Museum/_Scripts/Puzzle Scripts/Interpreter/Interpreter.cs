using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

                [Require(allow player to pickup shit in game and store it in inventory)]
                - give block
                    > field for what item to give
                    > when interpreter reads, Instantiate the item set to give block
                        > make it launch from the output panel or something, idk

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

    /* TODO: pickup-able items
                > allow players to pick up certain items and store them in their inventory
                > 

    */

    public class Interpreter : MonoBehaviour
    {
        [Header("Interpreter Lines")]
        public List<InterpreterLine> interpreterLines;

        [Header("Game Events")]
        public GameEvent onInterpreterClose;

        /*
            if execute is pressed:
                close interpreter
                execute blocks after a delay?
                    (use Coroutines?)
        */
        public void ExecuteLines()
        {
            StartCoroutine(ExecuteAllLines());
        }

        private IEnumerator ExecuteAllLines()
        {
            foreach (var interpreterLine in interpreterLines)
            {
                interpreterLine.EnableHighlight();

                CodeBlock codeBlock = interpreterLine.GetComponentInChildren<CodeBlock>();

                if (codeBlock == null)
                {
                    yield return new WaitForSeconds(0.25f);
                    interpreterLine.DisableHighlight();
                    continue;
                }
                // ExecuteBlock() should be coroutines
                yield return StartCoroutine(codeBlock.ExecuteBlock());

                interpreterLine.DisableHighlight();
            }
        }

        public void CloseInterpreter()
        {
            onInterpreterClose.Raise(this, name);
        }
    }
}
