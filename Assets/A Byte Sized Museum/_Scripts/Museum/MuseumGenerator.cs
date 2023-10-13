using KaChow.Extras;
using KaChow.WFC;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class MuseumGenerator : MonoBehaviour
    {
        [System.Serializable]
        public class Museum
        {
            public float museumSize;
            public float museumExhibitSize = 40f;
            public Tile[] exhibitPrefabs;
            [Tooltip("WFC Tiles")]
            [HideInInspector] public Vector3 exhibitSize;
        }

        // make singleton

        [Header("Museum Layout Generation")]
        [SerializeField] private Transform exhibitParent;
        [SerializeField] private Museum museum;

        private WaveFunctionCollapse WFC;

        private Vector3 exhibitSize;

        public void Initialize() 
        {
            exhibitSize = museum.exhibitPrefabs[0].gameObject.transform.localScale;
            WFC = GetComponent<WaveFunctionCollapse>();
        }
 
        // TODO: set up code structure for Exhibits
        public void GenerateExhibits()
        {
            // Generate Museum Layout using WFC
            WFC.InitializeGrid(museum, exhibitParent.gameObject, museum.exhibitPrefabs);
            // GenerateExhibitsNoWFC();

            int startExhibitIndex = (int)(0.5f * WFC.dimensions * WFC.dimensions - 1.5f * WFC.dimensions + WFC.dimensions);
            int finalExhibitIndex = startExhibitIndex + WFC.dimensions - 1;

            var startExhibitNode = WFC.gridComponents[startExhibitIndex].transform.GetChild(0).GetComponent<Node>();
            var finalExhibitNode = WFC.gridComponents[finalExhibitIndex].transform.GetChild(0).GetComponent<Node>();

            var pathExists = AStarPathfinding.PathExists(startExhibitNode, finalExhibitNode);

            Debug.Log($"path Exists? : {pathExists}");

            // TODO: A* pathfinding to check if there is a path towards final exhibit or 
            //              one of the non-corner rooms in the opposite side of the grid.
            //       if no path is found, generate again
            //      maybe make final exhibit not fixed in one position
            //      could just have position.x fixed and position.z to be random

            // TODO: Generate Hallways from path generated from A*

            // TODO: Probably 
        }

        private void GenerateExhibitsNoWFC()
        {
            // Calculate center position
            float cellCenterZ = (museum.museumExhibitSize / 2f) + 5f;
            float cellCenterX = (museum.museumExhibitSize / 2f) + 5f;

            // Calculate offset
            float offsetX = (museum.museumExhibitSize - exhibitSize.x) / 2f;
            float offsetZ = (museum.museumExhibitSize - exhibitSize.z) / 2f;

            // Align to the center of the grid cells
            Vector3 gridOffset = new Vector3(0, 0, -(museum.museumSize * museum.museumExhibitSize / 2));

            for (int x = 0; x < museum.museumSize; x++)
            {
                for (int z = 0; z < museum.museumSize; z++)
                {
                    Vector3 position = new Vector3(x * museum.museumExhibitSize + cellCenterX - offsetX, -1, z * museum.museumExhibitSize + cellCenterZ - offsetZ) + gridOffset;
                    Instantiate(museum.exhibitPrefabs[0], position, Quaternion.identity, exhibitParent);
                }
            }
        }
       
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
    }
}
