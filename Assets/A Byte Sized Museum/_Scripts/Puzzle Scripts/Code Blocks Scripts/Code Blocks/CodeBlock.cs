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
        [SerializeField] protected GameEvent onCodeBlockDragging;
        // [SerializeField] protected GameEvent onCodeBlockDropped;
        [SerializeField] protected GameObject codeBlockDetails;

        protected float delay = 0.5f;
        public bool isInfinite = false;

        [HideInInspector] public Transform parentBeforeDrag;
        [HideInInspector] public Transform parentAfterDrag;

        [HideInInspector] public Image image;

        private CodeBlock heldCodeBlock;

        private InputManager inputManager;

        private Vector3 mousePos;

        public abstract IEnumerator ExecuteBlock(int botID);

        public virtual void Start()
        {
            inputManager = InputManager.Instance;
            image = GetComponent<Image>();
            ToggleCodeBlockDetails(false);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            onCodeBlockDragging.Raise(this, true);
            inputManager.enabled = false;

            if (isInfinite)
            {
                heldCodeBlock = CloneCodeBlock();
            }
            else
            {
                heldCodeBlock = this;
                // movedBlock.parentAfterDrag = transform.parent;
                // movedBlock.parentBeforeDrag = transform.parent;
            }

            heldCodeBlock.transform.SetParent(GameObject.Find("ContainerCenter").transform);
            heldCodeBlock.image.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            heldCodeBlock.gameObject.transform.position = Input.mousePosition;
            ToggleCodeBlockDetails(false);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            onCodeBlockDragging.Raise(this, false);
            inputManager.enabled = true;

            GameObject dropTarget = eventData.pointerCurrentRaycast.gameObject;

            // CODE BLOCK SWAPPING MECHANIC IDK
            // if (dropTarget != null && dropTarget.TryGetComponent(out CodeBlock targetCodeBlock))
            // {
            //     // Debug.Log($"attempted to drop {heldCodeBlock.name} on {targetCodeBlock.name}", targetCodeBlock.gameObject);
            //     if (targetCodeBlock != this)
            //     {
            //         // Debug.Log("if (targetCodeBlock != this) == true");
            //         Transform originalParent = heldCodeBlock.parentAfterDrag;
            //         Transform targetParent = targetCodeBlock.parentAfterDrag;

            //         Debug.Log($"{heldCodeBlock.name} original parent: {originalParent.name}", originalParent);
            //         Debug.Log($"{targetCodeBlock.name} target parent: {targetParent.name}", targetParent);

            //         heldCodeBlock.transform.SetParent(targetParent);
            //         targetCodeBlock.transform.SetParent(originalParent);
            //     }
            // }
            if (dropTarget != null && dropTarget.TryGetComponent(out InterpreterLine targetInterpreterLine))
            {
                // Debug.Log($"attempted to drop {heldCodeBlock.name} on {targetInterpreterLine.name}", targetInterpreterLine.gameObject);
                if (targetInterpreterLine.transform.childCount == 0)
                {
                    // Debug.Log($"no children inside {targetInterpreterLine.name}");
                    heldCodeBlock.transform.SetParent(targetInterpreterLine.transform);
                    heldCodeBlock.parentAfterDrag = targetInterpreterLine.transform;
                }
                else
                {
                    heldCodeBlock.transform.SetParent(heldCodeBlock.transform.parent);
                }
            }
            else
            {
                Debug.Log("attempted to drop on neither codeblock or interpreterline. destroying held codeblock");
                Destroy(heldCodeBlock.gameObject);
            }

            heldCodeBlock.image.raycastTarget = true;
            heldCodeBlock = null;
        }

        private CodeBlock CloneCodeBlock()
        {
            CodeBlock clone = Instantiate(this, transform.position, transform.rotation);
            clone.name = gameObject.name;
            clone.isInfinite = false;

            clone.parentBeforeDrag = transform.parent;
            clone.parentAfterDrag = transform.parent;

            return clone;
        }

        public virtual void OnPointerClick(PointerEventData eventData) { }

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
