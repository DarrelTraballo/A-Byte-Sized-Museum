using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class MuseumGenerator : MonoBehaviour
    {
        [Header("Exhibit Generator variables")]
        [SerializeField] private ExhibitGenerator exhibitGenerator;
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
        [Header("Exhibit Parent GameObject")]
        [SerializeField] private Transform exhibitParent;


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
            int maxAttempts = 50;

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
                    else if (i == exhibitCount - 1)
                    {
                        exhibitStartPos = new Vector3(70f, 0f, -(rows / 2));
                    }
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

        // 4. Create paths
        //      how???
    }
}
