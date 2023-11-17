using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class InputManager : MonoBehaviour
    {
        private PlayerControls playerControls;

        #region Singleton
        public static InputManager Instance { get; private set; }
        private InputManager() {}
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

        public Vector2 GetPlayerMovement()
        {
            return playerControls.Player.Move.ReadValue<Vector2>();
        }

        public Vector2 GetMouseDelta()
        {
            return playerControls.Player.Look.ReadValue<Vector2>();
        }

        public bool PlayerJumpedThisFrame()
        {
            return playerControls.Player.Jump.triggered;
        }

        public bool PlayerInteractedThisFrame()
        {
            return playerControls.Player.Interact.triggered;
        }

        public bool IsPlayerRunning()
        {
            return playerControls.Player.Run.IsPressed();
        }
    }
}
