using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class Activator : MonoBehaviour
    {
        [SerializeField] private Color activatedColor;
        [SerializeField] private Color deactivatedColor;
        private bool isSolved = false;

        public bool tutorial = true;

        private Renderer rend;

        private Vector3 raycastOrigin;
        private Vector3 raycastDirection;
        private float raycastDistance;

        [SerializeField] private GameEvent onActivatorActivated;

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
            bool hitSuccess = FireRayCast(out GameObject hitObject);
            bool isPuzzleObject = hitSuccess && hitObject.TryGetComponent(out HelperBotPuzzleObject puzzleObject);

            if (isPuzzleObject)
            {
                AudioManager.Instance.PlaySFX("Completed");
                tutorial = false;

                if (!isSolved)
                {
                    onActivatorActivated.Raise(this, this);
                    isSolved = true;
                }
            }

            rend.material.color = isPuzzleObject ? activatedColor : deactivatedColor;
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
