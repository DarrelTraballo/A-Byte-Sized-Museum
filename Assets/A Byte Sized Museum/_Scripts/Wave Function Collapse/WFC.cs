using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
// using Unity.Mathematics;
using UnityEngine;

namespace KaChow.WFC {
    public class WaveFunctionCollapse : MonoBehaviour
    {
        [Header("Algorithm Variables")]
        public int dimensions;
        [SerializeField] private float generationSpeed;

        [Header("Tiles")]
        public Tile[] tileObjects;

        [Header("Grid Variables")]
        public Cell cellObj;
        public List<Cell> gridComponents;

        int iterations = 0;

        [SerializeField] public static bool IsDoneGenerating;

        private void Awake()
        {
            gridComponents = new List<Cell>();

            // AdjustCamera();

            InitializeGrid();
            IsDoneGenerating = false;
        }

        private void AdjustCamera() 
        {
            Vector3 center = new Vector3((dimensions - 1) / 2f, dimensions + 1, (dimensions - 1) / 2f);
            Camera.main.transform.position = center;

            Vector3 currentRotation = Camera.main.transform.rotation.eulerAngles;
            float newXRotation = 90f;
            Quaternion newRotation = Quaternion.Euler(newXRotation, currentRotation.y, currentRotation.z);

            Camera.main.transform.rotation = newRotation;
        }

        private void InitializeGrid()
        {
            GameObject map = new GameObject("Map");

            for (int y = 0; y < dimensions; y++)
            {
                for (int x = 0; x < dimensions; x++)
                {
                    Cell newCell = Instantiate(cellObj, new Vector3(x, 0, y), Quaternion.identity, map.transform);
                    newCell.CreateCell(false, tileObjects);
                    gridComponents.Add(newCell);
                }
            }

            StartCoroutine(CheckEntropy());
        }


        private IEnumerator CheckEntropy()
        {
            List<Cell> tempGrid = new List<Cell>(gridComponents);

            tempGrid.RemoveAll(c => c.isCollapsed);

            tempGrid.Sort((a, b) => { return a.tileOptions.Length - b.tileOptions.Length; });

            int arrLength = tempGrid[0].tileOptions.Length;
            int stopIndex = default;

            for (int i = 1; i < tempGrid.Count; i++)
            {
                if (tempGrid[i].tileOptions.Length > arrLength)
                {
                    stopIndex = i;
                    break;
                }
            }

            if (stopIndex > 0)
            {
                tempGrid.RemoveRange(stopIndex, tempGrid.Count - stopIndex);
            }

            yield return new WaitForSeconds(generationSpeed);

            CollapseCell(tempGrid);        
        }

        // private void CollapseCell(List<Cell> tempGrid)
        // {
        //     int randIndex = UnityEngine.Random.Range(0, tempGrid.Count);

        //     Cell cellToCollapse = tempGrid[randIndex];

        //     cellToCollapse.isCollapsed = true;
        //     Tile selectedTile = cellToCollapse.tileOptions[UnityEngine.Random.Range(0, cellToCollapse.tileOptions.Length-1)];
        //     cellToCollapse.tileOptions = new Tile[] { selectedTile };

        //     Tile foundTile = cellToCollapse.tileOptions[0];
        //     Instantiate(foundTile, cellToCollapse.transform.position, Quaternion.identity, cellToCollapse.transform);

        //     UpdateGeneration();
        // }

        private void CollapseCell(List<Cell> tempGrid)
        {
            // Create a list of cells that have tile options
            List<Cell> cellsWithTileOptions = tempGrid.Where(cell => cell.tileOptions.Length > 0).ToList();

            // if (cellsWithTileOptions.Count == 0)
            // {
            //     // Handle the case where there are no cells with tile options
            //     return;
            // }

            int randIndex = UnityEngine.Random.Range(0, cellsWithTileOptions.Count);
            Cell cellToCollapse = cellsWithTileOptions[randIndex];

            cellToCollapse.isCollapsed = true;
            Tile selectedTile = cellToCollapse.tileOptions[UnityEngine.Random.Range(0, cellToCollapse.tileOptions.Length)];
            cellToCollapse.tileOptions = new Tile[] { selectedTile };

            Tile foundTile = cellToCollapse.tileOptions[0];
            Instantiate(foundTile, cellToCollapse.transform.position, Quaternion.identity, cellToCollapse.transform);

            UpdateGeneration();
        }

        private void UpdateGeneration()
        {
            List<Cell> newGenerationCell = new List<Cell>(gridComponents);

            for (int y = 0; y < dimensions; y++)
            {
                for (int x = 0; x < dimensions; x++)
                {
                    var index = x + y * dimensions;
                    if (gridComponents[index].isCollapsed)
                    {
                        newGenerationCell[index] = gridComponents[index];
                    }
                    else
                    {
                        List<Tile> options = new List<Tile>();
                        foreach (Tile t in tileObjects)
                        {
                            options.Add(t);
                        }

                        //update above
                        if (y > 0)
                        {
                            Cell up = gridComponents[x + (y - 1) * dimensions];
                            List<Tile> validOptions = new List<Tile>();

                            foreach (Tile possibleOptions in up.tileOptions)
                            {
                                var valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
                                var valid = tileObjects[valOption].upNeighbors;

                                validOptions = validOptions.Concat(valid).ToList();
                            }

                            CheckValidity(options, validOptions);
                        }

                        //update right
                        if (x < dimensions - 1)
                        {
                            Cell right = gridComponents[x + 1 + y * dimensions];
                            List<Tile> validOptions = new List<Tile>();

                            foreach (Tile possibleOptions in right.tileOptions)
                            {
                                var valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
                                var valid = tileObjects[valOption].leftNeighbors;

                                validOptions = validOptions.Concat(valid).ToList();
                            }

                            CheckValidity(options, validOptions);
                        }

                        //look down
                        if (y < dimensions - 1)
                        {
                            Cell down = gridComponents[x + (y + 1) * dimensions];
                            List<Tile> validOptions = new List<Tile>();

                            foreach (Tile possibleOptions in down.tileOptions)
                            {
                                var valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
                                var valid = tileObjects[valOption].downNeighbors;

                                validOptions = validOptions.Concat(valid).ToList();
                            }

                            CheckValidity(options, validOptions);
                        }

                        //look left
                        if (x > 0)
                        {
                            Cell left = gridComponents[x - 1 + y * dimensions];
                            List<Tile> validOptions = new List<Tile>();

                            foreach (Tile possibleOptions in left.tileOptions)
                            {
                                var valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
                                var valid = tileObjects[valOption].rightNeighbors;

                                validOptions = validOptions.Concat(valid).ToList();
                            }

                            CheckValidity(options, validOptions);
                        }

                        Tile[] newTileList = new Tile[options.Count];

                        for (int i = 0; i < options.Count; i++)
                        {
                            newTileList[i] = options[i];
                        }

                        newGenerationCell[index].RecreateCell(newTileList);
                    }
                }
            }

            gridComponents = newGenerationCell;
            iterations++;

            if(iterations < dimensions * dimensions)
            {
                StartCoroutine(CheckEntropy());
            }

            else
            {
                IsDoneGenerating = !IsDoneGenerating;
            }
        }

        private void CheckValidity(List<Tile> optionList, List<Tile> validOption)
        {
            for (int x = optionList.Count - 1; x >= 0; x--)
            {
                var element = optionList[x];
                if (!validOption.Contains(element))
                {
                    optionList.RemoveAt(x);
                }
            }
        }
    }
}