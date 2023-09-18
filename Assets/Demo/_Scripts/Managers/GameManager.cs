using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

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
    [SerializeField] private Player player;
    private GameObject currentLevelPrefab;

    private void Start() 
    {
        LoadLevel(currentLevelIndex); 
    }

    // TODO: LEVEL 1 prompt to players you can use spacebar to jump when approaching the platforms
    public void LoadNextLevel()
    {
        currentLevelIndex++;
        if (currentLevelIndex >= levels.Count) return;

        Destroy(currentLevelPrefab);
        LoadLevel(currentLevelIndex);
    }

    public void LoadLevel(int levelIndex)
    {
        if (levelIndex >= levels.Count) 
        {
            Debug.Log("No more levels to load");
            return;
        }
        // GameObject player = GameObject.FindGameObjectWithTag("Player");

        var levelToLoad = levels[levelIndex];
        currentLevelPrefab = Instantiate(levelToLoad.levelPrefab);
        isDoorUnlocked = levelToLoad.isDoorUnlocked;

        if (player == null) 
        {
            Debug.Log("no player found");
            return;
        }

        // TODO: fix player spawning on specified coords when loading next level.
        // Debug.Log($"Setting player to position {levelToLoad.playerPosition}");
        player.transform.position = levelToLoad.playerPosition;
    }
}
