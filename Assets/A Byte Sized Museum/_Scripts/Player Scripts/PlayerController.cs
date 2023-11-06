using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        private PlayerControls playerControls;

        private CharacterController controller;
        private Vector3 playerVelocity;
        private bool groundedPlayer;
        [SerializeField] private float playerSpeed = 2.0f;
        [SerializeField] private float jumpHeight = 1.0f;
        [SerializeField] private float gravityValue = -9.81f;

        private void Awake()
        {
            playerControls = new PlayerControls();
            controller = GetComponent<CharacterController>();
        }

        private void OnEnable()
        {
            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Disable();
        }

        private void Start()
        {
            
        }

        void Update()
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector2 input = playerControls.Player.Move.ReadValue<Vector2>();
            Vector3 move = new Vector3(input.x, 0f, input.y);
            controller.Move(move * Time.deltaTime * playerSpeed);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }

            // Changes the height position of the player..
            if (playerControls.Player.Jump.triggered && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }
}
