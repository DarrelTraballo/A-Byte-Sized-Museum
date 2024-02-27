using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace KaChow.AByteSizedMuseum
{
    public class InterpreterLine : MonoBehaviour, IDropHandler, IPointerClickHandler
    {
        private Image highlight;
        private Color defaultColor;

        [SerializeField] private bool isOccupied = false;

        private void Start()
        {
            highlight = GetComponent<Image>();
            defaultColor = highlight.color;
        }

        public void EnableHighlight()
        {
            highlight.color = Color.white;
        }

        public void DisableHighlight()
        {
            highlight.color = defaultColor;
        }

        public void OnDrop(PointerEventData eventData)
        {
            GameObject dropped = eventData.pointerDrag;
            CodeBlock heldCodeBlock = dropped.GetComponent<CodeBlock>();
            GameObject dropTarget = eventData.pointerCurrentRaycast.gameObject;

            if (dropTarget.TryGetComponent<InterpreterLine>(out var targetInterpreterLine))
            {
                if (targetInterpreterLine.transform.childCount > 0)
                {
                    return;
                }
                // if (targetInterpreterLine.transform.childCount == 0)
                // {
                heldCodeBlock.parentAfterDrag = transform;
                // return;
                // }

                var targetCodeBlock = targetInterpreterLine.GetComponentInChildren<CodeBlock>();
                if (targetCodeBlock == null)
                {
                    Debug.LogError("no target codeblock found in this interpreter line");
                    return;
                }

                if (targetCodeBlock.isInfinite)
                {
                    Debug.Log("Destroyed in targetCodeBlock.isInfinite check");
                    Destroy(heldCodeBlock);
                }
            }
            else
            {
                Destroy(heldCodeBlock);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log($"child count : {transform.childCount}");
        }
    }
}
