using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Scriptable Objects/Level")]
public class LevelData : ScriptableObject
{
    public int levelNumber;
    public string levelName;

    public Vector3 playerPosition;
    public bool isDoorUnlocked;
    public GameObject levelPrefab;
}
