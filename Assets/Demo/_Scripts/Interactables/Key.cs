using UnityEngine;
using TMPro;

public class Key : InteractableBase
{
    [SerializeField]
    private GameObject keyPrefab;
    // TODO: make key appear on lower right, para magmukhang hawak mo yung key
    public override void OnInteract()
    {
        GameManager.Instance.isDoorUnlocked = true;
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
