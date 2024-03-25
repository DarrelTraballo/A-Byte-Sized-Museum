using KaChow.WFC;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    [CreateAssetMenu(fileName = "New Exhibit", menuName = "Scriptable Objects/Exhibit")]
    public class ExhibitData : ScriptableObject
    {
        [Header("Exhibit Information")]
        public string exhibitName;

        // TODO: Try to put tile adjacency constraints here somehow
        // para ida-drag and drop nalang yung prefab na gagamitin
        [Header("WFC Adjacency Rules")]
        public Tile[] upNeighbors;
        public Tile[] rightNeighbors;
        public Tile[] downNeighbors;
        public Tile[] leftNeighbors;
    }
}
