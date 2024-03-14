using UnityEngine;
using DG.Tweening;

namespace KaChow.AByteSizedMuseum
{
    public class ExhibitDoor : InteractableBase
    {
        // [Header("Events")]
        // public GameEvent onDoorInteract;

        [Header("Doors")]
        public Transform[] doors; // doors[0] => left door, doors[1] => right door
        // public Transform leftDoor;
        // public Transform rightDoor;

        [SerializeField] private bool isOpen = false;
        [SerializeField] private float doorOpenSpeed = 0.5f;
        public bool isLocked = false;

        protected override void OnTriggerExit(Collider actor)
        {
            AudioManager.Instance.PlaySFX("DoorOpen");
            base.OnTriggerExit(actor);

            if (isOpen)
                OpenCloseDoor();
        }

        public override void OnInteract()
        {
            base.OnInteract();
            AudioManager.Instance.PlaySFX("DoorOpen");

            if (!isOpen)
                OpenCloseDoor();
        }

        private void OpenCloseDoor()
        {
            if (isLocked) return; // Check if the door is locked and return if true

            Vector3 openRotation = new Vector3(0f, 90f, 0f); // Rotation for opening the door
            Vector3 closeRotation = new Vector3(0f, -90f, 0f); // Rotation for closing the door

            for (int i = 0; i < doors.Length; i++)
            {
                var door = doors[i];
                // Determine the target rotation based on the door's current state (open or closed)
                // and whether the door is the left or right door in the pair
                Vector3 targetRotation = isOpen ? closeRotation : openRotation;
                Ease ease = isOpen ? Ease.InExpo : Ease.OutExpo;
                targetRotation = (i % 2 == 0) ? -targetRotation : targetRotation;

                door.DORotate(targetRotation, doorOpenSpeed, RotateMode.LocalAxisAdd)
                    .SetLoops(1)
                    .SetEase(ease);
            }

            isOpen = !isOpen; // Toggle the isOpen state
        }
    }
}
