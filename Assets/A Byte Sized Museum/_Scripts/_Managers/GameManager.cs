using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public enum GameState
    {
        GenerateMuseum,
        Paused,
        Playing,
        SolvePuzzle,
        RunDialog,

    }
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        private GameManager() { }

        public GameState currentState;

        private MuseumGenerator museumGenerator;

        [Header("Player variables")]
        [SerializeField] private Vector3 playerStartPosition;
        public Player Player { get; private set; }

        private CharacterController characterController;

        [Header("Museum")]
        [SerializeField] private bool initMuseum = true;

        [Header("Misc")]
        [SerializeField] private GameObject crosshair;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;
        }

        private void Start()
        {
            GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
            Player = playerGO.GetComponent<Player>();
            characterController = playerGO.GetComponent<CharacterController>();

            SetGameState(GameState.Playing);
            if (!initMuseum) return;

            SetGameState(GameState.GenerateMuseum);
        }

        private void InitMuseum()
        {
            museumGenerator = MuseumGenerator.Instance;
            museumGenerator.Initialize();
            museumGenerator.GenerateExhibits();
            SetGameState(GameState.Playing);
        }

        public void SetGameState(GameState state)
        {
            currentState = state;

            switch (state)
            {
                case GameState.GenerateMuseum:
                    crosshair.SetActive(false);
                    SetCursorState(CursorLockMode.Confined);
                    InitMuseum();
                    break;

                case GameState.Playing:
                    crosshair.SetActive(true);
                    Player.SetCanMove(true);
                    SetCursorState(CursorLockMode.Locked);
                    break;

                case GameState.Paused:
                    crosshair.SetActive(false);
                    Player.SetCanMove(false);
                    SetCursorState(CursorLockMode.Confined);
                    break;

                case GameState.SolvePuzzle:
                    crosshair.SetActive(false);
                    Player.SetCanMove(false);
                    SetCursorState(CursorLockMode.None);
                    break;

                case GameState.RunDialog:
                    crosshair.SetActive(false);
                    Player.SetCanMove(false);
                    SetCursorState(CursorLockMode.Confined);
                    break;

                default:
                    break;

            }

        }

        public void SetCursorState(CursorLockMode cursorLockMode)
        {
            Cursor.lockState = cursorLockMode;
            switch (cursorLockMode)
            {
                case CursorLockMode.Locked:
                    Cursor.visible = false;
                    break;

                case CursorLockMode.Confined:
                    Cursor.visible = true;
                    break;

                case CursorLockMode.None:
                    Cursor.visible = true;
                    break;

                default:
                    break;
            }
        }
    }
}
