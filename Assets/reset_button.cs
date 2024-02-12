using UnityEngine;
using UnityEngine.UI;

public class ButtonClickHandler : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnButtonClick()
    {
        // Trigger the Highlighted state
        button.OnPointerEnter(null);

        // Additional logic to open your panel or perform other actions
    }

    public void OnPanelClosed()
    {
        // Reset the Highlighted state
        button.OnPointerExit(null);

        // Additional logic to close your panel or perform other actions
    }
}
