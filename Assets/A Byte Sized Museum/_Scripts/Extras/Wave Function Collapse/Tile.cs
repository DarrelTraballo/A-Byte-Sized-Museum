using KaChow.AByteSizedMuseum;
using UnityEngine;

namespace KaChow.WFC {
    public class Tile : MonoBehaviour
    {
        // TODO: something's up with this xdd
        // try turning them into GameObjects instead

        // haha maybe just revert everything back for now
        public Tile[] upNeighbors;
        public Tile[] rightNeighbors;
        public Tile[] downNeighbors;
        public Tile[] leftNeighbors;

        // [Header("Exhibit Prefab")]
        // public GameObject tilePrefab;

        // [Header("Exhibit Data")]
        // public ExhibitData exhibitData;


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("working");
                var tileContents = transform.Find("Contents");
                tileContents.gameObject.SetActive(true);
            }
        }
    }
}
