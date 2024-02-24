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
        public bool isInfinite = false;

        public abstract IEnumerator ExecuteBlock(int botID);

        [HideInInspector] public Transform parentAfterDrag;

        [HideInInspector] public Image image;

        private CodeBlock movedBlock;

        public virtual void Start()
        {
            image = GetComponentInChildren<Image>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (isInfinite)
            {
                CodeBlock clone = Instantiate(this, transform.position, transform.rotation);
                clone.name = gameObject.name;
                movedBlock = clone.GetComponent<CodeBlock>();
                movedBlock.isInfinite = false;
            }
            else
            {
                movedBlock = this;
            }

            movedBlock.onCodeBlockClick.Raise(this, this);
            movedBlock.parentAfterDrag = transform.parent;
            movedBlock.transform.SetParent(transform.root);
            movedBlock.transform.SetAsLastSibling();
            movedBlock.image.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            movedBlock.gameObject.transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            movedBlock.transform.SetParent(parentAfterDrag);
            movedBlock.image.raycastTarget = true;
            movedBlock = null;
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            onCodeBlockClick.Raise(this, this);
        }
    }
}
