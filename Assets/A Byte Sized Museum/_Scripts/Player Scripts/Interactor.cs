using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum {
    public class Interactor : MonoBehaviour
    {
        [Header("Player variables")]
        public Transform interactorSource;
        public float interactRange;

        private PlayerControls playerControls;

        private void Awake()
        {
            playerControls = new PlayerControls();
        }

        private void OnEnable()
        {
            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Disable();
        }

        private void Update() 
        {
            Ray r = new Ray(interactorSource.position, interactorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, interactRange))
            {
                var hit = hitInfo.collider.gameObject.TryGetComponent(out InteractableBase interactObj);
                if (hit)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        interactObj.OnInteract();
                        Debug.Log("Pressed");
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
