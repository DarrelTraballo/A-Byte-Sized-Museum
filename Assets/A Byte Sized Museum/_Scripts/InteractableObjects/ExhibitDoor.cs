using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class ExhibitDoor : InteractableBase
    {
        [Header("Events")]
        public GameEvent onDoorInteract;

        private Transform leftDoor;
        private Transform rightDoor;

        private bool isOpen = false;

        // Should have an idea what exhibit it is in??

        private void Start()
        {
            leftDoor = transform.Find("GlassDoor");
            rightDoor = transform.Find("GlassDoor.001");
        }

        protected override void OnTriggerExit(Collider actor)
        {
            base.OnTriggerExit(actor);

            if (isOpen)
            {
                leftDoor.gameObject.SetActive(true);
                rightDoor.gameObject.SetActive(true);
                isOpen = false;
            }
        }

        public override void OnInteract()
        {
            base.OnInteract();
            onDoorInteract.Raise(this, name);

            if (!isOpen)
            {
                leftDoor.gameObject.SetActive(false);
                rightDoor.gameObject.SetActive(false);
                isOpen = true;
            }
        }
    }
}
