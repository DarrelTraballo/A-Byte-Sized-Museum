using System.Collections;
using UnityEngine;
using TMPro;

namespace KaChow.AByteSizedMuseum
{
    public class LoopBlock : CodeBlock
    {
        /*
            initially, wala yung holder ng method block sa bottom right panel
                > only appears when method block is in "edit mode"
                    - basically whenever the method block is clicked.

            white execution indicator stops on this block's line
            execution indicator appears on Method Block Lines UI, executes all lines inside method block
            then continues executing the other lines of interpreter
        */

        [SerializeField] private TMP_Text counterText;
        [SerializeField] private int counter;

        public override void Start()
        {
            base.Start();
            UpdateCounterText();
        }

        public override IEnumerator ExecuteBlock(int botID)
        {
            while (counter > 0)
            {
                counter--;
                UpdateCounterText();

                foreach (var interpreterLine in Interpreter.Instance.interpreterLines)
                {
                    if (counter == 0) break;

                    interpreterLine.EnableHighlight();

                    CodeBlock codeBlock = interpreterLine.GetComponentInChildren<CodeBlock>();

                    if (codeBlock == null)
                    {
                        yield return new WaitForSeconds(Interpreter.Instance.executeTime);
                        interpreterLine.DisableHighlight();
                        continue;
                    }

                    yield return StartCoroutine(codeBlock.ExecuteBlock(botID));
                    interpreterLine.DisableHighlight();
                    Debug.Log("End of foreach");
                }
            }
        }

        private void UpdateCounterText()
        {
            if (counterText == null)
            {
                Debug.LogError("Counter Text obj is not assigned");
            }

            counterText.text = counter.ToString();
        }
    }
}
