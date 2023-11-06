using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using static KaChow.AByteSizedMuseum.MuseumGenerator;
using KaChow.AByteSizedMuseum;

namespace KaChow.WFC {
    public class WaveFunctionCollapse
    {
        private readonly int dimensions;
        private readonly Tile[] tileObjects;
        private readonly Museum museum;
        private readonly Cell cellObj;
        private readonly GameObject parentGO;

        // private ExhibitData[] exhibits;

        // list of rooms generated. starts bottom left, goes upwards until top right
        public List<Cell> gridComponents;

        private int iterations = 0;
        private readonly float exhibitSize;

        private bool isDoneGenerating = false;
        private bool firstCall = true;
        private bool secondCall = false;
        private bool thirdCall = false;

        public WaveFunctionCollapse(Museum museum, Cell cellObj, GameObject parentGO, Tile[] tileObjects)
        {
            gridComponents = new List<Cell>();
            dimensions = (int) museum.museumSize;
            this.museum = museum;
            this.cellObj = cellObj;
            this.parentGO = parentGO;
            // this.tileObjects = tileObjects;
            // this.exhibits = exhibits;

            // for (int i = 0; i < exhibits.Length; i++)
            // {
            //     tileObjects[i] = exhibits[i].rules;
            // }

            exhibitSize = tileObjects[0].gameObject.transform.GetChild(0).localScale.x;
        }

        // public void InitializeGrid(Museum museum, GameObject parent, Tile[] tileObjects)
        public void InitializeGrid()
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
                    Cell newCell = GameObject.Instantiate(cellObj, position, Quaternion.identity, parentGO.transform);
                    newCell.CreateCell(false, tileObjects);
                    // newCell.name = $"Exhibit ({x}, {z})";
                    newCell.name = $"Exhibit {z * museum.museumSize + x}";
                    gridComponents.Add(newCell);
                }
            }

            // MonoBehaviour.StartCoroutine(CheckEntropy());
            CheckEntropy();
        }

        private void CheckEntropy()
        // private IEnumerator CheckEntropy()
        {
            // creates a copy of gridComponents List
            List<Cell> tempGrid = new List<Cell>(gridComponents);

            // removes all collapsed Cells
            tempGrid.RemoveAll(c => c.isCollapsed);

            // sorts the List in ascending order based on how many Tile Options each cell has left
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

            // yield return new WaitForSeconds(0.01f);

            CollapseCell(tempGrid);        
        }

        private void CollapseCell(List<Cell> tempGrid)
        {
            Cell cellToCollapse;
            Tile selectedTile;

            // if first collapse, collapse tile in front of start exhibit to 4-way tile
            if (firstCall)
            {
                int startExhibitCellIndex = (int)(0.5f * dimensions * dimensions - 1.5f * dimensions + dimensions); 
                cellToCollapse = gridComponents[startExhibitCellIndex];
                selectedTile = cellToCollapse.tileOptions[0];
                firstCall = false;
                secondCall = true;
            }
            // TODO: if can, do secondCall and thirdCall on firstCall
            else if (secondCall)
            {
                int finalExhibitCellIndex = (int)(0.5f * dimensions * dimensions - 1.5f * dimensions + dimensions) + dimensions - 1;
                cellToCollapse = gridComponents[finalExhibitCellIndex];
                // select randomly between indices 3, 4, 5
                int randIndex = Random.Range(3, 6);
                // selectedTile = cellToCollapse.tileOptions[randIndex];
                selectedTile = cellToCollapse.tileOptions[4];
                secondCall = false;
                thirdCall = true;
            }
            else if (thirdCall)
            {
                int selectedExhibitCellIndex = dimensions - 2;
                cellToCollapse = gridComponents[selectedExhibitCellIndex];
                int randIndex = Random.Range(3, 6);
                selectedTile = cellToCollapse.tileOptions[3];
                thirdCall = false;
            }
            // else, collapses cells randomly based on entropy
            else
            {
                // picks which cell to collapse from tempGrid
                int randIndex = Random.Range(0, tempGrid.Count);
                cellToCollapse = tempGrid[randIndex];
                selectedTile = cellToCollapse.tileOptions[Random.Range(0, cellToCollapse.tileOptions.Length-1)];
            }

            // sets selected cell's isCollapsed to true
            // basically collapsing the cell
            cellToCollapse.isCollapsed = true;

            // selects a random tile from the selected cell's tileOptions
            cellToCollapse.tileOptions = new Tile[] { selectedTile };

            // sets cell to the selected tile
            Tile foundTile = cellToCollapse.tileOptions[0];
            // TODO: 
            GameObject.Instantiate(foundTile, cellToCollapse.transform.position, Quaternion.identity, cellToCollapse.transform);

            UpdateGeneration();
        }

        private void UpdateGeneration()
        {
            List<Cell> newGenerationCell = new List<Cell>(gridComponents);

            for (int z = 0; z < dimensions; z++)
            {
                for (int x = 0; x < dimensions; x++)
                {
                    var index = x + z * dimensions;
                    
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
                        if (z > 0)
                        {
                            Cell up = gridComponents[x + (z - 1) * dimensions];
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
                            Cell right = gridComponents[x + 1 + z * dimensions];
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
                        if (z < dimensions - 1)
                        {
                            Cell down = gridComponents[x + (z + 1) * dimensions];
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
                            Cell left = gridComponents[x - 1 + z * dimensions];
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

            if(!isDoneGenerating && iterations < dimensions * dimensions)
            {
                // StartCoroutine(CheckEntropy());
                CheckEntropy();
            }
            else 
            {
                isDoneGenerating = true;
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

        public void Clear()
        {
            isDoneGenerating = false;
            firstCall = true;
            secondCall = false;

            ClearGrid();
            ClearExhibits();
        }

        private void ClearGrid()
        {
            foreach (var parent in gridComponents)
            {
                Transform parentTransform = parent.transform;

                foreach (Transform child in parentTransform)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
            gridComponents.Clear();
        }

        private void ClearExhibits()
        {
            foreach (Transform child in parentGO.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}