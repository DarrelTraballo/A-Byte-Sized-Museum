using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace KaChow.AByteSizedMuseum
{
    /*
        possible approaches:
            1. may panel na si player sa inventory UI na nandun na lahat ng mga code blocks na need niya.
                - unlimited number of each code block, player just has to drag and drop the code block to where they choose to.
                - cannot drag and drop to the player's own inventory. picked up code block simply resets position
                > quick, temporary (?) solution

            2. each code block has their own count
                - players will have to look around the level to find more of these blocks
                    - adds complexity to level generation since we need to randomly place random code blocks around the level
                - need to implement a pickup system
                - need to implement some kind of counter for these code blocks.
                > 
    */
    
    public abstract class CodeBlock : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        protected float delay = 0.5f;
        public bool canDrag = true;
        public abstract IEnumerator ExecuteBlock();

        [HideInInspector]
        public Transform parentAfterDrag;

        // public TextMeshProUGUI countText;
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
