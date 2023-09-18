using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Key : MonoBehaviour, IInteractable
{
    public TextMeshProUGUI txtMissionUpdate;
    public TextMeshProUGUI txtInteractMessage;

    public void OnInteract()
    {
        GameManager.Instance.isKeyCollected = true;
        txtInteractMessage.text = "Collected Key";
        txtMissionUpdate.text = "Use key to open Door.";
        gameObject.SetActive(false);
        
    }

    private void OnTriggerEnter(Collider actor) 
    {
        if (actor.CompareTag("Player"))
        {
            txtInteractMessage.text = "Press [E] to collect.";
        }
    }

    private void OnTriggerExit(Collider actor) 
    {
        if (actor.CompareTag("Player"))
        {
            txtInteractMessage.text = "";
        }
    }
}
