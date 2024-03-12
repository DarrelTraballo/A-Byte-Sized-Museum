using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace KaChow.AByteSizedMuseum
{
    public class LoopBlock : CodeBlock
    {
        /*
            TODO: ENABLE PLAYER TO SET COUNTER AND SHIT
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

            yield return new WaitForSeconds(delay);

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
        }

        public void IncrementCounter()
        {
            Debug.Log("Inceremebt CLickec");
            counter++;
            if (counter > 10)
            {
                counter = 1;
            }
            UpdateCounterText();
        }

        private void UpdateCounterText()
        {
            if (counterText == null)
            {
                Debug.LogError("Counter Text obj is not assigned");
            }

            counterText.text = counter.ToString();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            IncrementCounter();
        }
    }
}
