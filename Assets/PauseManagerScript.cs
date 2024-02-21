using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject InputManager;
    private bool isPaused = false;
    private bool isCursorVisible = true;

    // Update is called once per frame
    void Update()
    {
        // Check for the Escape key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle the pause state
            ShowCursor();
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
       InputManager.SetActive(false);
       Time.timeScale = 0;
    }

    public void Continue()
    {
        PausePanel.SetActive(false);
        InputManager.SetActive(true);
        HideCursor();
        Time.timeScale = 1;
    }
    private void ToggleCursorVisibility()
    {
        // Toggle the cursor visibility
        if (isCursorVisible)
        {
            HideCursor();
        }
        else
        {
            ShowCursor();
        }
    }

    private void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // Optional: Set lock state according to your needs
        isCursorVisible = true;
    }

    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; // Optional: Set lock state according to your needs
        isCursorVisible = false;
    }
}
