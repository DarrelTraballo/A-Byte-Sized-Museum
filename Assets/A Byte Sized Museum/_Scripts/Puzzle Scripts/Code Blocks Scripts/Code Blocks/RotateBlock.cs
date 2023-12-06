using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

namespace KaChow.AByteSizedMuseum
{
    public class RotateBlock : CodeBlock
    {
        [TextArea] public string parameters; 

        [Header("Game Event")]
        public GameEvent onRotate;

        // Parameters
        [Header("Parameters")]
        [SerializeField] private TextMeshProUGUI rotateDirectionText;

        [SerializeField]
        private RotateDirection rotateDirection = RotateDirection.Clockwise;

        public override IEnumerator ExecuteBlock()
        {
            onRotate.Raise(this, rotateDirection);
            yield return new WaitForSeconds(delay);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            codeBlockName = $"Rotate Block ({rotateDirection})";

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
