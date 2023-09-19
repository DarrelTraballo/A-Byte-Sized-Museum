using UnityEngine;

public class Door : InteractableBase
{
    [Header("Door variables")]
    public Color locekdColor;
    public Color unlockedColor;
    public string doorOnInteractMessage;
    public string doorOnInteractMission;

    private new Renderer renderer;
    private void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material.color = locekdColor;
    }

    public override void OnInteract()
    {
        // if door is locked
        if (!GameManager.Instance.isDoorUnlocked) 
        {
            GameManager.Instance.txtInteractMessage.text = doorOnInteractMessage;
            GameManager.Instance.txtMissionUpdate.text = doorOnInteractMission;
        }
        // else if door has been unlocked
        else
        {
            renderer.material.color = unlockedColor;
            GameManager.Instance.txtInteractMessage.text = "Door opened.";
            GameManager.Instance.txtMissionUpdate.text = "Door Unlocked!";

            GameManager.Instance.Invoke("LoadNextLevel", 2.5f);
        }
    }
}
