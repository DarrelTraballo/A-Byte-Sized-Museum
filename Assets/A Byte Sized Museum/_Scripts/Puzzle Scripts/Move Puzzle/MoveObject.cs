using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class MoveObject : MonoBehaviour
    {
        [SerializeField]
        private float speed = 10.0f;

        [SerializeField]
        private GameObject raycastSource;
        [SerializeField]
        private float raycastRange;

        private Vector3 initialPosition;
        private Quaternion initialRotation;

        private void Start()
        {
            initialPosition = transform.position;
            initialRotation = transform.rotation;
        }
        
        public void Move()
        {
            if (!FireRaycast(out string hitObjectTag))
            {
                Debug.Log("Moving");
                transform.Translate(Vector3.forward);
            }
            
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

        public void Reset()
        {
            Debug.Log("Resetting position");
            transform.SetPositionAndRotation(initialPosition, initialRotation);
        }

        public bool FireRaycast(out string hitObjectTag)
        {
            Ray ray = new Ray(raycastSource.transform.position, raycastSource.transform.forward);

            Debug.DrawRay(ray.origin, ray.direction * raycastRange, Color.red);


            if (Physics.Raycast(ray, out RaycastHit hitInfo, raycastRange))
            {
                hitObjectTag = hitInfo.collider.tag;

                Debug.Log($"Hit {hitObjectTag}");

                return true;
            }

            hitObjectTag = null;
            return false;
        }
    }
}
