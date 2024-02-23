using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class WireScript : InteractableBase
    {
        [SerializeField]
        private GameObject wirePuzzleUI;
        private GameObject wirePuzzleUIInstance;

        private Camera maincamera;
        private bool isWirePuzzleOpen;

        public override void Start()
        {
            base.Start();

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && isWirePuzzleOpen)
            {
                CloseInterpreter();
            }
        }

        public override void OnInteract()
        {
            if (!isWirePuzzleOpen)
            {
                OpenInterpreter();
            }
        }

        private void OpenInterpreter()
        {
            gameManager.Player.playerCamera.enabled = false;
            gameManager.Player.gameObject.SetActive(false);

            if (wirePuzzleUI == null)
            {
                Debug.Log("Interpreter UI not set");
                return;
            }

            gameManager.SetGameState(GameState.SolvePuzzle);

            if (wirePuzzleUI == null)
            {
                wirePuzzleUI.SetActive(true);

                isWirePuzzleOpen = true;
            }
            else
            {
                isWirePuzzleOpen = !isWirePuzzleOpen;
                wirePuzzleUI.SetActive(isWirePuzzleOpen);
            }
        }

        public void CloseInterpreter()
        {
            gameManager.Player.playerCamera.enabled = true;
            gameManager.Player.gameObject.SetActive(true);
            gameManager.SetGameState(GameState.Playing);

            wirePuzzleUI.SetActive(false);
            isWirePuzzleOpen = false;
        }
    }
}
