using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class howtoplay : MonoBehaviour 
{ 
public GameObject[] background; 
private int index;
 
void Start() 
{ 

    index = PlayerPrefs.GetInt("index", 0); 
    SetActiveBackground();
 
 } 
 
public void Next() 
{ 

  index++; 
  
  if (index >= background.Length) index = 0; 
  SetActiveBackground(); 
  
} 

public void Previous() 
{ 

  index--; 
  
  if (index < 0) 
  index = background.Length - 1; 
  SetActiveBackground(); 
 
 } 
 
 void SetActiveBackground() 
 { 
 
 for (int i = 0; i < background.Length; i++) 
 { 
   background[i].SetActive(i == index); 
 } 
 
 PlayerPrefs.SetInt("index", index); 
 PlayerPrefs.Save(); 
 } 
}