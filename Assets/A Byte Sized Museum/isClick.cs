using UnityEngine;
using UnityEngine.UI;

public class EnterKeyToContinue : MonoBehaviour
{
    public Button continueButton;

    private void Update()
    {
        // Check if the "Enter" key is pressed
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // Invoke the click event of the "Continue" button
            if (continueButton != null)
            {
                continueButton.onClick.Invoke();
            }
        }
    }
}