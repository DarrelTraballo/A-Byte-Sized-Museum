using UnityEngine;
using TMPro;

public class Door : InteractableBase
{
    public string doorInteractMessage = "Press [E] to interact.";
    
    private void Start()
    {
        onEnterInteractMessage = doorInteractMessage;
    }

    public override void OnInteract()
    {
        if (!GameManager.Instance.isDoorUnlocked) 
        {
            GameManager.Instance.txtInteractMessage.text = "Door is locked.\nA key is needed to open this door.";
            GameManager.Instance.txtMissionUpdate.text = "Look for a Key";
        }
        else
        {
            GameManager.Instance.txtInteractMessage.text = "Door opened";
            GameManager.Instance.txtMissionUpdate.text = "Level Complete!\nLoading Next Level";
            GameManager.Instance.Invoke("LoadNextLevel", 5f);
        }
    }
}
