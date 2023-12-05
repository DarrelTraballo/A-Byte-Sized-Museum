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

            if (wirePuzzleUI == null)
            {
                Debug.Log("Interpreter UI not set");
                return;
            }

            gameManager.Player.canMove = false;
            gameManager.SetCursorState(CursorLockMode.Confined);

            if (wirePuzzleUIInstance == null)
            {
                wirePuzzleUIInstance = Instantiate(wirePuzzleUI);
                wirePuzzleUIInstance.transform.SetParent(GameObject.Find("PlayerUICanvas").transform, false);
                isWirePuzzleOpen = true;
            }
            else
            {
                isWirePuzzleOpen = !isWirePuzzleOpen;
               wirePuzzleUIInstance.SetActive(isWirePuzzleOpen);
            }
        }

        public void CloseInterpreter()
        {
            gameManager.Player.canMove = true;
            gameManager.SetCursorState(CursorLockMode.Locked);

            wirePuzzleUIInstance.SetActive(false);
            isWirePuzzleOpen = false;
        }
    }
}
