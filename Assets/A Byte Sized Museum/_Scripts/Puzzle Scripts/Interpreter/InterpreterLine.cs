using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace KaChow.AByteSizedMuseum
{
    public class InterpreterLine : MonoBehaviour, IDropHandler
    {
        private Image highlight;
        private Color defaultColor;

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
            if (transform.childCount == 0) 
            {
                var dropped = eventData.pointerDrag;
                var codeBlock = dropped.GetComponent<CodeBlock>();
                codeBlock.parentAfterDrag = transform;
            }
        }
    }
}
