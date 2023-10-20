using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    [CreateAssetMenu(fileName = "New Exhibit", menuName = "Scriptable Objects/Exhibit")]
    public class ExhibitData : ScriptableObject
    {
        [Header("Exhibit Information")]
        public string exhibitName;
        // public string 

        // puzzles
        //      can be a list of puzzles

        [Header("Door Spawn Points")]
        public Vector3 upDoorSpawnPoint;    
        public Vector3 rightDoorSpawnPoint;    
        public Vector3 downDoorSpawnPoint;    
        public Vector3 leftDoorSpawnPoint;

        [Space]
        [Header("Exhibit Prefab")]
        public GameObject exhibitPrefab;
    }
}
