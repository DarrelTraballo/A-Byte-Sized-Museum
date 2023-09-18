using UnityEngine;
using TMPro;

public class Key : MonoBehaviour, IInteractable
{
    public TextMeshProUGUI txtMissionUpdate;
    public TextMeshProUGUI txtInteractMessage;

    public void OnInteract()
    {
        GameManager.Instance.isDoorUnlocked = true;
        GameManager.Instance.txtInteractMessage.text = "Collected Key";
        GameManager.Instance.txtMissionUpdate.text = "Use key to open Door.";
        gameObject.SetActive(false);
        
    }

    private void OnTriggerEnter(Collider actor) 
    {
        if (actor.CompareTag("Player"))
        {
            GameManager.Instance.txtInteractMessage.text = "Press [E] to collect.";
        }
    }

    private void OnTriggerExit(Collider actor) 
    {
        if (actor.CompareTag("Player"))
        {
            GameManager.Instance.txtInteractMessage.text = "";
        }
    }
}
