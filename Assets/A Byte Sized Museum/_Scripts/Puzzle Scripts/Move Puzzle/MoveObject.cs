using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class MoveObject : MonoBehaviour
    {
        [SerializeField]
        private float speed = 10.0f;
        
        public void Move()
        {
            Debug.Log("Moving");
            transform.Translate(Vector3.forward);
            // Vector3 targetPosition = transform.position + transform.forward;
            // transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
            // transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }

        public void Rotate(Component sender, object data)
        {
            Debug.Log("Rotating");
            if (data is RotateDirection rotateDirection)
            {
                if (rotateDirection == RotateDirection.Clockwise)
                    transform.Rotate(0f, -90f, 0f);
                else if (rotateDirection == RotateDirection.CounterClockwise)
                    transform.Rotate(0f, 90f, 0f);
            }
        }
    }
}
