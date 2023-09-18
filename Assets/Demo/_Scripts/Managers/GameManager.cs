using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // Singleton reference
    public static GameManager Instance { get; private set; }

    private void Awake() 
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else 
            Instance = this;

    }

    [Header("Levels")]
    public List<Level> levels;

    [Header("Level Info")]
    public int currentLevelIndex = 1;
    public TextMeshProUGUI txtMissionUpdate;
    public TextMeshProUGUI txtInteractMessage;

    [Header("Level Elements")]
    public bool isDoorUnlocked;

    private void Start() 
    {
        LoadNextLevel(currentLevelIndex); 
        Debug.Log($"from scriptableObect: {levels[currentLevelIndex].isDoorUnlocked}");
        Debug.Log($"from gameManager: {isDoorUnlocked}");
    }

    // TODO: make something for moving to the next level

    public void LoadNextLevel(int levelNumber)
    {
        var currentLevel = levels[levelNumber];
        Instantiate(currentLevel.levelPrefab);
        isDoorUnlocked = currentLevel.isDoorUnlocked;
    }
}
