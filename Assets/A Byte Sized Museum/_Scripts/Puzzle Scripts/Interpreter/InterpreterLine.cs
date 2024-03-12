using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace KaChow.AByteSizedMuseum
{
    public class InterpreterLine : MonoBehaviour, IDropHandler
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
                if (targetInterpreterLine.transform.childCount >= 1)
                {
                    Debug.Log($"{heldCodeBlock.parentBeforeDrag.name}");

                    if (heldCodeBlock.parentBeforeDrag.GetComponent<CodeBlockSlot>() != null) return;

                    CodeBlock existingCodeBlock = targetInterpreterLine.GetComponentInChildren<CodeBlock>();

                    // Swap the parent of the held and existing code blocks
                    existingCodeBlock.transform.SetParent(heldCodeBlock.parentAfterDrag);
                    heldCodeBlock.transform.SetParent(targetInterpreterLine.transform);
                }

                heldCodeBlock.parentAfterDrag = transform;
            }
            else
            {
                heldCodeBlock.parentAfterDrag = null;
            }
        }
    }
}
