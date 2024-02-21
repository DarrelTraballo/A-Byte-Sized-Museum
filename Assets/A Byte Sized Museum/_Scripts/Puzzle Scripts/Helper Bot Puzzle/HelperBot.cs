using UnityEngine;
using DG.Tweening;
using System;

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
        [SerializeField] private int botID;
        [SerializeField] private float moveSpeed = 0.25f;

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

        public void Move(Component sender, object data)
        {
            if (data is not int interpreterID || interpreterID != botID) return;

            if (!FireRaycast(out GameObject hitObject))
            {
                Vector3 targetPosition = transform.position + transform.forward;
                transform.DOMove(targetPosition, moveSpeed)
                         .SetEase(Ease.OutExpo);

            }
        }

        public void Rotate(Component sender, object data)
        {
            if (data is not Tuple<RotateDirection, int> tupleData || tupleData.Item2 != botID) return;

            RotateDirection rotateDirection = tupleData.Item1;
            float targetRotation;

            switch (rotateDirection)
            {
                case RotateDirection.Clockwise:
                    targetRotation = transform.eulerAngles.y - 90.0f;
                    break;
                case RotateDirection.CounterClockwise:
                    targetRotation = transform.eulerAngles.y + 90.0f;
                    break;
                default:
                    targetRotation = transform.eulerAngles.y;
                    break;
            }

            transform.DORotate(new Vector3(0f, targetRotation, 0f), moveSpeed, RotateMode.Fast)
                     .SetEase(Ease.InOutCirc);
        }

        // TODO: fix
        public void Reset()
        {
            // reset bot's positions
            transform.SetPositionAndRotation(initialPosition, initialRotation);

            // reset object's position and rotation
            if (!isHoldingAnObject)
            {
                // heldObject.transform.SetPositionAndRotation(heldObjectInitialPosition, heldObjectInitialRotation);

                return;
            }

            RotateArms();

            heldObject = aboveHead.GetChild(0).gameObject;
            heldObject.transform.parent = heldObjectParent;
            heldObject.transform.SetPositionAndRotation(heldObjectInitialPosition, heldObjectInitialRotation);

            isHoldingAnObject = false;
        }

        public void PickUp(Component sender, object data)
        {
            if (data is not int interpreterID || interpreterID != botID) return;

            // if bot is already holding an object, do nothing
            if (isHoldingAnObject) return;

            // if there is no object in front of bot, do nothing
            if (!FireRaycast(out GameObject hitObject)) return;

            if (hitObject.TryGetComponent(out HelperBotPuzzleObject puzzleObject))
            {
                RotateArms();
                // cache object's parent, position and rotation
                heldObjectParent = puzzleObject.transform.parent;
                heldObjectInitialPosition = puzzleObject.transform.position;
                heldObjectInitialRotation = puzzleObject.transform.rotation;

                puzzleObject.transform.parent = aboveHead;
                Vector3 targetPosition = aboveHead.position;

                puzzleObject.transform.DOMove(targetPosition, moveSpeed)
                                     .SetEase(Ease.OutExpo);

                isHoldingAnObject = true;
            }
        }

        public void Drop(Component sender, object data)
        {
            if (data is not int interpreterID || interpreterID != botID) return;

            // if bot is NOT holding an object, do nothing
            if (!isHoldingAnObject) return;

            // if there is no object in front of bot, do nothing
            if (FireRaycast(out GameObject hitObject)) return;

            // Reset hand position
            RotateArms();

            heldObject = aboveHead.GetChild(0).gameObject;
            Vector3 targetPosition = transform.position + transform.forward;

            heldObject.transform.DOMove(targetPosition, moveSpeed)
                               .SetEase(Ease.OutExpo);

            heldObject.transform.parent = heldObjectParent;
            isHoldingAnObject = false;
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
            // leftHand.transform.DORotate()

            leftHand.transform.Rotate(180.0f, 0f, 0f);
            rightHand.transform.Rotate(180.0f, 0f, 0f);
        }
    }
}