using System.Collections;
using System.Collections.Generic;
using KaChow.WFC;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class Activator : MonoBehaviour
    {
        [SerializeField] private Color activatedColor;
        [SerializeField] private Color deactivatedColor;
        private bool isSolved;

        private Renderer rend;

        private Vector3 raycastOrigin;
        private Vector3 raycastDirection;
        private float raycastDistance;

        public bool IsSolved
        {
            get { return isSolved; }
            private set { isSolved = value; }
        }

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
                if (!IsSolved)
                {
                    AudioManager.Instance.sfxSource.Stop();
                    AudioManager.Instance.PlaySFX("Completed");

                    var cell = GetComponentInParent<Cell>();

                    onActivatorActivated.Raise(this, cell);
                    IsSolved = true;
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
