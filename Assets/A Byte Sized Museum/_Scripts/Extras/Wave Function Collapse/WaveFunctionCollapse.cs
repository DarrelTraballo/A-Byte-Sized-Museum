using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static KaChow.AByteSizedMuseum.MuseumGenerator;

namespace KaChow.WFC {
    public class WaveFunctionCollapse : MonoBehaviour
    {
        [Header("Algorithm Variables")]
        private int dimensions;

        [Header("Tiles")]
        [SerializeField]
        private Tile[] tileObjects;

        [Header("Grid Variables")]
        public Cell cellObj;

        [HideInInspector]
        public List<Cell> gridComponents;

        private int iterations = 0;

        public void InitializeGrid(Museum museum, GameObject parent, Tile[] tileObjects)
        {
            gridComponents = new List<Cell>();
            dimensions = (int) museum.museumSize;
            this.tileObjects = tileObjects;

            float exhibitSize = tileObjects[0].gameObject.transform.GetChild(0).localScale.x;

            // Calculate center position
            float cellCenterZ = (museum.museumExhibitSize / 2f) + 5f;
            float cellCenterX = (museum.museumExhibitSize / 2f) + 5f;

            // Calculate offset
            float offsetX = (museum.museumExhibitSize - exhibitSize) / 2f;
            float offsetZ = (museum.museumExhibitSize - exhibitSize) / 2f;

            // Align to the center of the grid cells
            Vector3 gridOffset = new Vector3(0, 0, -(museum.museumSize * museum.museumExhibitSize / 2));

            for (int x = 0; x < museum.museumSize; x++)
            {
                for (int z = 0; z < museum.museumSize; z++)
                {
                    Vector3 position = new Vector3(x * museum.museumExhibitSize + cellCenterX - offsetX, -1, z * museum.museumExhibitSize + cellCenterZ - offsetZ) + gridOffset;
                    Cell newCell = Instantiate(cellObj, position, Quaternion.identity, parent.transform);
                    newCell.CreateCell(false, tileObjects);
                    newCell.name = "Exhibit";
                    gridComponents.Add(newCell);
                }
            }

            StartCoroutine(CheckEntropy());
            // CheckEntropy();
        }

        private IEnumerator CheckEntropy()
        // private void CheckEntropy()
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

            yield return new WaitForSeconds(0.01f);

            CollapseCell(tempGrid);        
        }

        private void CollapseCell(List<Cell> tempGrid)
        {
            int randIndex = UnityEngine.Random.Range(0, tempGrid.Count);

            Cell cellToCollapse = tempGrid[randIndex];

            cellToCollapse.isCollapsed = true;
            // FIXME: IndexOutOfRangeException: Index was outside the bounds of the array.
            Tile selectedTile = cellToCollapse.tileOptions[UnityEngine.Random.Range(0, cellToCollapse.tileOptions.Length-1)];
            cellToCollapse.tileOptions = new Tile[] { selectedTile };

            Tile foundTile = cellToCollapse.tileOptions[0];
            Instantiate(foundTile, cellToCollapse.transform.position, Quaternion.identity, cellToCollapse.transform);

            UpdateGeneration();
        }

        private void UpdateGeneration()
        {
            List<Cell> newGenerationCell = new List<Cell>(gridComponents);

            for (int x = 0; x < dimensions; x++)
            {
                for (int z = 0; z < dimensions; z++)
                {
                    var index = z + x * dimensions;
                    // FIXME: fix index out of range shenanigans somewhere here, idk\
                    // ArgumentOutOfRangeException: Index was out of range. Must be non-negative and less than the size of the collection.
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
                        if (x > 0)
                        {
                            Cell up = gridComponents[z + (x - 1) * dimensions];
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
                        if (z < dimensions - 1)
                        {
                            Cell right = gridComponents[z + 1 + x * dimensions];
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
                        if (x < dimensions - 1)
                        {
                            Cell down = gridComponents[z + (x + 1) * dimensions];
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
                        if (z > 0)
                        {
                            Cell left = gridComponents[z - 1 + x * dimensions];
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
                // CheckEntropy();
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

        private void AdjustCamera() 
        {
            Vector3 center = new Vector3((dimensions - 1) / 2f, dimensions + 1, (dimensions - 1) / 2f);
            Camera.main.transform.position = center;

            Vector3 currentRotation = Camera.main.transform.rotation.eulerAngles;
            float newXRotation = 90f;
            Quaternion newRotation = Quaternion.Euler(newXRotation, currentRotation.y, currentRotation.z);

            Camera.main.transform.rotation = newRotation;
        }
    }
}