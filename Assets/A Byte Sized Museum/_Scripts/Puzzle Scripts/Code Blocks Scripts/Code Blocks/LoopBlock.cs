using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace KaChow.AByteSizedMuseum
{
    public class LoopBlock : CodeBlock
    {
        /*
            TODO: ENABLE PLAYER TO SET COUNTER AND SHIT
        */

        [SerializeField] private TMP_Text counterText;
        [SerializeField] private int counter;

        [SerializeField] private Button plusIcon;
        [SerializeField] private Button minusIcon;

        public override void Start()
        {
            base.Start();
            UpdateCounterText();
        }

        public override IEnumerator ExecuteBlock(int botID)
        {
            plusIcon.enabled = false;
            minusIcon.enabled = false;

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
                }
            }

            plusIcon.enabled = true;
            minusIcon.enabled = true;
        }

        public void IncrementCounter()
        {
            Debug.Log("Inceremebt CLickec");
            counter++;
            UpdateCounterText();
        }

        public void DecrementCounter()
        {
            Debug.Log("Decrement CLickec");
            if (counter > 0)
            {
                counter--;
                UpdateCounterText();
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
