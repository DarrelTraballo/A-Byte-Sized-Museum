using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class HelperBotPuzzleObject : MonoBehaviour
    {
        private Vector3 initialPosition;
        private Quaternion initialRotation;

        private void Start()
        {
            initialPosition = transform.position;
            initialRotation = transform.rotation;
        }

        public void Reset()
        {
            transform.SetPositionAndRotation(initialPosition, initialRotation);
        }
    }
}
