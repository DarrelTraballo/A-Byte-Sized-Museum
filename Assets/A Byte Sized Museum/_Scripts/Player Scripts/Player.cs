using UnityEngine;

namespace KaChow.AByteSizedMuseum {
    // If can, completely refactor playerControllers
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour
    {
        public float walkingSpeed = 7.5f;
        public float runningSpeed = 11.5f;
        public float jumpSpeed = 8.0f;
        public float gravity = 20.0f;
        public Camera playerCamera;
        public float lookSpeed;
        public float lookXLimit = 45.0f;

        public PlayerControls playerControls;

        private CharacterController characterController;
        private Vector3 moveDirection = Vector3.zero;
        private float rotationX = 0f;

        [HideInInspector]
        public bool canMove = true;

        private void Awake() 
        {
            playerControls = new PlayerControls();
            characterController = GetComponent<CharacterController>();
        }

        // TODO: UI BUTTON FOR JUMP, INTERACT

        private void OnEnable()
        {
            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Disable();
        }

        private void Update() 
        {
            // We are grounded, so recalculate move direction based on axes
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            Vector2 moveInput = playerControls.Player.Move.ReadValue<Vector2>();
            Vector2 lookInput = playerControls.Player.Look.ReadValue<Vector2>();

            // Press Left Shift to run
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float currentSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * moveInput.y : 0;
            float currentSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * moveInput.x : 0;


            float movementDirectionY = moveDirection.y;
            Vector3 desiredMove = (forward * currentSpeedX) + (right * currentSpeedY);
            moveDirection = desiredMove.normalized * (isRunning ? runningSpeed : walkingSpeed);

            if (playerControls.Player.Jump.triggered && canMove && characterController.isGrounded)
            {
                moveDirection.y = jumpSpeed;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)
            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            // Move the controller
            characterController.Move(moveDirection * Time.deltaTime);

            // Player's camera rotation
            if (canMove)
            {
                rotationX += lookInput.y * lookSpeed;
                // Debug.Log($"lookInput.y : {lookInput.y}\nlookInput.y * lookSpeed : {lookInput.y * lookSpeed}");
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(-rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, lookInput.x * lookSpeed, 0);        
            }
            // if (canMove)
            // {
            //     rotationX += Input.GetAxis("Mouse Y") * lookSpeed;
            //     Debug.Log($"lookInput.y : {Input.GetAxis("Mouse Y")}\nlookInput.y * lookSpeed : {Input.GetAxis("Mouse Y") * lookSpeed}");
            //     rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            //     playerCamera.transform.localRotation = Quaternion.Euler(-rotationX, 0, 0);
            //     transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);        
            // }
        }
    }
}