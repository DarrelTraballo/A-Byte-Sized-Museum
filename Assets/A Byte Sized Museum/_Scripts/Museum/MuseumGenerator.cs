using System.Collections.Generic;
using KaChow.WFC;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class MuseumGenerator : MonoBehaviour
    {
        public static MuseumGenerator Instance { get; private set; }
        #region Singleton
        private MuseumGenerator() { }
        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;
        }
        #endregion

        private GameManager gameManager;

        [Header("Museum Layout Generation")]
        [SerializeField] private Transform exhibitParent;
        [SerializeField] private Museum museum;
        [SerializeField] private Cell cellObj;

        private WaveFunctionCollapse WFC;

        private float exhibitSize;

        [Space]
        [SerializeField] private List<GameObject> exhibitList;
        private int puzzleExhibitAmount;
        [HideInInspector] public List<Cell> puzzleExhibitCells;

        public void Initialize()
        {
            exhibitSize = museum.exhibitPrefabs[0].gameObject.transform.GetChild(0).localScale.x;
            WFC = new WaveFunctionCollapse(museum, cellObj, exhibitParent.gameObject, museum.exhibitPrefabs);

            gameManager = GameManager.Instance;
            puzzleExhibitAmount = gameManager.PuzzleExhibitAmount;
        }

        // TODO: set up code structure for Exhibits
        public void GenerateExhibits()
        {
            WFC.InitializeGrid();
            WFC.DisableExhibits();
            WFC.CheckEdges();
            WFC.ToggleExhibit(WFC.gridComponents[12], false);
            GeneratePuzzleExhibits(WFC.gridComponents);


        }

        public void GeneratePuzzleExhibits(List<Cell> gridComponents)
        {
            puzzleExhibitCells = new List<Cell>();
            List<Cell> gridComponentsCopy = new List<Cell>(gridComponents);

            int excludedIndex = 12;
            gridComponentsCopy.RemoveAt(excludedIndex);

            for (int i = 0; i < puzzleExhibitAmount; i++)
            {
                int randomIndex = Random.Range(0, gridComponentsCopy.Count);
                var potentialExhibitCell = gridComponentsCopy[randomIndex];

                // Check if the cell has an Exhibit component

                var potentialExhibit = potentialExhibitCell.GetComponentInChildren<Exhibit>();

                if (potentialExhibit == null || potentialExhibit.isPuzzleExhibit)
                {
                    --i;
                    continue;
                }

                potentialExhibit.InitializePuzzleExhibit();
                potentialExhibit.SetIsPuzzleExhibit(true);
                potentialExhibit.TogglePuzzleExhibit();


                puzzleExhibitCells.Add(potentialExhibitCell);
                gridComponentsCopy.RemoveAt(randomIndex);

            }
        }

        private void GenerateExhibitsNoWFC()
        {
            // Calculate center position
            float cellCenterZ = (museum.museumExhibitSize / 2f) + 5f;
            float cellCenterX = (museum.museumExhibitSize / 2f) + 5f;

            // Calculate offset
            float offsetX = (museum.museumExhibitSize - exhibitSize) / 2f;
            float offsetZ = (museum.museumExhibitSize - exhibitSize) / 2f;

            // Align to the center of the grid cells
            Vector3 gridOffset = new Vector3(0, 0, -(museum.museumSize * museum.museumExhibitSize / 2));

            for (int z = 0; z < museum.museumSize; z++)
            {
                for (int x = 0; x < museum.museumSize; x++)
                {
                    Vector3 position = new Vector3(x * museum.museumExhibitSize + cellCenterX - offsetX, -1, z * museum.museumExhibitSize + cellCenterZ - offsetZ) + gridOffset;
                    GameObject spawnedExhibit = Instantiate(museum.exhibitPrefabs[0].gameObject, position, Quaternion.identity, exhibitParent);
                    spawnedExhibit.name = $"Exhibit {z * museum.museumSize + x}";

                    exhibitList.Add(spawnedExhibit);
                }
            }
        }

        #region OnDrawGizmos()
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            DrawGrid();
        }

        private void DrawGrid()
        {
            Gizmos.color = Color.black;

            Vector3 offset = new Vector3(0, 0, -(museum.museumExhibitSize * museum.museumSize) / 2);

            Vector3 origin = transform.position + offset;

            float cellWidth = museum.museumExhibitSize * museum.museumSize;
            float cellHeight = museum.museumExhibitSize * museum.museumSize;

            // vertical grid lines
            for (int i = 0; i <= museum.museumSize; i++)
            {
                Vector3 start = origin + new Vector3(i * museum.museumExhibitSize, 0, 0);
                Vector3 end = start + new Vector3(0, 0, cellHeight);
                Gizmos.DrawLine(start, end);
            }

            // vertical grid lines
            for (int i = 0; i <= museum.museumSize; i++)
            {
                Vector3 start = origin + new Vector3(0, 0, i * museum.museumExhibitSize);
                Vector3 end = start + new Vector3(cellWidth, 0, 0);
                Gizmos.DrawLine(start, end);
            }
        }
#endif
        #endregion
    }
}


