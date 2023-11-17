using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum {
    public class Interactor : MonoBehaviour
    {
        [Header("Player variables")]
        public Transform interactorSource;
        public float interactRange;

        private InputManager inputManager;

        private void Start()
        {
            inputManager = InputManager.Instance;
        }

        private void Update() 
        {
            Ray r = new Ray(interactorSource.position, interactorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, interactRange))
            {
                var hit = hitInfo.collider.gameObject.TryGetComponent(out InteractableBase interactObj);
                if (hit)
                {
                    interactObj.OnLookEnter();

                    if (inputManager.PlayerInteractedThisFrame())
                    {
                        interactObj.OnInteract();
                    }
                }
            } 
            else 
            {
                // GameManager.Instance.crossHairText.text = "";
            }
        }
    }
}
