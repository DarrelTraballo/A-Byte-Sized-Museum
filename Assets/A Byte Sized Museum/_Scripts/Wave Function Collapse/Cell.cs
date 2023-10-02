using UnityEngine;

namespace KaChow.WFC {
    public class Cell : MonoBehaviour
    {
        public bool isCollapsed;
        public Tile[] tileOptions;

        public void CreateCell(bool collapseState, Tile[] tiles) {
            isCollapsed = collapseState;
            tileOptions = tiles;
        }

        public void RecreateCell(Tile[] tiles) {
            tileOptions = tiles;
        }
    }
}
