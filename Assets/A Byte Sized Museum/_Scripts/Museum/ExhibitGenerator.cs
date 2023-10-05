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

        // TODO: 
        //      GENERATE A BUNCH OF THEM AT THE SAME TIME, 
        private void Start() 
        {
            Debug.Log($"start was called, Tile Size = {tileSize}");
            // GenerateExhibit(new Vector3(10, 0, 0), rows, cols, 0);
        }

        public void GenerateExhibit(Vector3 startPos, int rows, int columns, int id) 
        {
            Debug.Log($"Generated {rows} x {columns} Exhibit");

            GameObject exhibit = new GameObject($"Exhibit {id}");
            exhibit.transform.position = startPos;

            tileSize = gridTilePrefabs[0].transform.localScale.x;

            GenerateGrid(startPos, rows, columns, exhibit);

            GenerateWalls(startPos, rows, columns, exhibit);

            // TODO: Generate ceiling, one prefab nalang i-initialize then stretch or something
            // GenerateGrid(x, z, exhibit, isFloor: false);

            exhibit.transform.parent = museumTransform;
        }

        private void GenerateGrid(Vector3 startPos, int rows, int columns,  GameObject parent)
        {           
            GameObject floor = new GameObject("Floor");
            Debug.Log($"Tile Size = {tileSize}");
            
            // WFC
            // GameManager.Instance.WFC.InitializeGrid(rows, columns, floor, gridTilePrefabs);

            for (int x = (int) startPos.x; x < rows + (int) startPos.x; x++)
            {
                for (int z = (int) startPos.z; z < columns + (int) startPos.z; z++)
                {
                    Vector3 position = new Vector3(x * tileSize, 0, z * tileSize);
                    var gridCell = Instantiate(gridTilePrefabs[0], position, Quaternion.identity, floor.transform);
                    gridCell.name = $"Grid Cell ({x * tileSize}, {z * tileSize})";
                }
            }

            floor.transform.parent = parent.transform;
        }

        // Doors where?
        private void GenerateWalls(Vector3 startPos, int rows, int columns, GameObject parent)
        {
            GameObject walls = new GameObject("Walls");

            // Back Wall
            var backWall = Instantiate(wallPrefab, new Vector3(rows - 1, 0f, -tileSize) + (startPos * tileSize), Quaternion.identity, walls.transform);
            backWall.transform.localScale = new Vector3(tileSize * rows, backWall.transform.localScale.y, tileSize);
            backWall.name = "Back Wall";

            // Back Wall
            var frontWall = Instantiate(wallPrefab, new Vector3(rows - 1, 0f, columns * tileSize) + (startPos * tileSize), Quaternion.identity, walls.transform);
            frontWall.transform.localScale = new Vector3(tileSize * rows, frontWall.transform.localScale.y, tileSize);
            frontWall.name = "Front Wall";

            // Back Wall
            var leftWall = Instantiate(wallPrefab, new Vector3(-tileSize, 0f, columns - 1) + (startPos * tileSize), Quaternion.identity, walls.transform);
            leftWall.transform.localScale = new Vector3(tileSize, leftWall.transform.localScale.y, tileSize * columns);
            leftWall.name = "Left Wall";

            // Back Wall
            var rightWall = Instantiate(wallPrefab, new Vector3(rows * tileSize, 0f, columns - 1) + (startPos * tileSize), Quaternion.identity, walls.transform);
            rightWall.transform.localScale = new Vector3(tileSize, rightWall.transform.localScale.y, tileSize * columns);
            rightWall.name = "Left Wall";


            walls.transform.parent = parent.transform;
        }
    }
}
