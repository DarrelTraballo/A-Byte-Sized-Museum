using System.Collections.Generic;
using KaChow.WFC;
using Unity.VisualScripting;
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
            [Tooltip("WFC Tiles")]
            public Tile[] exhibitPrefabs;
            // public ExhibitData[] exhibits;
            [HideInInspector] public Vector3 exhibitSize;
        }

        // MuseumGenerator.Instance to access MuseumGenerator variables
        public static MuseumGenerator Instance { get; private set; }
        #region Singleton
        private MuseumGenerator() {}
        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;
        }
        #endregion

        [Header("Museum Layout Generation")]
        [SerializeField] private Transform exhibitParent;
        [SerializeField] private Museum museum;
        [SerializeField] private Cell cellObj;

        [Space]
        [SerializeField] private bool enableWFC;

        private float exhibitSize;

        private WaveFunctionCollapse WFC;

        [Space]
        [SerializeField]
        private List<GameObject> exhibitList;
        private int currentRoomIndex;

        public void Initialize() 
        {
            exhibitSize = museum.exhibitPrefabs[0].gameObject.transform.GetChild(0).localScale.x;
            WFC = new WaveFunctionCollapse(museum, cellObj, exhibitParent.gameObject, museum.exhibitPrefabs);

            currentRoomIndex = (int)(0.5f * museum.museumSize * museum.museumSize - 1.5f * museum.museumSize + museum.museumSize);
        }
 
        // TODO: set up code structure for Exhibits
        public void GenerateExhibits()
        {
            // Generate Museum Layout using WFC
            if (enableWFC)
                // WFC.InitializeGrid();
                GenerateExhibitsNoWFC();

            // for testing purposes
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


