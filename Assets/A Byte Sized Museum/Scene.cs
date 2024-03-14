using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoaderVolume : MonoBehaviour
{
    public GameObject Volume_Panel; // Reference to the Volume_Panel GameObject
    public Button Button_volume; 

    public void LoadScene()
    {
        if (Volume_Panel != null)
        {
            Volume_Panel.SetActive(true);
        }
        else
        {
            Debug.LogError("Volume_Panel reference is not set. Please assign it in the Unity Editor.");
        }

        // Always disable the button after setting up Volume_Panel
        Button_volume.interactable = false;
    }

    public void EnableButton()
    {
        Button_volume.interactable = true;
    }
}
