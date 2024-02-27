using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace KaChow.AByteSizedMuseum
{
    public class CodeBlockSlot : MonoBehaviour, IPointerClickHandler
    {
        private CodeBlock codeBlock;
        [SerializeField] private CodeBlock clonedBlock;

        private void Start()
        {
            codeBlock = GetComponentInChildren<CodeBlock>();
        }

        private CodeBlock Clone()
        {
            if (codeBlock == null) return null;
            Debug.Log("Clone");
            var clone = Instantiate(codeBlock);
            clone.gameObject.transform.position = Input.mousePosition;
            clone.gameObject.transform.SetParent(transform.root);

            clone.isInfinite = true;

            return clone;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (clonedBlock == null) return;
            Debug.Log("OnPointerClick from CodeBlockSlot called");

        }
    }
}
