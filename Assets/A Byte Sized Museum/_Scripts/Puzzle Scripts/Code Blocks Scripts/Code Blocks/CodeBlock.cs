using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace KaChow.AByteSizedMuseum
{
    public abstract class CodeBlock : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        [Header("Code Block Details")]
        public string codeBlockName;
        [TextArea] public string codeBlockDescription;
        [SerializeField] protected GameEvent onCodeBlockClick;

        protected float delay = 0.5f;
        public bool isInfinite = false;

        public abstract IEnumerator ExecuteBlock(int botID);

        [HideInInspector] public Transform parentBeforeDrag;
        [HideInInspector] public Transform parentAfterDrag;

        [HideInInspector] public Image image;

        private CodeBlock movedBlock;

        private InputManager inputManager;

        public virtual void Start()
        {
            inputManager = InputManager.Instance;
            image = GetComponentInChildren<Image>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            inputManager.enabled = false;
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
            movedBlock.parentBeforeDrag = transform.parent;
            parentBeforeDrag = transform.parent;
            movedBlock.parentAfterDrag = transform.parent;
            movedBlock.transform.SetParent(GameObject.Find("ContainerCenter").transform);
            movedBlock.image.raycastTarget = false;

        }

        public void OnDrag(PointerEventData eventData)
        {
            movedBlock.gameObject.transform.position = Input.mousePosition;

            // Debug.Log($"{name} parent BEFORE drag : {movedBlock.parentBeforeDrag.name}");
            // Debug.Log($"{name} parent AFTER drag : {movedBlock.parentAfterDrag.name}");
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (parentAfterDrag == null)
            {
                Destroy(movedBlock.gameObject);
                return;
            }
            movedBlock.transform.SetParent(parentAfterDrag);
            movedBlock.image.raycastTarget = true;
            movedBlock = null;
            inputManager.enabled = true;
            parentAfterDrag = null;
            // parentBeforeDrag = null;
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            onCodeBlockClick.Raise(this, this);
        }
    }
}
