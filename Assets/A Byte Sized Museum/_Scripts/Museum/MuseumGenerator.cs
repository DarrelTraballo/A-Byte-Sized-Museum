using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class MuseumGenerator : MonoBehaviour
    {
        [Header("Exhibit Generator Variables")]
        [SerializeField] private ExhibitGenerator exhibitGenerator;
        [SerializeField] private Transform exhibitParent;
        [SerializeField] private int exhibitCount = 1;

        [Space]
        [Range(15, 30)]
        [SerializeField] private int minGridSize;

        [Range(15, 30)]
        [SerializeField] private int maxGridSize;

        private int rows;
        private int cols;
        private int tileSize;

        [Space]
        [SerializeField] private Vector3 minBounds;
        [SerializeField] private Vector3 maxBounds;
        [SerializeField] private float minSeparation = 10f;

        [Space]
        [SerializeField] private GameObject hallwayPrefab;
        [SerializeField] private Transform hallwayParent;
        [SerializeField] private float hallwayHeight = 0.1f;

        // TODO: maybe give exhibits their own Exhibit script
        private List<GameObject> exhibits;
        private List<GameObject> hallways;
        private List<Vector3> roomCenters;

        public void Initialize() 
        {
            tileSize = (int) exhibitGenerator.GridTilePrefabs[0].transform.localScale.x;
            exhibitGenerator.Initialize();

            exhibits = new List<GameObject>();
            hallways = new List<GameObject>();
            roomCenters = new List<Vector3>();
        }

        public void GenerateExhibits() 
        {
            if (exhibitParent == null) 
            {
                Debug.LogError("Unassigned Museum Parent.");
                return;
            }

            Vector3 exhibitStartPos;
            GameObject exhibit;
            List<Bounds> existingExhibitBounds = new List<Bounds>();

            int maxAttempts = 150;
            // bool positionFound = false;

            for (int i = 0; i < exhibitCount; i++)
            {
                int attempts = 0;
                // bool isOverlapping = true;

                // maybe have pre-defined room sizes
                // but the floors will still depend on wfc
                rows = (int) Mathf.Ceil(Random.Range(minGridSize, maxGridSize + 1));
                cols = (int) Mathf.Ceil(Random.Range(minGridSize, maxGridSize + 1));

                do 
                {
                    // first exhibit generated always generates in front of museum lobby
                    // first exhibit will always have a door on left wall
                    if (i == 0) 
                    {
                        exhibitStartPos = new Vector3(0f, 0f, -(rows / 2));
                    }
                    // last exhibit generated always generates in front of museum lobby
                    else if (i == exhibitCount - 1)
                    {
                        exhibitStartPos = new Vector3(maxBounds.x, 0f, -(rows / 2));
                    }
                    else
                    {
                        float randomX = Mathf.Ceil(Random.Range(minBounds.x, maxBounds.x));
                        float randomZ = Mathf.Ceil(Random.Range(minBounds.z, maxBounds.z));
                        exhibitStartPos = new Vector3(randomX, 0f, randomZ);
                    }

                    bool isOverlapping = CheckOverlap(exhibitStartPos, rows, cols, existingExhibitBounds, minSeparation);

                    // overlap check here
                    if (!isOverlapping)
                    {
                        // GenerateExhibit(Vector3 startPos, int rows, int cols, int id)
                        exhibit = exhibitGenerator.GenerateExhibit(exhibitStartPos, rows, cols, i);
                        exhibit.transform.parent = exhibitParent;

                        // Update the list of existing exhibit bounds
                        Bounds newExhibitBounds = new Bounds(exhibitStartPos, new Vector3(rows, 1f, cols));
                        existingExhibitBounds.Add(newExhibitBounds);

                        roomCenters.Add(exhibitStartPos + new Vector3(rows / 2f, 0f, cols / 2f));

                        // positionFound = true;
                        break;
                    }
                    else 
                    {
                        attempts++;

                        if (attempts >= maxAttempts)
                        {
                            Debug.LogWarning("Could not place exhibit without overlap after " + attempts + " attempts.");
                            break;
                        }

                        // instead of getting another random position, why not just push the overlapping exhibit until
                        // until it doesnt overlap anymore?
                        float randomX = Mathf.Ceil(Random.Range(minBounds.x, maxBounds.x));
                        float randomZ = Mathf.Ceil(Random.Range(minBounds.z, maxBounds.z));
                        exhibitStartPos = new Vector3(randomX, 0f, randomZ);
                        // exhibitStartPos += new Vector3(1f, 0f, 1f);
                    }
                }
                while (true);
                // while (!positionFound);
            }

            GenerateGraph(roomCenters);
        }

        private bool CheckOverlap(Vector3 position, int rows, int cols, List<Bounds> existingExhibitBounds, float minSeparation)
        {
            Bounds newExhibitBounds = new Bounds(position, new Vector3(rows, 1f, cols));

            foreach (Bounds existingBounds in existingExhibitBounds)
            {
                if (existingBounds.Intersects(newExhibitBounds))
                {
                    // Overlaps with an existing exhibit
                    return true;
                }

                // Calculate the minimum distance required to consider them not overlapping
                float minDistance = Mathf.Max(existingBounds.size.x, newExhibitBounds.size.x) / 2f + minSeparation;
                if (Vector3.Distance(existingBounds.center, newExhibitBounds.center) < minDistance)
                {
                    // Exhibits are too close, consider them overlapping.
                    return true;
                }
            }

            // No overlap
            return false;
        }

        // 4. Create hallways
        // TODO: DOORS :>
        // also TODO: make hallways not just be diagonal lines connecting each rooms.
        // make them look and feel like actual hallways.
        // also TODO: remove unnecessary hallways.

        // IDEA 1
        // https://vazgriz.com/119/procedurally-generated-dungeons/
        // Hallways already generate a graph-like structure
        // maybe use a 

        // IDEA 2
        // 1. Generate a grid from the area bounds
        // 2. 
        private void GenerateGraph(List<Vector3> roomCenters)
        {
            for (int i = 0; i < roomCenters.Count; i++)
            {
                for (int j = i + 1; j < roomCenters.Count; j++)
                {
                    // Calculate hallway points (you can modify this based on your needs)
                    Vector3 startPoint = roomCenters[i];
                    Vector3 endPoint = roomCenters[j];

                    // Ensure the path is either horizontal or vertical
                    Vector3 pathDirection = endPoint - startPoint;
                    pathDirection.y = 0;  // Ensure no vertical displacement

                    // Instantiate the hallway prefab
                    var hallway = Instantiate(hallwayPrefab, Vector3.zero, Quaternion.identity);
                    hallway.transform.parent = hallwayParent;

                    // Calculate the midpoint between the start and end points
                    Vector3 midpoint = (startPoint + endPoint) / 2f * tileSize;

                    // Position path at the midpoint
                    // hallway.transform.SetLocalPositionAndRotation(midpoint, Quaternion.LookRotation(endPoint - startPoint, Vector3.up));
                    hallway.transform.SetLocalPositionAndRotation(midpoint, Quaternion.LookRotation(pathDirection, Vector3.up));

                    // Scale the object to stretch between the start and end points
                    float distance = Vector3.Distance(startPoint, endPoint);
                    hallway.transform.localScale = new Vector3(hallway.transform.localScale.x * 5, hallwayHeight, distance * tileSize);
                }
            }
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            // Gizmos for Room generation bounds
            Gizmos.color = Color.black;
            Vector3 offset = Vector3.up;
            Gizmos.DrawWireCube((minBounds + maxBounds) / 2f * 2f, new Vector3((maxBounds.x - minBounds.x) * 2, maxBounds.y - minBounds.y, (maxBounds.z - minBounds.z) * 2));

            // Gizmos for Hallways
            Gizmos.color = Color.green;

            // if (roomCenters == null) return;
            for (int i = 0; i < roomCenters?.Count; i++)
            {
                for (int j = i + 1; j < roomCenters.Count; j++)
                {
                    Vector3 startPoint = roomCenters[i];
                    Vector3 endPoint = roomCenters[j];

                    // Draw a line between the centers of the rooms
                    Gizmos.DrawLine(startPoint * tileSize, endPoint * tileSize);
                }
            }
        }
        #endif
    }
}
