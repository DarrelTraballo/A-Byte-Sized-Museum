using System.Collections;
using UnityEngine;
using TMPro;

namespace KaChow.AByteSizedMuseum
{
    public class RotateBlock : CodeBlock
    {
        [Header("Game Event")]
        public GameEvent onRotate;

        // Parameters
        [Header("Parameters")]
        public TextMeshProUGUI rotateDirectionText;

        private RotateDirection rotateDirection;

        public override IEnumerator ExecuteBlock()
        {
            Debug.Log("Rotate Block");
            onRotate.Raise(this, rotateDirection);
            yield return new WaitForSeconds(1f);
        }

        private void SetDirection()
        {
            
        }
    }

    public enum RotateDirection
    {
        Clockwise, CounterClockwise
    }
}
