using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KaChow.Extras
{
    public class AStarPathfinding : MonoBehaviour
    {
        // TODO: FIGURE THIS SHIT OUT
        public static bool PathExists(Node startNode, Node targetNode)
        {
            List<Vector3Int> path = new List<Vector3Int>();

            HashSet<Node> openSet = new HashSet<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();
            Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
            Dictionary<Node, float> gScore = new Dictionary<Node, float>();

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet.First();

                if (currentNode == targetNode)
                    return true;

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                foreach (Node neighbor in currentNode.neighbors)
                {
                    if (!neighbor.upPassable && neighbor.transform.position.y > currentNode.transform.position.y) continue;
                    if (!neighbor.downPassable && neighbor.transform.position.y < currentNode.transform.position.y) continue;
                    if (!neighbor.leftPassable && neighbor.transform.position.x < currentNode.transform.position.x) continue;
                    if (!neighbor.rightPassable && neighbor.transform.position.x > currentNode.transform.position.x) continue;

                    float tentativeGScore = gScore[currentNode] + Distance(currentNode, neighbor);

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                    else if (tentativeGScore >= gScore[neighbor])
                        continue;  // This is not a better path

                    cameFrom[neighbor] = currentNode;
                    gScore[neighbor] = tentativeGScore;
                    neighbor.cost = tentativeGScore + Heuristic(neighbor, targetNode);
                }
            }

            return false;
        }

        private static Node GetLowestFScoreNode(HashSet<Node> nodeSet)
        {
            Node lowestNode = null;
            float lowestFScore = float.MaxValue;

            foreach (Node node in nodeSet)
            {
                if (node.cost < lowestFScore)
                {
                    lowestFScore = node.cost;
                    lowestNode = node;
                }
            }

            return lowestNode;
        }

        private static float Distance(Node nodeA, Node nodeB)
        {
            // Implement your distance calculation based on your grid (e.g., Euclidean distance)
            // Adjust this based on your specific requirements and grid structure
            return Vector3.Distance(new Vector3(nodeA.x, nodeA.y), new Vector2(nodeB.x, nodeB.y));
        }

        private static float Heuristic(Node node, Node endNode)
        {
            // Implement your heuristic calculation based on your grid (e.g., Manhattan distance)
            // Adjust this based on your specific requirements and grid structure
            return Mathf.Abs(node.x - endNode.x) + Mathf.Abs(node.y - endNode.y);
        }

        private static List<Node> ReconstructPath(Dictionary<Node, Node> cameFrom, Node currentNode)
        {
            List<Node> path = new List<Node>();
            path.Add(currentNode);

            while (cameFrom.ContainsKey(currentNode))
            {
                currentNode = cameFrom[currentNode];
                path.Insert(0, currentNode);  // Insert at the beginning to maintain the correct order
            }

            return path;
        }
    }
}
