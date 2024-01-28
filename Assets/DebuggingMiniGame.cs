using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DebuggingMiniGame : MonoBehaviour
{
    public TMP_Text codeText; // Use TMP_Text for TextMeshPro text elements
    public TMP_InputField userInputField; // Use TMP_InputField for TextMeshPro input fields
    public TMP_Text feedbackText; // Use TMP_Text for TextMeshPro text elements

    public Button SubmitButton;
    private string correctCode = "]";

    private void Start()
    {
        DisplayCode();

        // Assuming SubmitButton is a reference to your Button component
        SubmitButton.onClick.AddListener(() => SubmitSolution());
    }


    private void DisplayCode()
    {

    }


    public void SubmitSolution()
    {
        string userSolution = userInputField.text;

        if (userSolution == correctCode)
        {
            feedbackText.text = "Correct! Code is fixed.";
            // Add scoring logic or move to the next challenge
        }
        else
        {
            feedbackText.text = "Incorrect. Check for errors.";
            // Deduct points or provide hints
        }
    }
}
