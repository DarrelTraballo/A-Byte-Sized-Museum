using System;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.Extras
{
    public class Node : MonoBehaviour
    {
        [HideInInspector] public List<Node> neighbors;

        [HideInInspector] public float gScore;
        [HideInInspector] public float fScore;
        [HideInInspector] public float cost;


        [HideInInspector] public float x, y;
        [HideInInspector] public int gCost;
        [HideInInspector] public int hCost;
        [HideInInspector] public int fCost;

        [HideInInspector] public Node cameFromNode;

        public bool upPassable;
        public bool rightPassable;
        public bool downPassable;
        public bool leftPassable;

        private void Awake()
        {
            // x = transform.position.x;
            // y = transform.position.z;

            neighbors = new List<Node>();
            gScore = Mathf.Infinity;
            fScore = Mathf.Infinity;
            cost = Mathf.Infinity;
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            foreach (Node neighbor in neighbors)
            {
                if (neighbor == null) continue;

                if (neighbor.gScore < Mathf.Infinity)
                {
                    Gizmos.DrawLine(transform.position, neighbor.transform.position);
                }
            }
        }
        #endif
    }
}
