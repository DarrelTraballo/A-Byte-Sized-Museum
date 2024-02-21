using System.Linq;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class Exhibit : MonoBehaviour
    {
        [SerializeField] private ExhibitDoor[] doors;
        [SerializeField] private bool isExhibitLocked;

        [HideInInspector] public bool isPuzzleExhibit = false;
        public GameObject pathwayGuide;

        private bool isPuzzleSolved;

        private void Update()
        {
            // if (Input.GetKeyDown(KeyCode.Q))
            // {
            //     isExhibitLocked = !isExhibitLocked;



            //     // ToggleDoors(isExhibitLocked);
            // }
        }

        public void TogglePuzzleExhibit()
        {
            if (isPuzzleExhibit)
            {
                for (int i = 0; i < pathwayGuide.transform.childCount; i++)
                {
                    GameObject pathwayObject = pathwayGuide.transform.GetChild(i).gameObject;
                    Renderer renderer = pathwayObject.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.color = Color.blue;
                    }
                }
            }
            else
            {
                Debug.LogError("An exhibit not set as a puzzle exhibit is being being toggled as a puzzle exhibit.");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // isExhibitLocked = true;
            // ToggleDoors(isExhibitLocked);
        }

        private void OnTriggerExit(Collider other)
        {
            // isExhibitLocked = false;
            // ToggleDoors(isExhibitLocked);

        }

        private void ToggleDoors(bool isOpen)
        {
            foreach (var door in doors)
            {
                door.isLocked = isOpen;
            }
        }

        public void SetPuzzleSolved(bool isPuzzleSolved)
        {
            this.isPuzzleSolved = isPuzzleSolved;
        }
    }
}
