using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Scriptable Objects/Level")]
public class LevelData : ScriptableObject
{
    [Header("Level Information")]
    public int levelNumber;
    public string levelName;
    public Vector3 playerPosition;
    public bool isDoorUnlocked;
    // variable for how many things needed to unlock/proceed to next level
    public int unlockCondition;

    [Header("Level Prefab")]
    public GameObject levelPrefab;
}
