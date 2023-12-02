using UnityEngine;

/*
    Spiderman PS4 circuit voltage puzzles 

    logic gates

    final puzzle sa pinaka dulo, lightbot puzzles can give items that can help solving the final puzzle
*/

namespace KaChow.AByteSizedMuseum
{
    public class HelperBot : MonoBehaviour
    {
        [Header("Helper Bot Variables")]
        [SerializeField] private float speed = 10.0f;
        [SerializeField] private float moveDistance = 1f;
        
        [Header("Helper Bot Raycast Variables")]
        [SerializeField] private GameObject raycastSource;
        [SerializeField] private float raycastRange;

        [Header("Helper Bot Pick Up Variables")]
        [SerializeField] private GameObject leftHand;
        [SerializeField] private GameObject rightHand;
        [SerializeField] private Transform aboveHead;
        private bool isHoldingAnObject;

        // bot's initial position and rotation
        private Vector3 initialPosition;
        private Quaternion initialRotation;

        // pickupable object's parent, position, and rotation
        private GameObject heldObject;
        private Transform heldObjectParent;
        private Vector3 heldObjectInitialPosition;
        private Quaternion heldObjectInitialRotation;

        private void Start()
        {
            initialPosition = transform.position;
            initialRotation = transform.rotation;
        }
        
        public void Move()
        {
            if (!FireRaycast(out GameObject hitObject))
            {
                transform.Translate(Vector3.forward * moveDistance);
            }

            // TODO: look DOTween
            // Vector3 targetPosition = transform.position + transform.forward;
            // transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
            // transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }

        public void Rotate(Component sender, object data)
        {
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
            // reset bot's positions
            transform.SetPositionAndRotation(initialPosition, initialRotation);

            // reset object's position and rotation
            if (!isHoldingAnObject) 
            {
                heldObject.transform.SetPositionAndRotation(heldObjectInitialPosition, heldObjectInitialRotation);

                return;
            }

            RotateArms();

            heldObject = aboveHead.GetChild(0).gameObject;
            heldObject.transform.parent = heldObjectParent;
            heldObject.transform.SetPositionAndRotation(heldObjectInitialPosition, heldObjectInitialRotation);

            isHoldingAnObject = false;
        }

        public void PickUp()
        {
            // if bot is already holding an object, do nothing
            if (isHoldingAnObject) return;

            // if there is an object in front of bot, pick it up
            if (FireRaycast(out GameObject hitObject))
            {
                RotateArms();

                // cache object's parent, position and rotation
                heldObjectParent = hitObject.transform.parent;
                heldObjectInitialPosition = hitObject.transform.position;
                heldObjectInitialRotation = hitObject.transform.rotation;

                hitObject.transform.parent = aboveHead;
                hitObject.transform.position = aboveHead.position;
                
                isHoldingAnObject = true;
            }
        }

        public void Drop()
        {   
            // if bot is NOT holding an object
            if (!isHoldingAnObject) return;

            // check if there is an object in front.
            if (!FireRaycast(out GameObject hitObject))
            {
                // Reset hand position
                RotateArms();

                heldObject = aboveHead.GetChild(0).gameObject;

                heldObject.transform.position = transform.position + transform.forward;
                heldObject.transform.parent = heldObjectParent;

                isHoldingAnObject = false;
            }
        }

        public bool FireRaycast(out GameObject hitObject)
        {
            Ray ray = new Ray(raycastSource.transform.position, raycastSource.transform.forward);

            Debug.DrawRay(ray.origin, ray.direction * raycastRange, Color.red);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, raycastRange))
            {
                hitObject = hitInfo.collider.gameObject;

                return true;
            }

            hitObject = null;
            return false;
        }

        private void RotateArms()
        {
            leftHand.transform.Rotate(180.0f, 0f, 0f);
            rightHand.transform.Rotate(180.0f, 0f, 0f);
        }
    }
}
