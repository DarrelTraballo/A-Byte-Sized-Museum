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
            [HideInInspector] public Vector3 exhibitSize;
        }

        [Header("Museum Layout Generation")]
        [Tooltip("WFC Tiles")]
        [SerializeField] private Transform exhibitParent;
        [SerializeField] private Museum museum;
        // [SerializeField] private Tile[] exhibitPrefabs;

        private Vector3 exhibitSize;

        public void Initialize() 
        {
            exhibitSize = museum.exhibitPrefabs[0].gameObject.transform.localScale;
        }
 
        // TODO: set up code structure for Exhibits
        // TODO: Specify where the generation should start (in front of museum lobby)
        // TODO: pseudocode for museum layout generation
        // Generate using WFC
        // Choose which rooms to use
        public void GenerateExhibits()
        {
            // WFC
            GameManager.Instance.WFC.InitializeGrid(museum, exhibitParent.gameObject, museum.exhibitPrefabs);
            // GenerateExhibitsNoWFC();
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
