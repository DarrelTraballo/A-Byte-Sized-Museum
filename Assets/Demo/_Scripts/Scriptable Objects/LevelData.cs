using UnityEngine;

namespace KaChow.Demo {
    [CreateAssetMenu(fileName = "New Level", menuName = "Scriptable Objects/Level")]
    public class LevelData : ScriptableObject
    {
        [Header("Level Information")]
        public int levelNumber;
        public string levelName;

        // temporary holders
        public string levelMissionText;
        public string levelInteractText;
        public Vector3 playerPosition;
        public bool isDoorUnlocked;
        // variable for how many things needed to unlock/proceed to next level
        public int unlockConditions;

        [Header("Level Prefab")]
        public GameObject levelPrefab;
    }
}
