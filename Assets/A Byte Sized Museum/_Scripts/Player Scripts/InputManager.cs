using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace KaChow.AByteSizedMuseum
{
    public class InputManager : MonoBehaviour
    {
        /// <summary>
        /// Singleton instance of InputManager.
        /// </summary>
        public static InputManager Instance { get; private set; }
        private InputManager() { }

        private PlayerControls playerControls;
        public List<string> allCheatsBindings = new List<string>();

        private void Awake()
        {
            playerControls = new PlayerControls();
            StoreAllCheatBindings();

            if (Instance != null && Instance != this)
                Destroy(this.gameObject);
            else
                Instance = this;
        }

        private void OnEnable()
        {
            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Disable();
        }

        /// <summary>
        /// Gets the current player movement vector.
        /// </summary>
        /// <returns>Vector2 representing player movement.</returns>
        public Vector2 GetPlayerMovement()
        {
            return playerControls.Player.Move.ReadValue<Vector2>();
        }

        /// <summary>
        /// Gets the current mouse delta for looking around.
        /// </summary>
        /// <returns>Vector2 representing mouse movement delta.</returns>
        public Vector2 GetMouseDelta()
        {
            return playerControls.Player.Look.ReadValue<Vector2>();
        }

        /// <summary>
        /// Checks if the player jumped in the current frame.
        /// </summary>
        /// <returns>True if the player jumped this frame, otherwise false.</returns>
        public bool PlayerJumpedThisFrame()
        {
            return playerControls.Player.Jump.IsPressed();
        }

        /// <summary>
        /// Checks if the player interacted (e.g., pressed the interact button) in the current frame.
        /// </summary>
        /// <returns>True if the player interacted this frame, otherwise false.</returns>
        public bool PlayerInteractedThisFrame()
        {
            return playerControls.Player.Interact.triggered;
        }

        /// <summary>
        /// Checks if the player is currently running.
        /// </summary>
        /// <returns>True if the player is running, otherwise false.</returns>
        public bool IsPlayerRunning()
        {
            return playerControls.Player.Run.IsPressed();
        }

        /// <summary>
        /// Checks if the player is currently sneaking.
        /// </summary>
        /// <returns>True if the player is sneaking, otherwise false.</returns>
        public bool IsPlayerSneaking()
        {
            return playerControls.Player.Sneak.IsPressed();
        }

        /// <summary>
        /// Checks if the escape button was pressed in the current frame.
        /// </summary>
        /// <returns>True if the escape button was pressed, otherwise false.</returns>
        public bool IsEscapeButtonPressed()
        {
            return playerControls.Player.Escape.triggered;
        }

        /// <summary>
        /// Checks if debug mode was toggled in the current frame.
        /// </summary>
        /// <returns>True if debug mode was toggled, otherwise false.</returns>
        public bool IsCheatsEnabled()
        {
            return playerControls.Cheats.ToggleCheats.triggered;
        }

        /// <summary>
        /// Checks if the "Give Fragment" cheat was activated in the current frame.
        /// </summary>
        /// <returns>True if the "Give Fragment" cheat was activated, otherwise false.</returns>
        public bool IsGiveFragmentPressed()
        {
            return playerControls.Cheats.GiveFragment.triggered;
        }

        /// <summary>
        /// Checks if the "Add Time" cheat was activated in the current frame.
        /// </summary>
        /// <returns>True if the "Add Time" cheat was activated, otherwise false.</returns>
        public bool IsAddTimePressed()
        {
            return playerControls.Cheats.AddTime.triggered;
        }

        /// <summary>
        /// Checks if the "Subtract Time" cheat was activated in the current frame.
        /// </summary>
        /// <returns>True if the "Subtract Time" cheat was activated, otherwise false.</returns>
        public bool IsSubtractTimePressed()
        {
            return playerControls.Cheats.SubtractTime.triggered;
        }

        /// <summary>
        /// Checks if the "Show Debug Overlay" cheat was activated in the current frame.
        /// </summary>
        /// <returns>True if the "Show Debug Overlay" cheat was activated, otherwise false.</returns>
        public bool IsShowDebugOverlayPressed()
        {
            return playerControls.Cheats.ShowDebugOverlay.triggered;
        }

        public bool IsToggleUIPressed()
        {
            return playerControls.Cheats.ToggleUI.triggered;
        }

        /// <summary>
        /// Stores all cheat bindings in a list for easy access and display.
        /// </summary>
        private void StoreAllCheatBindings()
        {
            if (playerControls == null)
            {
                Debug.LogError("PlayerControls is not initialized.");
                return;
            }

            InputActionMap cheatsActionMap = playerControls.Cheats.Get();

            foreach (var action in cheatsActionMap.actions)
            {
                // Assuming you want to get the display string for the first binding of each action.
                // You might need to adjust this if you have multiple bindings per action and you want to handle them differently.
                string bindingDisplayString = action.GetBindingDisplayString();

                // Since GetBindingDisplayString already provides a formatted string, you might not need additional formatting.
                // However, if you still need to adjust the format, you can apply your formatting here.
                string formattedBinding = FormatBindingDisplayString(bindingDisplayString);

                allCheatsBindings.Add($"{action.name}: {formattedBinding}");
            }
        }

        /// <summary>
        /// Formats the display string of a binding for better readability.
        /// </summary>
        /// <param name="displayString">The original display string of the binding.</param>
        /// <returns>The formatted display string.</returns>
        private string FormatBindingDisplayString(string displayString)
        {
            // Apply any custom formatting you need. For example, you might want to convert to uppercase or adjust the formatting of modifiers.
            // If no additional formatting is needed, you can simply return the displayString.
            return displayString.ToUpper();
        }

        /// <summary>
        /// Formats the path of a binding to remove device prefixes and capitalize modifiers.
        /// </summary>
        /// <param name="path">The original path of the binding.</param>
        /// <returns>The formatted path.</returns>
        private string FormatBindingPath(string path)
        {
            // Your existing method to format the binding path
            path = path.Replace("<Keyboard>/", "");
            path = path.Replace("ctrl", "Ctrl")
                       .Replace("shift", "Shift")
                       .Replace("alt", "Alt");
            path = path.Replace("/", "+");
            path = path.ToUpper();

            return path;
        }
    }
}
