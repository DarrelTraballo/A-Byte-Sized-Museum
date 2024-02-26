using System.Collections;
using System.Collections.Generic;
using KaChow.AByteSizedMuseum;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject PausePanel;
    private bool isPaused = false;
    private bool isCursorVisible = true;
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
        PausePanel.SetActive(true);
        gameManager.SetGameState(GameState.Paused);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        PausePanel.SetActive(false);
        gameManager.SetGameState(GameState.Playing);
        Time.timeScale = 1;
    }

    // private void ToggleCursorVisibility()
    // {
    //     // Toggle the cursor visibility
    //     if (isCursorVisible)
    //     {
    //         HideCursor();
    //     }
    //     else
    //     {
    //         ShowCursor();
    //     }
    // }

    // private void ShowCursor()
    // {
    //     isCursorVisible = true;
    // }

    // private void HideCursor()
    // {
    //     isCursorVisible = false;
    // }
}
