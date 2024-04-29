using System.Linq;
using KaChow.WFC;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class Exhibit : MonoBehaviour
    {
        [SerializeField] private PuzzleSetData puzzles;
        [SerializeField] private Transform puzzleHolder;
        [SerializeField] private GameObject miniMapTiles;

        public bool isPuzzleExhibit = false;
        public GameObject pathwayGuide;
        public GameObject sign;
        private MeshRenderer signMesh;

        [Space]
        public GameObject topBlock;
        public GameObject topPath;

        public GameObject rightBlock;
        public GameObject rightPath;

        public GameObject bottomBlock;
        public GameObject bottomPath;

        public GameObject leftBlock;
        public GameObject leftPath;

        private bool isPuzzleSolved = false;
        private static bool hasEnteredAPuzzleExhibit = false;
        [SerializeField] private GameEvent onFirstPuzzleExhibitEnter;

        [Space]
        [SerializeField] private Color baseColor;
        [SerializeField] private Color puzzleColor;
        [SerializeField] private Color solvedColor;

        private void Awake()
        {
            sign.SetActive(isPuzzleExhibit);
            signMesh = sign.GetComponent<MeshRenderer>();
            // signMesh.GetComponent<MeshRenderer>().enabled = false;
        }

        private void Update()
        {
            if (isPuzzleSolved && sign.activeSelf)
            {
                Debug.Log("This should run");
                sign.SetActive(false);
                return;
            }
        }

        private void Start()
        {
            hasEnteredAPuzzleExhibit = false;
            miniMapTiles.SetActive(false);
        }

        public void InitializePuzzleExhibit()
        {
            // puzzleInterpreterAndCam = puzzleHolder.GetChild(0).gameObject;
            // puzzleInterpreterAndCam.SetActive(false);
            // sign.SetActive(true);
        }

        public void TogglePuzzleExhibit()
        {
            if (isPuzzleExhibit)
            {
                SetPathColor(puzzleColor);
                sign.SetActive(true);
                // signMesh.GetComponent<MeshRenderer>().enabled = true;


                // puzzleInterpreterAndCam.SetActive(true);

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
                if (pathwayObject.TryGetComponent<Renderer>(out var renderer))
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

            isPuzzleSolved = true;

            Debug.LogWarning("Set Path Color");
            var exhibit = exhibitCell.GetComponentInChildren<Exhibit>();
            ToggleSign();
            // signMesh.GetComponent<MeshRenderer>().enabled = false;

            exhibit.SetPathColor(solvedColor);
        }

        public void SetExhibitSolved()
        {
            isPuzzleSolved = true;

            ToggleSign();
            SetPathColor(solvedColor);
        }

        private void ToggleSign()
        {
            if (isPuzzleSolved)
            {
                Debug.Log("This should run");
                // signMesh.GetComponent<MeshRenderer>().enabled = false;
                sign.SetActive(false);
            }
        }
    }
}
