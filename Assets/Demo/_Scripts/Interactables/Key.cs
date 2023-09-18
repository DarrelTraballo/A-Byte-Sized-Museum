using UnityEngine;
using TMPro;

public class Key : InteractableBase
{
    public string keyInteractMessage = "Press [E] to pick up.";

    private void Start()
    {
        onEnterInteractMessage = keyInteractMessage;
    }

    public override void OnInteract()
    {
        GameManager.Instance.isDoorUnlocked = true;
        GameManager.Instance.txtInteractMessage.text = "Collected Key";
        GameManager.Instance.txtMissionUpdate.text = "Use key to open Door.";
        gameObject.SetActive(false);
        
    }
}
