using System;
using UnityEngine;
using UnityEngine.UI;

public class howtoplay : MonoBehaviour
{
    public Sprite[] backgrounds;
    public string[] text;
    public Image backgroundImage;

    public TMPro.TextMeshProUGUI textcontent;

    public int index;

    void Start()
    {
        // index = PlayerPrefs.GetInt("index", 0);
        index =0;
        SetActiveBackgroundandText();
    }

    public void Next()
    {
        index++;
        if (index >= backgrounds.Length)
        {
             index = 0;
        }
        SetActiveBackgroundandText();
    }

    public void Previous()
    {
        index--;
        if (index < 0)
            index = backgrounds.Length - 1;
        SetActiveBackgroundandText();
    }
    public void setIndex()
    {
        index = 0;
    }

    void SetActiveBackgroundandText()
    {
        backgroundImage.sprite = backgrounds[index];
        textcontent.SetText(text[index]);
        // PlayerPrefs.SetInt("index", index);
        // PlayerPrefs.Save();
    }
}
