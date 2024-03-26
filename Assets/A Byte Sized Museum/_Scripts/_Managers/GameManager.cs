using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KaChow.AByteSizedMuseum
{
    public enum GameState
    {
        GenerateMuseum,
        Paused,
        Playing,
        SolvePuzzle,
        RunDialog,
        PlayerWin, // TODO: Rename
        GameOver,
        MainMenu
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        private GameManager() { }

        public GameState currentState;

        private MuseumGenerator museumGenerator;
        private InputManager inputManager;
        private DebugOverlayManager debugOverlayManager;
        private Dialogmanager_Maingame dialogueManager;

        public Player Player { get; private set; }

        private CharacterController characterController;

        [Header("UI")]
        [SerializeField] private GameObject crosshairUI;
        [SerializeField] private GameObject miniMapUI;
        [SerializeField] private GameObject interpreterUI;
        [SerializeField] private GameObject pauseUI;
        [SerializeField] private GameObject toolTipUI;
        [SerializeField] private GameObject gameOverUI;
        [SerializeField] private GameObject playerWinUI; // TODO: Rename

        private Canvas interpreterUICanvas;
        private TMP_Text toolTipTitle;
        private TMP_Text toolTipSubtitle;

        private Interpreter interpreter;

        [Header("Gameplay")]
        [SerializeField, Range(10f, 30f)] private int remainingTimeInMinutes = 15;
        [SerializeField, Range(10f, 30f)] private int timeAllowance = 30;
        [SerializeField, Range(10f, 30f)] private int secondsToAdd = 30;
        [SerializeField, Range(7, 12)] private int puzzleExhibitAmount = 10;
        [SerializeField] private bool isPaused = false;

        // [Header("Debug")]
        // [SerializeField] private bool debugModeEnabled = false;

        public int RemainingTimeInMinutes
        {
            get { return remainingTimeInMinutes; }
            private set { remainingTimeInMinutes = Mathf.Clamp(value, 10, 30); }
        }

        public int TimeAllowance
        {
            get { return timeAllowance; }
            private set { timeAllowance = Mathf.Clamp(value, 10, 30); }
        }

        public int SecondsToAdd
        {
            get { return secondsToAdd; }
            private set { secondsToAdd = Mathf.Clamp(value, 10, 30); }
        }

        public int PuzzleExhibitAmount
        {
            get { return puzzleExhibitAmount; }
            private set { puzzleExhibitAmount = Mathf.Clamp(value, 7, 12); }
        }

        public bool IsGameOver { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            inputManager = InputManager.Instance;
            debugOverlayManager = DebugOverlayManager.Instance;

            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;

            GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
            Player = playerGO.GetComponent<Player>();
            characterController = playerGO.GetComponent<CharacterController>();

            interpreterUICanvas = interpreterUI.GetComponentInChildren<Canvas>();
            interpreter = interpreterUICanvas.GetComponentInChildren<Interpreter>();

            toolTipTitle = toolTipUI.transform.Find("ToolTip Text Title").GetComponent<TMP_Text>();
            toolTipSubtitle = toolTipUI.transform.Find("ToolTip Text Subtitle").GetComponent<TMP_Text>();

            toolTipUI.SetActive(false);

            SetGameState(GameState.Playing);
            if (sceneName.Equals("Tutorial") || sceneName.Equals("MainMenu") || sceneName.Equals("Cutscene")) return;

            dialogueManager = Dialogmanager_Maingame.Instance;
            dialogueManager.LoadDialogues();

            SetGameState(GameState.GenerateMuseum);
        }

        private void Update()
        {
            if (inputManager.IsEscapeButtonPressed() &&
               (currentState == GameState.Playing ||
                currentState == GameState.Paused))
                HandlePause();
        }

        private void InitMuseum()
        {
            museumGenerator = MuseumGenerator.Instance;
            museumGenerator.Initialize();
            museumGenerator.GenerateExhibits();
            SetGameState(GameState.Playing);
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

        public IEnumerator SetToolTipTextCoroutine(string title, string subtitle, float delay = 2.5f)
        {
            SetToolTipText(title, subtitle);

            yield return new WaitForSeconds(delay);

            DisableToolTipText();
        }

        public void SetToolTipText(string title, string subtitle)
        {
            toolTipUI.SetActive(true);
            toolTipTitle.text = title;
            toolTipSubtitle.text = subtitle;
        }

        public void DisableToolTipText()
        {
            toolTipUI.SetActive(false);
        }

        public void HandlePause()
        {
            GameState state = isPaused ? GameState.Playing : GameState.Paused;
            SetGameState(state);

            isPaused = !isPaused;
        }

        public void SetGameState(GameState state)
        {
            currentState = state;

            // Reset UI and player states before applying specific state settings
            ResetUI();
            ResetPlayerState();

            switch (state)
            {
                case GameState.GenerateMuseum:
                    SetCursorState(CursorLockMode.Confined);
                    InitMuseum();
                    break;

                case GameState.Playing:
                    crosshairUI.SetActive(true);
                    miniMapUI.SetActive(true);
                    Player.SetCanMove(true);
                    SetCursorState(CursorLockMode.Locked);
                    Time.timeScale = 1;
                    break;

                case GameState.Paused:
                    pauseUI.SetActive(true);
                    SetCursorState(CursorLockMode.Confined);
                    Time.timeScale = 0;
                    break;

                case GameState.SolvePuzzle:
                    interpreterUICanvas.enabled = true;
                    SetCursorState(debugOverlayManager.CheatsEnabled ? CursorLockMode.None : CursorLockMode.Confined);
                    break;

                case GameState.RunDialog:
                    crosshairUI.SetActive(true);
                    miniMapUI.SetActive(true);
                    Player.SetCanMove(false);
                    SetCursorState(CursorLockMode.Confined);
                    break;

                case GameState.PlayerWin:
                    playerWinUI.SetActive(true);
                    SetCursorState(CursorLockMode.Confined);
                    break;

                case GameState.GameOver:
                    gameOverUI.SetActive(true);
                    SetCursorState(CursorLockMode.Confined);
                    break;

                case GameState.MainMenu:
                    ResetUI();
                    Time.timeScale = 1;
                    break;

                default:
                    break;
            }
        }

        private void ResetUI()
        {
            crosshairUI.SetActive(false);
            miniMapUI.SetActive(false);
            gameOverUI.SetActive(false);
            playerWinUI.SetActive(false);
            interpreterUICanvas.enabled = false;
            pauseUI.SetActive(false);
            DisableToolTipText();
        }

        private void ResetPlayerState()
        {
            Player.SetCanMove(false);
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void ResumeGame()
        {
            HandlePause();
        }

        public void MainMenuState()
        {
            SetGameState(GameState.MainMenu);
        }
    }
}
