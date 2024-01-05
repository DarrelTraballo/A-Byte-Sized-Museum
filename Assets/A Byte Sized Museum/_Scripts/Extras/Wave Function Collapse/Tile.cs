using KaChow.AByteSizedMuseum;
using UnityEngine;

namespace KaChow.WFC {
    public class Tile : MonoBehaviour
    {
        // TODO: something's up with this xdd
        // try turning them into GameObjects instead

        // haha maybe just revert everything back for now
        public ExhibitData exhibitData;

        public Tile[] UpNeighbors => exhibitData.upNeighbors;
        public Tile[] RightNeighbors => exhibitData.rightNeighbors;
        public Tile[] DownNeighbors => exhibitData.downNeighbors;
        public Tile[] LeftNeighbors => exhibitData.leftNeighbors;        
    }
}
