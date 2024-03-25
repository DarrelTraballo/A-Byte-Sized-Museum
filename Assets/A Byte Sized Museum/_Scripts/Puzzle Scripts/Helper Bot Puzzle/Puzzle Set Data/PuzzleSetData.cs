using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    [CreateAssetMenu(fileName = "New Puzzle Data", menuName = "Scriptable Objects/Puzzle Data")]
    public class PuzzleSetData : ScriptableObject
    {
        public List<GameObject> puzzleSets;
    }
}
