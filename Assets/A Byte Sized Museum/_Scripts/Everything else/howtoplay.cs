using System;
using UnityEngine;
using UnityEngine.UI;

public class howtoplay : MonoBehaviour
{
    public GameObject[] Panel;
    public GameObject Disablebutton;
    public GameObject imagecanvas;
    public GameObject Cutscene;
    public string[] text;

    public int index;

    void Start()
    {
        index =0;
 
        SetActiveBackgroundandText();
    }
    void Update()
    {
       if (index == 0)
        {
            Disablebutton.SetActive(false);
        }
        else
        {
            Disablebutton.SetActive(true);
        }
    }

    public void Next()
    {
        Panel[index].SetActive(false);
        index++;
        if (index >= Panel.Length)
        {
             Cutscene.SetActive(true);
             imagecanvas.SetActive(false);
        }
        else{
            SetActiveBackgroundandText();
        }
        
    }

    public void Previous()
    {
        Panel[index].SetActive(false);
        index--;
        if (index < 0)
            index = Panel.Length - 1;
        SetActiveBackgroundandText();
    }
    public void setIndex()
    {
        index = 0;
    }

    void SetActiveBackgroundandText()
    {
        Panel[index].SetActive(true);
        //textcontent.SetText(text[index]);
        
        // This line saves the player preference
        // PlayerPrefs.SetInt("index", index);
        // PlayerPrefs.Save();
    }
}
