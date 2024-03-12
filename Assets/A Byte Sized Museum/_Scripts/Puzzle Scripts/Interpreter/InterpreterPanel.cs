using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class InterpreterPanel : InteractableBase
    {
        [SerializeField] private int interpreterID;
        [Header("UI")]
        [SerializeField] private GameObject interpreterUI;
        [SerializeField] private GameObject interpreterUIInstance;

        private bool isInterpreterOpen;

        public override void Start()
        {
            base.Start();
            subtitleText = "Press E to OPEN";
        }

        private void Update()
        {
            if (inputManager.IsEscapeButtonPressed() && gameManager.currentState == GameState.SolvePuzzle)
            {
                CloseInterpreter();
            }
        }

        public override void OnLookEnter()
        {
            base.OnLookEnter();
            // gameManager.SetToolTipText("Interpreter", "Press E to OPEN");

        }

        public override void OnLookExit()
        {
            base.OnLookExit();
        }

        public override void OnInteract()
        {
            base.OnInteract();
            if (!isInterpreterOpen)
            {
                OpenInterpreter();
            }
        }

        private void OpenInterpreter()
        {
            gameManager.SetGameState(GameState.SolvePuzzle);
            isInterpreterOpen = true;
        }

        public void CloseInterpreter()
        {
            gameManager.SetGameState(GameState.Playing);
            isInterpreterOpen = false;
        }
    }
}
