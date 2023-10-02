using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace KaChow.AByteSizedMuseum
{
    public class ExhibitGenerator : MonoBehaviour
    {
        [SerializeField]
        [Range(15, 30)]
        private int minGridSize;

        [SerializeField]
        [Range(15, 30)]
        private int maxGridSize;

        [SerializeField] 
        private GameObject gridCellPrefab;
        [SerializeField]
        private GameObject wallPrefab;

        private void Start() 
        {
            int x = Random.Range(minGridSize, maxGridSize + 1);
            int z = Random.Range(minGridSize, maxGridSize + 1);

            GameObject exhibit = new GameObject("Exhibit");

            GenerateGrid(x, z, exhibit);
            GenerateGrid(x, z, exhibit, isFloor: false);

            GenerateWalls(x, z, exhibit);
            Debug.Log($"Generating {x} x {z} grid");
        }


        private void GenerateGrid(int rows, int columns,  GameObject parent, bool isFloor = true)
        {
            // insert WFC generation code here
            GameObject floor = new GameObject(isFloor ? "Floor" : "Ceiling");

            int height = isFloor ? 0 : 6;

            for (int x = 0; x < rows; x++) 
            {
                for (int z = 0; z < columns; z++)
                {
                    Vector3 position = new Vector3(x, height, z);

                    var gridCell = Instantiate(gridCellPrefab, position, Quaternion.identity, floor.transform);
                }
            }

            floor.transform.parent = parent.transform;
        }

        // Try if it is possible to have just one wall for each face, and just scale it depending on row and col size
        private void GenerateWalls(int rows, int columns, GameObject parent)
        {
            GameObject walls = new GameObject("Walls");

            // corners
            InstantiateWall(new Vector3(-1, 0, -1), walls);
            InstantiateWall(new Vector3(rows, 0, -1), walls);
            InstantiateWall(new Vector3(-1, 0, columns), walls);
            InstantiateWall(new Vector3(rows, 0, columns), walls);

            for (int x = 0; x < rows; x++)
            {
                // Left Wall
                InstantiateWall(new Vector3(x, 0, -1), walls);

                // Right Wall
                InstantiateWall(new Vector3(x, 0, columns), walls);
            }

            for (int z = 0; z < columns; z++)
            {
                // Left Wall
                InstantiateWall(new Vector3(-1, 0, z), walls);

                // Right Wall
                InstantiateWall(new Vector3(rows, 0, z), walls);
            }
            walls.transform.parent = parent.transform;
        }

        private void InstantiateWall(Vector3 position, GameObject parent)
        {
            Instantiate(wallPrefab, position, Quaternion.identity, parent.transform);
        }
    }
}
