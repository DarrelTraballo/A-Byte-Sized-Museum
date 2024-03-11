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

        protected virtual void OnTriggerEnter(Collider actor)
        {
            if (actor.CompareTag("Player"))
            {
                Debug.Log("Press E to interact");
            }
        }

        protected virtual void OnTriggerExit(Collider actor)
        {
            if (actor.CompareTag("Player"))
            {
                Debug.Log("player exited");
            }
        }

        public virtual void OnLookEnter()
        {
        }

        public virtual void OnLookExit()
        {
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
