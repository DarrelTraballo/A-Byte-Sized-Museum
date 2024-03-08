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

    }
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        private GameManager() { }

        public GameState currentState;

        private MuseumGenerator museumGenerator;
        private InputManager inputManager;

        [Header("Player variables")]
        [SerializeField] private Vector3 playerStartPosition;
        public Player Player { get; private set; }

        private CharacterController characterController;

        [Header("UI")]
        [SerializeField] private GameObject crosshair;
        [SerializeField] private GameObject miniMapUI;
        [SerializeField] private GameObject interpreterUI;
        [SerializeField] private GameObject toolTipUI;

        private Canvas interpreterUICanvas;
        private TMP_Text toolTipTitle;
        private TMP_Text toolTipSubtitle;

        [Header("Debug")]
        [SerializeField] private bool debugModeEnabled = false;

        public bool DebugModeEnabled
        {
            get { return debugModeEnabled; }
            private set
            {
                debugModeEnabled = value;
                Debug.Log($"Debug Mode {(debugModeEnabled ? "Enabled" : "Disabled")}");
            }
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;
        }

        private void Start()
        {
            inputManager = InputManager.Instance;
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;

            GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
            Player = playerGO.GetComponent<Player>();
            characterController = playerGO.GetComponent<CharacterController>();

            interpreterUICanvas = interpreterUI.GetComponentInChildren<Canvas>();

            toolTipTitle = toolTipUI.transform.Find("ToolTip Text Title").GetComponent<TMP_Text>();
            toolTipSubtitle = toolTipUI.transform.Find("ToolTip Text Subtitle").GetComponent<TMP_Text>();

            toolTipUI.SetActive(false);

            SetGameState(GameState.Playing);
            if (sceneName.Equals("Tutorial") || sceneName.Equals("MainMenu")) return;

            SetGameState(GameState.GenerateMuseum);
        }

        private void Update()
        {
            // ctrl + shit + t
            if (inputManager.IsDebugModeToggled())
            {
                ToggleDebugMode();
            }
        }

        private void ToggleDebugMode()
        {
            DebugModeEnabled = !DebugModeEnabled;
            StartCoroutine(UpdateToolTipText("Debug Mode", $"{(debugModeEnabled ? "Enabled" : "Disabled")}", 3f));
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
                    interpreterUICanvas.enabled = false;
                    crosshair.SetActive(false);
                    miniMapUI.SetActive(false);
                    SetCursorState(CursorLockMode.Confined);
                    InitMuseum();
                    break;

                case GameState.Playing:
                    interpreterUICanvas.enabled = false;
                    crosshair.SetActive(true);
                    miniMapUI.SetActive(true);
                    Player.SetCanMove(true);
                    SetCursorState(CursorLockMode.Locked);
                    break;

                case GameState.Paused:
                    interpreterUICanvas.enabled = false;
                    crosshair.SetActive(false);
                    miniMapUI.SetActive(false);
                    Player.SetCanMove(false);
                    SetCursorState(CursorLockMode.Confined);
                    break;

                case GameState.SolvePuzzle:
                    interpreterUICanvas.enabled = true;
                    crosshair.SetActive(false);
                    miniMapUI.SetActive(false);
                    Player.SetCanMove(false);
                    // SetCursorState(CursorLockMode.Locked);
                    SetCursorState(CursorLockMode.None);
                    break;

                case GameState.RunDialog:
                    interpreterUICanvas.enabled = false;
                    crosshair.SetActive(false);
                    miniMapUI.SetActive(false);
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

        public IEnumerator UpdateToolTipText(string title, string subtitle, float delay)
        {
            UpdateToolTipText(title, subtitle);

            yield return new WaitForSeconds(delay);

            DisableToolTipText();
        }

        // TODO: make disappear after a while
        public void UpdateToolTipText(string title, string subtitle)
        {
            toolTipUI.SetActive(true);
            toolTipTitle.text = title;
            toolTipSubtitle.text = subtitle;

            // yield return new WaitForSeconds(delay);

        }

        public void DisableToolTipText()
        {
            toolTipUI.SetActive(false);
        }
    }
}
