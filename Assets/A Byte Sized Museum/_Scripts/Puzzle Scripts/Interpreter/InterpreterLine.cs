using UnityEngine;
using UnityEngine.EventSystems;

namespace KaChow.AByteSizedMuseum
{
    public class InterpreterLine : MonoBehaviour, IDropHandler
    {
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
