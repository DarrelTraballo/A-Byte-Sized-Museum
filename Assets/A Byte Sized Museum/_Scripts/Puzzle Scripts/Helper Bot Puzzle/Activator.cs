using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class Activator : MonoBehaviour
    {
        [SerializeField] private Color activatedColor;
        [SerializeField] private Color deactivatedColor;

        private Renderer rend;
        
        private Vector3 raycastOrigin;
        private Vector3 raycastDirection;
        private float raycastDistance;

        private void Start()
        {
            rend = GetComponent<Renderer>();
            rend.material.color = deactivatedColor;

            raycastOrigin = transform.position;
            raycastDirection = transform.up;
            raycastDistance = 1.0f;
        }

        private void Update()
        {
            if (FireRayCast(out GameObject hitObject))
            {
                if (hitObject.TryGetComponent(out HelperBotPuzzleObject puzzleObject))
                {
                    rend.material.color = activatedColor;

                    // TODO: GameEvent for signaling something na na-activate na yung activator
                }

                else 
                {
                    rend.material.color = deactivatedColor;
                }
            }
            else
            {
                rend.material.color = deactivatedColor;
            }

        }
        
        private bool FireRayCast(out GameObject hitObject)
        {
            if (Physics.Raycast(raycastOrigin, raycastDirection, out RaycastHit hit, raycastDistance))
            {
                hitObject = hit.collider.gameObject;
                return true;
            }
            hitObject = null;
            return false;
        }
    }
}