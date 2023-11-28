using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class ExhibitDoor : InteractableBase
    {
        [Header("Events")]
        // public GameEvent onDoorInteract;

        [SerializeField]
        private Transform[] doors;

        private bool isOpen = false;

        // Should have an idea what exhibit it is in??

        protected override void OnTriggerExit(Collider actor)
        {
            base.OnTriggerExit(actor);

            if (isOpen)
            {
                foreach (var door in doors)
                {
                    door.gameObject.SetActive(true);
                }
                isOpen = false;
            }
        }

        public override void OnInteract()
        {
            base.OnInteract();
            // onDoorInteract.Raise(this, name);

            if (!isOpen)
            {
                foreach (var door in doors)
                {
                    door.gameObject.SetActive(false);
                }
                isOpen = true;
            }
        }
    }
}
