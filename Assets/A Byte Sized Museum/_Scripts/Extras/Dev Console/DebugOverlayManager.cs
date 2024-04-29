using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class DebugOverlayManager : MonoBehaviour
    {
        public static DebugOverlayManager Instance { get; private set; }
        private DebugOverlayManager() { }

        private GameManager gameManager;
        private InputManager inputManager;

        private Canvas debugConsoleCanvas;
        [Header("UI")]
        [SerializeField] private GameObject debugConsole;
        [SerializeField] private GameObject cheatsPanel;
        [SerializeField] private TMP_Text cheatItem;

        [Header("Cheats")]
        [SerializeField] private bool debugModeEnabled = false;
        [SerializeField, Range(5, 60)] private int secondsToAdd = 30;
        [SerializeField, Range(5, 60)] private int secondsToRemove = 60;

        private bool isDebugPaused = false;

        // [Header("References to Scripts")]
        private Timer timer;

        public bool CheatsEnabled
        {
            get { return debugModeEnabled; }
            private set
            {
                debugModeEnabled = value;
                Debug.Log($"Cheats {(debugModeEnabled ? "Enabled" : "Disabled")}");
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
            gameManager = GameManager.Instance;
            inputManager = InputManager.Instance;
            timer = Timer.Instance;

            debugConsoleCanvas = debugConsole.GetComponentInChildren<Canvas>();
            debugConsoleCanvas.enabled = false;

            DisplayAllCheatBindings();
        }

        private void Update()
        {
            if (inputManager.IsCheatsEnabled())
            {
                ToggleDebugMode();
                ShowDebugOverlay();
            }

            if (!CheatsEnabled) return;

            ToggleDevConsole();
            CheatCommands();
        }

        private void CheatCommands()
        {
            if (inputManager.IsGiveFragmentPressed())
                timer.AddFragment();

            if (inputManager.IsAddTimePressed())
                timer.AddSecondsToTimer(secondsToAdd);

            if (inputManager.IsSubtractTimePressed())
                timer.RemoveSecondsToTimer(secondsToRemove);

            if (inputManager.IsToggleUIPressed())
            {
                gameManager.ToggleUI();
                debugConsoleCanvas.enabled = !debugConsoleCanvas.enabled;
            }

            if (inputManager.IsDebugPausedPressed() &&
                (gameManager.currentState == GameState.Playing ||
                gameManager.currentState == GameState.DebugPaused))
                HandleDebugPause();

            if (inputManager.IsGoFastPressed())
                gameManager.GoFast();
        }

        private void ShowDebugOverlay()
        {
            debugConsoleCanvas.enabled = CheatsEnabled;
        }

        private void ToggleDevConsole()
        {
            if (inputManager.IsShowDebugOverlayPressed())
            {
                debugConsoleCanvas.enabled = !debugConsoleCanvas.enabled;
            }
        }

        private void ToggleDebugMode()
        {
            CheatsEnabled = !CheatsEnabled;
            StartCoroutine(gameManager.SetToolTipTextCoroutine("Cheats", $"{(debugModeEnabled ? "Enabled" : "Disabled")}"));
        }

        private void DisplayAllCheatBindings()
        {
            foreach (var action in inputManager.allCheatsBindings)
            {
                TMP_Text newCheat = Instantiate(cheatItem, cheatsPanel.transform);
                newCheat.text = action;
            }
        }

        private void HandleDebugPause()
        {
            GameState state = isDebugPaused ? GameState.Playing : GameState.DebugPaused;
            gameManager.SetGameState(state);

            isDebugPaused = !isDebugPaused;
        }
    }
}
