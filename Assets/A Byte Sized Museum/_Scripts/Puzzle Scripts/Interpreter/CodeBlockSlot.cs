using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace KaChow.AByteSizedMuseum
{
    public class CodeBlockSlot : MonoBehaviour, IDropHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        [SerializeField] private CodeBlock codeBlock;
        [SerializeField] private CodeBlock clonedBlock;

        private void Start()
        {
            if (codeBlock == null) return;
            // codeBlock.image.raycastTarget = false;
        }

        private CodeBlock Clone()
        {
            if (codeBlock == null) return null;
            Debug.Log("Clone");
            var clone = Instantiate(codeBlock);
            clone.gameObject.transform.position = Input.mousePosition;
            clone.gameObject.transform.SetParent(transform.root);

            clone.canDrag = true;

            return clone;
        }

        public void OnDrop(PointerEventData eventData)
        {

        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("erm");
            clonedBlock = Clone();

            if (clonedBlock != null)
            {
                Debug.Log("clonedBlock == null D:");
                clonedBlock.OnBeginDrag(eventData);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (clonedBlock == null) return;
            clonedBlock.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (clonedBlock == null) return;
            clonedBlock.OnEndDrag(eventData);
            clonedBlock = null;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (clonedBlock == null) return;
            Debug.Log("OnPointerClick from CodeBlockSlot called");

        }
    }
}
