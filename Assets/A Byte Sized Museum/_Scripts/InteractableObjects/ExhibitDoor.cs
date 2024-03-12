using System.IO.IsolatedStorage;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class ExhibitDoor : InteractableBase
    {
        // [Header("Events")]
        // public GameEvent onDoorInteract;

        [Header("Doors")]
        public Transform[] doors;

        private bool isOpen = false;
        public bool isLocked = false;

        protected override void OnTriggerExit(Collider actor)
        {
            AudioManager.Instance.PlaySFX("DoorOpen");
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
            AudioManager.Instance.PlaySFX("DoorOpen");
            // onDoorInteract.Raise(this, name);

            if (!isOpen)
            {
                foreach (var door in doors)
                {
                    if (!isLocked)
                    {
                        door.gameObject.SetActive(false);
                    }
                    else
                    {
                        // Debug.Log("Door is Locked");
                    }
                }
                isOpen = true;
            }
        }
    }
}
