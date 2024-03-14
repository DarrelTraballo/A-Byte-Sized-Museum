using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace KaChow.AByteSizedMuseum
{
    public class ExhibitDoor : InteractableBase
    {
        [Header("Doors")]
        [Tooltip("Doors[0] => left door, Doors[1] => right door")]
        public Transform[] doors; // doors[0] => left door, doors[1] => right door
        [SerializeField] private BoxCollider doorCollider;

        private bool isOpen = false;
        [SerializeField] private float doorOpenSpeed = 0.5f;
        [HideInInspector] public bool isLocked = false;

        public override void Start()
        {
            base.Start();
            className = "Door";
        }

        protected override void OnTriggerExit(Collider actor)
        {
            base.OnTriggerExit(actor);

            if (isOpen)
                OpenCloseDoor();
        }

        public override void OnInteract()
        {
            base.OnInteract();

            if (!isOpen)
                OpenCloseDoor();
        }

        private void OpenCloseDoor()
        {
            if (isLocked) return; // Check if the door is locked and return if true
            AudioManager.Instance.PlaySFX("DoorOpen");

            doorCollider.enabled = isOpen;

            Vector3 openRotation = new Vector3(0f, 90f, 0f); // Rotation for opening the door
            Vector3 closeRotation = new Vector3(0f, -90f, 0f); // Rotation for closing the door

            for (int i = 0; i < doors.Length; i++)
            {
                var door = doors[i];
                // Determine the target rotation based on the door's current state (open or closed)
                // and whether the door is the left or right door in the pair
                Vector3 targetRotation = isOpen ? closeRotation : openRotation;
                targetRotation = (i % 2 == 0) ? -targetRotation : targetRotation;

                door.DORotate(targetRotation, doorOpenSpeed, RotateMode.LocalAxisAdd)
                    .SetLoops(1)
                    .SetEase(Ease.OutExpo);
            }

            isOpen = !isOpen; // Toggle the isOpen state
        }
    }
}
