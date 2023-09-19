using UnityEngine;
using TMPro;

// Script needed 
public class InteractableBase : MonoBehaviour
{
    [Header("Interactable Prompts")]
    public string onEnterInteractMessage = "Press [E] to interact.";
    public string onExitInteractMessage = "";
    public string interactableObjName = "";

    protected virtual void OnTriggerEnter(Collider actor) 
    {
        if (actor.CompareTag("Player"))
        {
            GameManager.Instance.txtInteractMessage.text = onEnterInteractMessage;
        }
    }

    protected virtual void OnTriggerExit(Collider actor) 
    {
        if (actor.CompareTag("Player"))
        {
            GameManager.Instance.txtInteractMessage.text = onExitInteractMessage;
        }
    }

    public virtual void OnInteract()
    {

    }
    
}
