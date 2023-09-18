using UnityEngine;
using TMPro;

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

    [Header("Level Info")]
    public int currentLevel;
    public TextMeshProUGUI txtMissionUpdate;
    public TextMeshProUGUI txtInteractMessage;

    [Header("Level 1")]
    public bool isKeyCollected = false;

    private void Update() 
    {
        switch (currentLevel)
        {
            case 1:
                // no condition
                // just check if key is collected
                break;
            default:
                break;
        }    
    }
}
