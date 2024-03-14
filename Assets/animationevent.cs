using KaChow.AByteSizedMuseum;
using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour
{
    private Animator animatorComponent;
    private Image imageComponent; // Use Image instead of Renderer

    public GameObject Gameovertext;
    public GameManager gamemanager;
    public GameObject Exit;
    public GameObject Tryagain;

    // Change the backgroundColor type to Color
    public Color backgroundColor = Color.red;

    private void Start()
    {
        // Assign imageComponent and animatorComponent
        imageComponent = GetComponent<Image>();
        animatorComponent = GetComponent<Animator>();

        // Check if animatorComponent is missing
        if (animatorComponent == null)
        {
            Debug.LogError("Animator component is missing!");
        }

        // Check if imageComponent is missing
        if (imageComponent == null)
        {
            Debug.LogError("Image component is missing!");
        }
    }

    public void StopAnimation()
    {
        if (animatorComponent != null)
        {
            // Change the trigger name to match your Animator Controller
            animatorComponent.SetTrigger("StopLooping");
        }
        else
        {
            Debug.LogError("Animator component is missing!");
        }
    }

    public void ShowCanvas()
    {
        gamemanager.SetGameState(GameState.GameOver);

        SetBackgroundColor();
        Gameovertext.SetActive(true);
        Exit.SetActive(true);
        Tryagain.SetActive(true);
        
    }

    public void SetBackgroundColor()
    {
        if (imageComponent != null)
        {
            // Set the color of the image component
            imageComponent.color = backgroundColor;
        }
        else
        {
            Debug.LogError("Image component is missing!");
        }
    }
}
