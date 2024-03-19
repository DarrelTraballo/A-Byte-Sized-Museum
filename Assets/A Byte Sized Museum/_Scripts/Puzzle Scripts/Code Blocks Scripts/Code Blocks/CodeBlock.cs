using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;

namespace KaChow.AByteSizedMuseum
{
    public abstract class CodeBlock : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Code Block Details")]
        public string codeBlockName;
        [TextArea] public string codeBlockDescription;
        [SerializeField] protected GameEvent onCodeBlockHover;
        [SerializeField] protected GameObject codeBlockDetails;

        protected float delay = 0.5f;
        public bool isInfinite = false;

        [HideInInspector] public Transform parentBeforeDrag;
        [HideInInspector] public Transform parentAfterDrag;

        [HideInInspector] public Image image;

        private CodeBlock movedBlock;

        private InputManager inputManager;

        private Vector3 mousePos;

        public abstract IEnumerator ExecuteBlock(int botID);

        public virtual void Start()
        {
            inputManager = InputManager.Instance;
            image = GetComponentInChildren<Image>();
            ToggleCodeBlockDetails(false);
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

            movedBlock.onCodeBlockHover.Raise(this, this);
            movedBlock.parentBeforeDrag = transform.parent;
            parentBeforeDrag = transform.parent;
            movedBlock.parentAfterDrag = transform.parent;
            movedBlock.transform.SetParent(GameObject.Find("ContainerCenter").transform);
            movedBlock.image.raycastTarget = false;

        }

        public void OnDrag(PointerEventData eventData)
        {
            movedBlock.gameObject.transform.position = Input.mousePosition;
            ToggleCodeBlockDetails(false);
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

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // mousePos = Input.mousePosition;
            // onCodeBlockHover.Raise(this, new Tuple<bool, Vector3>(true, mousePos));

            ToggleCodeBlockDetails(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // mousePos = Input.mousePosition;
            // onCodeBlockHover.Raise(this, new Tuple<bool, Vector3>(false, mousePos));

            ToggleCodeBlockDetails(false);
        }

        private void ToggleCodeBlockDetails(bool isActive)
        {
            codeBlockDetails.SetActive(isActive);

            Transform codeBlockDetailsPanelTransform = codeBlockDetails.transform;

            TMP_Text codeBlockNameText = codeBlockDetailsPanelTransform.Find("Code Block Name").GetComponent<TMP_Text>();
            TMP_Text codeBlockDescriptionText = codeBlockDetailsPanelTransform.Find("Code Block Description").GetComponent<TMP_Text>();

            codeBlockNameText.text = codeBlockName;
            codeBlockDescriptionText.text = codeBlockDescription;

            Color codeBlockColor = image.color;
            codeBlockDetails.GetComponent<Image>().color = codeBlockColor;

            codeBlockDetails.transform.SetAsLastSibling();
        }
    }
}
