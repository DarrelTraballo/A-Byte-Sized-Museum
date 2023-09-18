using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour, IInteractable
{
    public TextMeshProUGUI txtMissionUpdate;
    public TextMeshProUGUI txtInteractMessage;

    public void OnInteract()
    {
        if (!GameManager.Instance.isKeyCollected) 
        {
            txtInteractMessage.text = "A key is needed to open this door.";
            txtMissionUpdate.text = "Look for a Key";
        }
        else
        {
            txtInteractMessage.text = "Door opened";
            txtMissionUpdate.text = "Level Complete!";
        }
    }

    private void OnTriggerEnter(Collider actor) 
    {
        if (actor.CompareTag("Player"))
        {
            txtInteractMessage.text = "Press [E] to interact.";
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
