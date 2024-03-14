using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace KaChow.AByteSizedMuseum
{
    public class InputManager : MonoBehaviour
    {
        private PlayerControls playerControls;

        #region Singleton
        /// <summary>
        /// Singleton instance of InputManager.
        /// </summary>
        public static InputManager Instance { get; private set; }
        private InputManager() { }
        private void Awake()
        {
            playerControls = new PlayerControls();

            if (Instance != null && Instance != this)
                Destroy(this.gameObject);
            else
                Instance = this;
        }
        #endregion

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
        public bool IsDebugModeToggled()
        {
            return playerControls.Player.DebugToggle.triggered;
        }
    }
}
