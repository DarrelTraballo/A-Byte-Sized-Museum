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
            var dropped = eventData.pointerDrag;
            var codeBlock = dropped.GetComponent<CodeBlock>();
            var dropTarget = eventData.pointerCurrentRaycast.gameObject;

            if (dropTarget.GetComponent<InterpreterLine>() != null)
            {
                if (transform.childCount == 0)
                {
                    codeBlock.parentAfterDrag = transform;
                }
                else
                {
                    if (!codeBlock.isInfinite)
                    {
                        Destroy(dropped);
                    }
                }
            }
            else
            {
                Destroy(dropped);
            }
        }
    }
}
