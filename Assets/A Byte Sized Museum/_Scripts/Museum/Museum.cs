using System.Collections;
using System.Collections.Generic;
using KaChow.WFC;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    [System.Serializable]
    public class Museum
    {
        public float museumSize;
        public float museumExhibitSize = 40f;
        [Tooltip("WFC Tiles")]
        public Tile[] exhibitPrefabs;
        // public ExhibitData[] exhibits;
        [HideInInspector] public Vector3 exhibitSize;
    }
}
