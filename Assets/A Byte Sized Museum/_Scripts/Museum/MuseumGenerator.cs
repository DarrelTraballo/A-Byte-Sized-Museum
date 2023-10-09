using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class MuseumGenerator : MonoBehaviour
    {
        [Header("Museum Layout Generation")]
        [SerializeField] private GameObject[] exhibitPrefabs;
        [SerializeField] private Transform exhibitParent;
        [SerializeField] private float museumSize;
        [SerializeField] private float museumCellSize;
        private Vector3 exhibitSize;

        public void Initialize() 
        {
            exhibitSize = exhibitPrefabs[0].transform.localScale;
        }


        public void GenerateExhibits()
        {
            // WFC



            GenerateExhibitsNoWFC();
        }

        private void GenerateExhibitsNoWFC()
        {
            // Calculate center position
            float cellCenterZ = (museumCellSize / 2f) + 5f;
            float cellCenterX = (museumCellSize / 2f) + 5f;

            // Calculate offset
            float offsetX = (museumCellSize - exhibitSize.x) / 2f;
            float offsetZ = (museumCellSize - exhibitSize.z) / 2f;

            Vector3 gridOffset = new Vector3(0, 0, -(museumSize * museumCellSize / 2));

            for (int x = 0; x < museumSize; x++)
            {
                for (int z = 0; z < museumSize; z++)
                {
                    Vector3 position = new Vector3(x * museumCellSize + cellCenterX - offsetX, -1, z * museumCellSize + cellCenterZ - offsetZ);
                    Instantiate(exhibitPrefabs[0], position + gridOffset, Quaternion.identity, exhibitParent);
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

            Vector3 offset = new Vector3(0, 0, -(museumCellSize * museumSize) / 2);

            Vector3 origin = transform.position + offset;

            float cellWidth = museumCellSize * museumSize;
            float cellHeight = museumCellSize * museumSize;

            // vertical grid lines
            for (int i = 0; i <= museumSize; i++)
            {
                Vector3 start = origin + new Vector3(i * museumCellSize, 0, 0);
                Vector3 end = start + new Vector3(0, 0, cellHeight);
                Gizmos.DrawLine(start, end);
            }
            
            // vertical grid lines
            for (int i = 0; i <= museumSize; i++)
            {
                Vector3 start = origin + new Vector3(0, 0, i * museumCellSize);
                Vector3 end = start + new Vector3(cellWidth, 0, 0);
                Gizmos.DrawLine(start, end);
            }


        }
        #endif
    }
}
