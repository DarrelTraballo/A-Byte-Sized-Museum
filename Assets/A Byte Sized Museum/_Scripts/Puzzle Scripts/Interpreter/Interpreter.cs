using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class Interpreter : MonoBehaviour
    {
    /* TODO: Interpreter UI: may line numbers sa left side, similar sa mga code editors (like vscode)
<- these things
    */

    // TODO: make for loop block expandable or something
    //       like if you try to put another code block inside the for loop, it expands (take up the bottom 1-2 InterpreterLines)

    /* TODO: also fucking start working on the easy code blocks or something
             both UI and functionality
                - variable block
                    > fields for variable name and value
                    > when interpreter reads, just Debug.Log() name and value for now

                [RequireShit(allow player to pickup shit in game and store it in inventory)]
                - give block
                    > field for what item to give
                    > when interpreter reads, Instantiate the item set to give block
                        > make it launch from the output panel or something, idk

                [RequireShit(objects for it to control)]
                - move block
                    > no parameters
                    > simply moves the puzzle object in the room a set amount of units
                        > should be only tied to that object in the current room, it should not alter objects outside the current room

                - rotate block
                    > one parameter (clockwise / counterclockwise)
                        > drop-down maybe?
                    > simply rotates the puzzle object clock/counterclockwise

    */

    /* TODO: pickup-able items
                > allow players to pick up certain items and store them in their inventory

    */
        public List<InterpreterLine> interpreterLines;

        public void ExecuteCodeBlocks()
        {
            Debug.Log("Execute Button Pressed");
            foreach (var interpreterLine in interpreterLines)
            {
                CodeBlock codeBlock = interpreterLine.GetComponentInChildren<CodeBlock>();

                if (codeBlock == null)
                    return;
                else
                {
                    codeBlock.ExecuteBlock();
                }
            }
        }
    }
}
