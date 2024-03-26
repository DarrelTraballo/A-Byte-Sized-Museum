using System.Linq;
using KaChow.WFC;
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

        [Space]
        public GameObject topBlock;
        public GameObject rightBlock;
        public GameObject bottomBlock;
        public GameObject leftBlock;

        private bool isPuzzleSolved;
        private static bool hasEnteredAPuzzleExhibit = false;
        [SerializeField] private GameEvent onFirstPuzzleExhibitEnter;

        private void Start()
        {
            hasEnteredAPuzzleExhibit = false;
        }

        public void InitializePuzzleExhibit()
        {
            puzzleInterpreterAndCam = puzzleHolder.GetChild(0).gameObject;
            puzzleInterpreterAndCam.SetActive(false);
        }

        public void TogglePuzzleExhibit()
        {
            if (isPuzzleExhibit)
            {
                SetPathColor(Color.red);

                puzzleInterpreterAndCam.SetActive(true);

                int selectedPuzzleSet = Random.Range(0, puzzles.puzzleSets.Count);
                Instantiate(puzzles.puzzleSets[selectedPuzzleSet], puzzleHolder);
            }
            else
            {
                Debug.LogError("An exhibit not set as a puzzle exhibit is being being toggled as a puzzle exhibit.");
            }
        }

        public void SetPathColor(Color color)
        {
            for (int i = 0; i < pathwayGuide.transform.childCount; i++)
            {
                GameObject pathwayObject = pathwayGuide.transform.GetChild(i).gameObject;
                Renderer renderer = pathwayObject.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = color;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && isPuzzleExhibit && !hasEnteredAPuzzleExhibit)
            {
                onFirstPuzzleExhibitEnter.Raise(this, this);

                hasEnteredAPuzzleExhibit = true;
            }
        }

        public void SetIsPuzzleExhibit(bool isPuzzleExhibit) => this.isPuzzleExhibit = isPuzzleExhibit;

        public void SetPathColorSolved(Component sender, object data)
        {
            if (!isPuzzleExhibit || data is not Cell exhibitCell) return;

            var exhibit = exhibitCell.GetComponentInChildren<Exhibit>();

            exhibit.SetPathColor(Color.green);
        }
    }
}
