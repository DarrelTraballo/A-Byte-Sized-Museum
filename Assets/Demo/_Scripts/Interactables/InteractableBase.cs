using UnityEngine;

// Base script for interactable objects in the game
public class InteractableBase : MonoBehaviour
{
    [Header("Interactable Base Variables")]
    public string onEnterInteractMessage = "Press [E] to interact.";
    public string onExitInteractMessage = "";
    public string interactableObjName = "";
    protected GameManager gameManager;
    private void Start() 
    {
        gameManager = GameManager.Instance;
    }

    // what happens if player enters interactable object collider
    protected virtual void OnTriggerEnter(Collider actor) 
    {
        if (actor.CompareTag("Player"))
        {
            gameManager.txtInteractMessage.text = onEnterInteractMessage;
        }
    }

    // what happens if player exits interactable object collider
    protected virtual void OnTriggerExit(Collider actor) 
    {
        if (actor.CompareTag("Player"))
        {
            gameManager.txtInteractMessage.text = onExitInteractMessage;
        }
    }

    // base method for interactable object specific interactions
    // just leave empty 
    public virtual void OnInteract()
    {

    }
    
}
