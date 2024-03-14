using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class Interactor : MonoBehaviour
    {
        [Header("Player variables")]
        public Transform interactorSource;
        public float interactRange;

        private InputManager inputManager;
        private GameManager gameManager;

        private InteractableBase lastInteractedObject = null;

        private void Start()
        {
            inputManager = InputManager.Instance;
            gameManager = GameManager.Instance;
        }

        private void Update()
        {
            if (gameManager.currentState != GameState.Playing) return;

            Ray r = new Ray(interactorSource.position, interactorSource.forward);
            bool hitSomething = Physics.Raycast(r, out RaycastHit hitInfo, interactRange);
            InteractableBase currentInteractObject = null;

            if (hitSomething && hitInfo.collider.gameObject.TryGetComponent(out InteractableBase interactObj))
            {
                currentInteractObject = interactObj;

                if (lastInteractedObject != currentInteractObject)
                {
                    interactObj.OnLookEnter();
                }

                if (inputManager.PlayerInteractedThisFrame())
                    interactObj.OnInteract();
            }

            if (lastInteractedObject != null && lastInteractedObject != currentInteractObject)
            {
                lastInteractedObject.OnLookExit();
            }

            lastInteractedObject = currentInteractObject;
        }
    }
}
