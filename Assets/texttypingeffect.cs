using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextTypingEffect : MonoBehaviour
{
    public float typingSpeed = 0.01f; // Adjust the typing speed as needed
    public TMPro.TextMeshProUGUI tmproTextField; // Corrected line for TextMeshPro
    public string fullText;

    private string currentText = "";

    void Start()
    {
        tmproTextField.text = "";
    }
    void OnEnable()
    {

        if (currentText != fullText) // Check if typing is not already completed
        {
            currentText = "";
            tmproTextField.text = "";
            StartCoroutine(TypeText());
        }
    }

    IEnumerator TypeText()
    {

        foreach (char letter in fullText.ToCharArray())
        {
            currentText += letter;
            tmproTextField.text = currentText;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
