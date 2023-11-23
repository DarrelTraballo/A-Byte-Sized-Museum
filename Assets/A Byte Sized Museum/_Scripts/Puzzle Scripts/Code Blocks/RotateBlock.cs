using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEditor.Experimental.GraphView;

namespace KaChow.AByteSizedMuseum
{
    public class RotateBlock : CodeBlock, IPointerClickHandler
    {
        [Header("Game Event")]
        public GameEvent onRotate;

        // Parameters
        [Header("Parameters")]
        [SerializeField] private TextMeshProUGUI rotateDirectionText;

        [SerializeField]
        private RotateDirection rotateDirection = RotateDirection.Clockwise;

        public override IEnumerator ExecuteBlock()
        {
            Debug.Log("Rotate Block");
            onRotate.Raise(this, rotateDirection);
            yield return new WaitForSeconds(1f);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Clicked!");
            SetDirection();
        }

        private void SetDirection()
        {
            rotateDirection = (RotateDirection) (((int) rotateDirection + 1) % System.Enum.GetValues(typeof(RotateDirection)).Length);
            rotateDirectionText.text = GetTextRotation(rotateDirection);
        }

        private string GetTextRotation(RotateDirection rotateDirection)
        {
            return rotateDirection switch
            {
                RotateDirection.Clockwise => "CC",
                RotateDirection.CounterClockwise => "CW",
                _ => rotateDirection.ToString(),
            };
        }
    }

    public enum RotateDirection
    {
        Clockwise, CounterClockwise
    }
}
