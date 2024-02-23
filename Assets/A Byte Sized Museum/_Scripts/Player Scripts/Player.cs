using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    // If can, completely refactor playerControllers
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour
    {
        public static Player Instance { get; private set; }
        private Player() { }

        public float walkingSpeed = 7.5f;
        public float runningSpeed;
        public float sneakSpeed;
        public float jumpSpeed = 10.0f;
        public float gravity = 25.0f;
        public Camera playerCamera;
        public float lookSpeed;
        public float lookXLimit = 90.0f;

        private CharacterController characterController;
        private Vector3 moveDirection = Vector3.zero;
        private float rotationX = 0f;
        private InputManager inputManager;

        private bool canMove = true;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;

            characterController = GetComponent<CharacterController>();
            runningSpeed = walkingSpeed * 1.3f;
            sneakSpeed = walkingSpeed * 0.322f;
        }

        private void Start()
        {
            inputManager = InputManager.Instance;
        }

        private void Update()
        {
            HandleMovementInput();
            HandleJumpInput();
            HandleGravity();
            MoveController();
            HandleCameraRotation();
        }

        private void HandleMovementInput()
        {
            if (!canMove) return;

            // We are grounded, so recalculate move direction based on axes
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            Vector2 moveInput = inputManager.GetPlayerMovement();

            // Press Left Ctrl to run
            bool isRunning = inputManager.IsPlayerRunning();

            // Press Left Shift to sneak
            bool isSneaking = inputManager.IsPlayerSneaking();

            float currentSpeedX = (isRunning ? runningSpeed : walkingSpeed) * moveInput.y;
            float currentSpeedY = (isRunning ? runningSpeed : walkingSpeed) * moveInput.x;

            float movementDirectionY = moveDirection.y;
            Vector3 desiredMove = (forward * currentSpeedX) + (right * currentSpeedY);
            moveDirection = desiredMove.normalized * (isRunning ? runningSpeed : isSneaking ? sneakSpeed : walkingSpeed);

            if (inputManager.PlayerJumpedThisFrame() && characterController.isGrounded)
            {
                moveDirection.y = jumpSpeed;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }
        }

        private void HandleJumpInput()
        {
            if (!canMove || !characterController.isGrounded) return;

            if (inputManager.PlayerJumpedThisFrame())
            {
                moveDirection.y = jumpSpeed;
            }
        }

        private void HandleGravity()
        {
            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)
            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }
        }

        private void MoveController()
        {
            // Move the controller
            characterController.Move(moveDirection * Time.deltaTime);
        }

        private void HandleCameraRotation()
        {
            if (!canMove) return;

            Vector2 lookInput = inputManager.GetMouseDelta();
            rotationX += lookInput.y * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

            playerCamera.transform.localRotation = Quaternion.Euler(-rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, lookInput.x * lookSpeed, 0);
        }

        public void SetCanMove(bool newCanMove)
        {
            canMove = newCanMove;
        }
    }
}