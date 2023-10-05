using UnityEngine;
using KaChow.WFC;

namespace KaChow.AByteSizedMuseum
{
    public class ExhibitGenerator : MonoBehaviour
    {
        [Header("WFC Variables")]
        [SerializeField] private Tile[] gridTilePrefabs;

        [Header("Wall")]
        [SerializeField] private GameObject wallPrefab;

        [SerializeField] private Transform museumTransform;
        private float tileSize;

        public GameObject GenerateExhibit(Vector3 startPos, int rows, int columns, int id) 
        {
            // Debug.Log($"Generated {rows} x {columns} Exhibit");

            GameObject exhibit = new GameObject($"Exhibit {id}")
            {
                tag = "Exhibit"
            };
            exhibit.transform.position = startPos;

            tileSize = gridTilePrefabs[0].transform.localScale.x;

            GenerateGrid(startPos, rows, columns, exhibit);

            GenerateWalls(startPos, rows, columns, exhibit);

            exhibit.transform.parent = museumTransform;

            return exhibit;
        }

        private void GenerateGrid(Vector3 startPos, int rows, int columns,  GameObject parent)
        {           
            GameObject floor = new GameObject("Floor");
            
            // WFC
            // GameManager.Instance.WFC.InitializeGrid(rows, columns, floor, gridTilePrefabs);

            for (int x = (int) startPos.x; x < rows + (int) startPos.x; x++)
            {
                for (int z = (int) startPos.z; z < columns + (int) startPos.z; z++)
                {
                    Vector3 position = new Vector3(x * tileSize, 0, z * tileSize);
                    var gridCell = Instantiate(gridTilePrefabs[0], position, Quaternion.identity, floor.transform);

                    // for debugging purposes
                    gridCell.name = $"Grid Cell ({x * tileSize}, {z * tileSize})";
                }
            }

            floor.transform.parent = parent.transform;
        }

        // Doors where?
        // gawa nalang different wall prefabs
        private void GenerateWalls(Vector3 startPos, int rows, int columns, GameObject parent)
        {
            GameObject walls = new GameObject("Walls");

            // Create walls
            CreateWall("Back Wall", tileSize * rows, tileSize, new Vector3(rows - 1, 0f, 0) + (startPos * tileSize), walls);
            CreateWall("Front Wall", tileSize * rows, tileSize, new Vector3(rows - 1, 0f, columns * tileSize - tileSize) + (startPos * tileSize), walls);
            CreateWall("Left Wall", tileSize, tileSize * columns, new Vector3(0, 0f, columns - 1) + (startPos * tileSize), walls);
            CreateWall("Right Wall", tileSize, tileSize * columns, new Vector3(rows * tileSize - tileSize, 0f, columns - 1) + (startPos * tileSize), walls);

            walls.transform.parent = parent.transform;
        }

        private void CreateWall(string wallName, float width, float length, Vector3 position, GameObject parent)
        {
            GameObject wall = Instantiate(wallPrefab, position, Quaternion.identity, parent.transform);
            wall.transform.localScale = new Vector3(width, wall.transform.localScale.y, length);
            wall.name = wallName;
        }
    }
}
