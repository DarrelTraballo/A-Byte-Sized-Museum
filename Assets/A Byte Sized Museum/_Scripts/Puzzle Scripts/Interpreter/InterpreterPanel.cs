using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class InterpreterPanel : InteractableBase
    {
        [SerializeField] private int interpreterID;
        [Header("UI")]
        [SerializeField]
        private GameObject interpreterUI;
        private GameObject interpreterUIInstance;

        private bool isInterpreterOpen;

        public override void Start()
        {
            base.Start();
        }

        private void Update()
        {
            if (inputManager.IsEscapeButtonPressed() && isInterpreterOpen && gameManager.currentState == GameState.SolvePuzzle)
            {
                CloseInterpreter();
                gameManager.SetGameState(GameState.Playing);
            }
        }

        public override void OnInteract()
        {
            base.OnInteract();
            if (!isInterpreterOpen)
            {
                gameManager.SetGameState(GameState.SolvePuzzle);
                OpenInterpreter();
            }
        }

        private void OpenInterpreter()
        {
            if (interpreterUI == null)
            {
                Debug.Log("Interpreter UI not set");
                return;
            }

            if (interpreterUIInstance == null)
            {
                interpreterUIInstance = Instantiate(interpreterUI);
                var interpreter = interpreterUIInstance.GetComponent<Interpreter>();
                interpreter.SetInterpreterID(interpreterID);
                interpreterUIInstance.transform.SetParent(GameObject.Find("PlayerUICanvas").transform, false);
                isInterpreterOpen = true;
            }
            else
            {
                isInterpreterOpen = !isInterpreterOpen;
                interpreterUIInstance.SetActive(isInterpreterOpen);
            }
        }

        public void CloseInterpreter()
        {
            interpreterUIInstance.SetActive(false);
            isInterpreterOpen = false;
        }
    }
}
