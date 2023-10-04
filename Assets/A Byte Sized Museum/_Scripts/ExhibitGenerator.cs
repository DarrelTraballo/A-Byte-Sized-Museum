using UnityEngine;
using KaChow.WFC;

namespace KaChow.AByteSizedMuseum
{
    public class ExhibitGenerator : MonoBehaviour
    {
        [Header("WFC Variables")]
        [SerializeField]
        [Range(15, 30)]
        private int minGridSize;

        [SerializeField]
        [Range(15, 30)]
        private int maxGridSize;

        [SerializeField]
        private int rows;
        [SerializeField]
        private int cols;

        [Space]
        [SerializeField] 
        private Tile[] gridTilePrefabs;

        [Header("Wall")]
        [SerializeField]
        private GameObject wallPrefab;

        // TODO: 
        //      GENERATE A BUNCH OF THEM AT THE SAME TIME, 
        private void Start() 
        {
            rows = Random.Range(minGridSize, maxGridSize + 1);
            cols = Random.Range(minGridSize, maxGridSize + 1);
            Debug.Log($"Generating {rows} x {cols} grid");

            GenerateExhibit(rows, cols);
        }

        public void GenerateExhibit(int rows, int columns) 
        {
            GameObject exhibit = new GameObject("Exhibit");

            GenerateGrid(rows, columns, exhibit);

            // TODO: Generate ceiling, one prefab nalang i-initialize then stretch or something
            // GenerateGrid(x, z, exhibit, isFloor: false);

            GenerateWalls(rows, columns, exhibit);
        }

        private void GenerateGrid(int rows, int columns,  GameObject parent, bool isFloor = true)
        {           
            GameObject floor = new GameObject(isFloor ? "Floor" : "Ceiling");

            GameManager.Instance.WFC.InitializeGrid(rows, columns, floor, gridTilePrefabs);

            floor.transform.parent = parent.transform;
        }

        // Try if it is possible to have just one wall for each face, and just scale it depending on row and col size
        // Doors where?
        private void GenerateWalls(int rows, int columns, GameObject parent)
        {
            GameObject walls = new GameObject("Walls");

            float tileSize = 2f;

            // corners
            InstantiateWall(new Vector3(-tileSize, 0, -tileSize), walls);
            InstantiateWall(new Vector3(rows * tileSize, 0, -tileSize), walls);
            InstantiateWall(new Vector3(-tileSize, 0, columns * tileSize), walls);
            InstantiateWall(new Vector3(rows * tileSize, 0, columns * tileSize), walls);

            for (int x = 0; x < rows; x++)
            {
                // Left Wall
                InstantiateWall(new Vector3(x * tileSize, 0, -tileSize), walls);

                // Right Wall
                InstantiateWall(new Vector3(x * tileSize, 0, columns * tileSize), walls);
            }

            for (int z = 0; z < columns; z++)
            {
                // Left Wall
                InstantiateWall(new Vector3(-tileSize, 0, z * tileSize), walls);

                // Right Wall
                InstantiateWall(new Vector3(rows * tileSize, 0, z * tileSize), walls);
            }

            walls.transform.parent = parent.transform;
        }

        private void InstantiateWall(Vector3 position, GameObject parent)
        {
            Instantiate(wallPrefab, position, Quaternion.identity, parent.transform);
        }
    }
}
