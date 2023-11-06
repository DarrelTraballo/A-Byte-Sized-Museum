using UnityEngine;

namespace KaChow.WFC {
    [System.Serializable]
    public class Tile
    {
        public Tile[] upNeighbors;
        public Tile[] rightNeighbors;
        public Tile[] downNeighbors;
        public Tile[] leftNeighbors;

        [Header("Exhibit Prefab")]
        public GameObject tilePrefab;
    }
}
