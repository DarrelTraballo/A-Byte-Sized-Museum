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
            [Tooltip("WFC Tiles")]
            public Tile[] exhibitPrefabs;
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

        private WaveFunctionCollapse WFC;

        public void Initialize() 
        {
            WFC = new WaveFunctionCollapse(museum, cellObj, exhibitParent.gameObject, museum.exhibitPrefabs);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                GenerateExhibits();
            else if (Input.GetKeyDown(KeyCode.E))
                WFC.Clear();
        }
 
        // TODO: set up code structure for Exhibits
        public void GenerateExhibits()
        {
            // Generate Museum Layout using WFC
            WFC.InitializeGrid();
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
