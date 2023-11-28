using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace KaChow.AByteSizedMuseum
{
    public abstract class CodeBlock : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public abstract IEnumerator ExecuteBlock();

        [HideInInspector]
        public Transform parentAfterDrag;

        public TextMeshProUGUI countText;
        private Image image;

        private void Start()
        {
            image = GetComponentInChildren<Image>();
        }

        public void RefreshCount()
        {
            
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            image.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.SetParent(parentAfterDrag);
            image.raycastTarget = true;
        }

    }
}
