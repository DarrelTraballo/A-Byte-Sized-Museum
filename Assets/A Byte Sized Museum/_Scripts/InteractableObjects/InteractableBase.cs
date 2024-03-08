using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public abstract class InteractableBase : MonoBehaviour
    {
        protected GameManager gameManager;
        protected InputManager inputManager;

        public virtual void Start()
        {
            gameManager = GameManager.Instance;
            inputManager = InputManager.Instance;
        }

        // what happens if player enters interactable object collider
        protected virtual void OnTriggerEnter(Collider actor)
        {
            if (actor.CompareTag("Player"))
            {
                Debug.Log("Press E to interact");
            }
        }

        // what happens if player exits interactable object collider
        protected virtual void OnTriggerExit(Collider actor)
        {
            if (actor.CompareTag("Player"))
            {
                Debug.Log("player exited");
            }
        }

        public virtual void OnLookEnter()
        {
            Debug.Log($"Looking at {name}");
        }

        public virtual void OnLookExit()
        {
            Debug.Log($"Looked away from {name}");
            gameManager.DisableToolTipText();
        }

        // base method for interactable object specific interactions
        // just leave empty
        public virtual void OnInteract()
        {
            AudioManager.Instance.PlaySFX("Click");
        }
    }
}
