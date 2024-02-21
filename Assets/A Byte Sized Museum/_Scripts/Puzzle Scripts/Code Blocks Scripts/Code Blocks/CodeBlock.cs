using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace KaChow.AByteSizedMuseum
{
    /*
        possible approaches:
            1. may panel na si player sa inventory UI na nandun na lahat ng mga code blocks na need niya.
                - unlimited number of each code block, player just has to drag and drop the code block to where they choose to.
                - cannot drag and drop to the player's own inventory. picked up code block simply resets position
                > quick, temporary (?) solution
                > ITO NALANG MUNA

            2. each code block has their own count
                - players will have to look around the level to find more of these blocks
                    - adds complexity to level generation since we need to randomly place random code blocks around the level
                - need to implement a pickup system
                - need to implement some kind of counter for these code blocks.
                >
    */

    public abstract class CodeBlock : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        [Header("Code Block Details")]
        public string codeBlockName;
        [TextArea] public string codeBlockDescription;
        [SerializeField] protected GameEvent onCodeBlockClick;

        protected float delay = 1.0f;
        public bool canDrag = true;

        public abstract IEnumerator ExecuteBlock(int botID);

        [HideInInspector] public Transform parentAfterDrag;

        [HideInInspector] public Image image;

        private void Start()
        {
            image = GetComponentInChildren<Image>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!canDrag) return;

            onCodeBlockClick.Raise(this, this);
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            image.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!canDrag) return;

            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!canDrag) return;

            transform.SetParent(parentAfterDrag);
            image.raycastTarget = true;
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            onCodeBlockClick.Raise(this, this);
        }
    }
}
