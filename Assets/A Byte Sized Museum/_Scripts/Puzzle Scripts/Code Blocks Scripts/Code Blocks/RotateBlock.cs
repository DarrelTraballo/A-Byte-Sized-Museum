using System;
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

        public override IEnumerator ExecuteBlock(int botID)
        {
            onRotate.Raise(this, new Tuple<RotateDirection, int>(rotateDirection, botID));
            yield return new WaitForSeconds(delay);
        }

        public override void Start()
        {
            base.Start();
            // TODO: currently does not fit the panel
            // codeBlockName = $"Rotate Block \n({rotateDirection})";
            SetDirection();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            // codeBlockName = $"Rotate Block \n({rotateDirection})";

            SetDirection();
        }

        private void SetDirection()
        {
            rotateDirection = (RotateDirection)(((int)rotateDirection + 1) % Enum.GetValues(typeof(RotateDirection)).Length);
            rotateDirectionText.text = GetTextRotation(rotateDirection);
        }

        private string GetTextRotation(RotateDirection rotateDirection)
        {
            return rotateDirection switch
            {
                RotateDirection.Clockwise => "CW",
                RotateDirection.CounterClockwise => "CC",
                _ => rotateDirection.ToString(),
            };
        }
    }

    public enum RotateDirection
    {
        Clockwise, CounterClockwise
    }
}
