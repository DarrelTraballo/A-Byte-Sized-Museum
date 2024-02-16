using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject InputManager;
    private bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        // Check for the Escape key press
        if (Input.GetKeyDown(KeyCode.Escape))
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
       InputManager.SetActive(false);
       Time.timeScale = 0;
    }

    public void Continue()
    {
        PausePanel.SetActive(false);
        InputManager.SetActive(true);
        Time.timeScale = 1;
    }
}
