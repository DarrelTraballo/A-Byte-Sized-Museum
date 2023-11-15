using UnityEngine;
using UnityEngine.EventSystems;

namespace KaChow.AByteSizedMuseum
{
    public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("Begin Drag");
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("Dragging");
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("End Drag");
        }
    }
}
