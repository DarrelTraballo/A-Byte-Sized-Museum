using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Scriptable Objects/Level")]
public class Level : ScriptableObject
{
    public int levelNumber;
    public string levelName;
    public bool isDoorUnlocked;
    public GameObject levelPrefab;
}
