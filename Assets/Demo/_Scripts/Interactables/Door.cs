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
        gameManager = GameManager.Instance;
    }

    // overriden method from InteractableBase class
    public override void OnInteract()
    {
        // if door is locked
        if (!gameManager.isDoorUnlocked) 
        {
            gameManager.txtInteractMessage.text = doorOnInteractMessage;
            gameManager.txtMissionUpdate.text = doorOnInteractMission;

            if (gameManager.levelUnlockCounter == gameManager.levelUnlockProgress) 
            {
                gameManager.isDoorUnlocked = true;
                OnInteract();
                return;
            }
            else
            {
                int diff = gameManager.levelUnlockProgress - gameManager.levelUnlockCounter;
                gameManager.txtInteractMessage.text = $"Door is still locked.\nYou still need {diff} key{(diff > 1 ? "s" : "" )}.";
            }
            
        }
        // else if door has been unlocked
        else 
        {
            gameManager.isDoorUnlocked = true;
            renderer.material.color = unlockedColor;
            gameManager.txtInteractMessage.text = "Door opened.";
            gameManager.txtMissionUpdate.text = "Door Unlocked!";

            gameManager.Invoke("LoadNextLevel", 2.5f);
        }
    }
}
