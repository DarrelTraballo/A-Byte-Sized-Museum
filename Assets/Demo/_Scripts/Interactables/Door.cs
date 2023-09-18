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
        if (!GameManager.Instance.isDoorUnlocked) 
        {
            GameManager.Instance.txtInteractMessage.text = "Door is locked.\nA key is needed to open this door.";
            GameManager.Instance.txtMissionUpdate.text = "Look for a Key";
        }
        else
        {
            GameManager.Instance.txtInteractMessage.text = "Door opened";
            GameManager.Instance.txtMissionUpdate.text = "Level Complete!";
        }
    }

    private void OnTriggerEnter(Collider actor) 
    {
        if (actor.CompareTag("Player"))
        {
            GameManager.Instance.txtInteractMessage.text = "Press [E] to interact.";
        }
    }

    private void OnTriggerExit(Collider actor) 
    {
        if (actor.CompareTag("Player"))
        {
            // GameManager.Instance.txtInteractMessage.text = "";
        }
    }
}
