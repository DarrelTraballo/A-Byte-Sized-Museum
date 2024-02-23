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

        private void Start()
        {
            inputManager = InputManager.Instance;
            gameManager = GameManager.Instance;
        }

        private void Update()
        {
            if (inputManager.PlayerInteractedThisFrame() && gameManager.currentState == GameState.Playing)
            {
                Ray r = new Ray(interactorSource.position, interactorSource.forward);
                if (Physics.Raycast(r, out RaycastHit hitInfo, interactRange))
                {
                    var hit = hitInfo.collider.gameObject.TryGetComponent(out InteractableBase interactObj);
                    if (hit)
                    {
                        interactObj.OnLookEnter();

                        interactObj.OnInteract();

                    }
                }
            }
            // else
            // {
            //     // GameManager.Instance.crossHairText.text = "";
            // }
        }
    }
}
