using System.Linq;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class Exhibit : MonoBehaviour
    {
        [SerializeField] private ExhibitDoor[] doors;
        [SerializeField] private bool isExhibitLocked;

        [SerializeField] private PuzzleSetData puzzles;
        [SerializeField] private Transform puzzleHolder;
        private GameObject puzzleInterpreterAndCam;

        [HideInInspector] public bool isPuzzleExhibit = false;
        public GameObject pathwayGuide;

        private bool isPuzzleSolved;

        public void InitializeExhibit()
        {
            puzzleInterpreterAndCam = puzzleHolder.GetChild(0).gameObject;
            puzzleInterpreterAndCam.SetActive(false);
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

                puzzleInterpreterAndCam.SetActive(true);

                // TODO: Make random selection once more puzzle sets have been created
                Instantiate(puzzles.puzzleSets[2], puzzleHolder);
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
