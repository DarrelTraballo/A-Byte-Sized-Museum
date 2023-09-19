using UnityEngine;
using TMPro;

public class Key : InteractableBase
{
    // [SerializeField]
    // // private GameObject keyPrefab;
    private Door door;
    private void Start() 
    {
        door = FindObjectOfType<Door>();
    }

    public override void OnInteract()
    {
        door.doorUnlockCounter++;
        GameManager.Instance.txtInteractMessage.text = "Collected Key";
        GameManager.Instance.txtMissionUpdate.text = "Use key to open Door.";
        GameManager.Instance.crossHairText.text = "";
        gameObject.SetActive(false);
    }

    private void GiveKey()
    {
        // lmao
    }
}
