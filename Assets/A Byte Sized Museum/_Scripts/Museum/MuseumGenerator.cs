using System.Collections.Generic;
using System.IO;
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

        // 1. Define the entire area where exhibits can generate
        [Space]
        [SerializeField] private Vector3 minBounds;
        [SerializeField] private Vector3 maxBounds;
        [SerializeField] private float minSeparation = 10f;

        [Space]
        [SerializeField] private GameObject hallwayPrefab;
        [SerializeField] private Transform hallwayParent;
        [SerializeField] private float hallwayHeight = 0.1f;

        // 2. Place exhibits randomly inside the defined area
        public void GenerateExhibits() 
        {
            if (exhibitParent == null) 
            {
                Debug.LogError("Unassigned Museum Bounds.");
                return;
            }

            Vector3 exhibitStartPos;
            GameObject exhibit;
            List<Bounds> existingExhibitBounds = new List<Bounds>();
            List<Vector3> roomCenters = new List<Vector3>();

            int maxAttempts = 100;

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
                    // TODO: align to the middle of museum lobby
                    if (i == 0) 
                        exhibitStartPos = new Vector3(0f, 0f, -(rows / 2));
                    // else if (i == exhibitCount - 1)
                    // {
                    //     exhibitStartPos = new Vector3(70f, 0f, -(rows / 2));
                    // }
                    else
                    {
                        float randomX = Mathf.Ceil(Random.Range(minBounds.x, maxBounds.x));
                        float randomZ = Mathf.Ceil(Random.Range(minBounds.z, maxBounds.z));
                        exhibitStartPos = new Vector3(randomX, 0f, randomZ);
                    }

                    bool isOverlapping = CheckOverlap(exhibitStartPos, rows, cols, existingExhibitBounds, minSeparation);

                    // overlap check here
                    Debug.Log($"isOverlapping: {isOverlapping}");
                    if (!isOverlapping)
                    {
                        // GenerateExhibit(Vector3 startPos, int rows, int cols, int id)
                        exhibit = exhibitGenerator.GenerateExhibit(exhibitStartPos, rows, cols, i);
                        exhibit.transform.parent = exhibitParent;

                        // Update the list of existing exhibit bounds
                        Bounds newExhibitBounds = new Bounds(exhibitStartPos, new Vector3(rows, 1f, cols));
                        existingExhibitBounds.Add(newExhibitBounds);

                        roomCenters.Add(exhibitStartPos + new Vector3(rows / 2f, 0f, cols / 2f));
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

                        float randomX = Mathf.Ceil(Random.Range(minBounds.x, maxBounds.x));
                        float randomZ = Mathf.Ceil(Random.Range(minBounds.z, maxBounds.z));
                        exhibitStartPos = new Vector3(randomX, 0f, randomZ);
                    }
                }
                while (true);
            }

            GenerateHallways(roomCenters);
        }

        // 3. Check if generated exhibit overlaps an already generated exhibit
        //      if not, place exhibit
        //      if overlaps, repeat step 2

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
        private void GenerateHallways(List<Vector3> roomCenters)
        {
            for (int i = 0; i < roomCenters.Count; i++)
            {
                for (int j = i + 1; j < roomCenters.Count; j++)
                {
                    // Calculate hallway points (you can modify this based on your needs)
                    Vector3 startPoint = roomCenters[i];
                    Vector3 endPoint = roomCenters[j];

                    // Instantiate the hallway prefab
                    var hallway = Instantiate(hallwayPrefab, Vector3.zero, Quaternion.identity);
                    hallway.transform.parent = hallwayParent;
                    hallway.name = $"Hallway ({i} -> {j})";

                    // Calculate the midpoint between the start and end points
                    Vector3 midpoint = (startPoint + endPoint) / 2f * 2f;

                    // Position path at the midpoint
                    hallway.transform.SetLocalPositionAndRotation(midpoint, Quaternion.LookRotation(endPoint - startPoint, Vector3.up));

                    Debug.Log($"{hallway.name} midpoint: {midpoint}", hallway);
                    Debug.Log($"{hallway.name} position: {hallway.transform.position}", hallway);

                    // Scale the object to stretch between the start and end points
                    float distance = Vector3.Distance(startPoint, endPoint);
                    hallway.transform.localScale = new Vector3(hallway.transform.localScale.x * 5, hallwayHeight, distance * 2);

                    // Adjust the position to align with the midpoint
                    // hallway.transform.Translate(Vector3.forward * (distance * 0.5f));
                }
            }
        }
    }
}
