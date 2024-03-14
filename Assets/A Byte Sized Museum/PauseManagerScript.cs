using System.Collections;
using System.Collections.Generic;
using KaChow.AByteSizedMuseum;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject PausePanel;
    private GameManager gameManager;
    private InputManager inputManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        inputManager = InputManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        // Check for the Escape key press
        if (inputManager.IsEscapeButtonPressed() && (gameManager.currentState == GameState.Playing || gameManager.currentState == GameState.Paused))
        {
            // Toggle the pause state
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (PausePanel.activeSelf)
        {
            Continue();
            return;
        }
        gameManager.SetGameState(GameState.Paused);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        gameManager.SetGameState(GameState.Playing);
        Time.timeScale = 1;
    }
}
