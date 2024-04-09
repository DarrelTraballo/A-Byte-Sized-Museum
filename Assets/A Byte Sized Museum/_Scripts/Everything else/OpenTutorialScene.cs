using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OpenTutorialScene : MonoBehaviour
{
    void OnEnable()
    {
        LoadScene();
    }
    public void LoadScene()
    {
       SceneManager.LoadScene("MainMenu");
    }
}
