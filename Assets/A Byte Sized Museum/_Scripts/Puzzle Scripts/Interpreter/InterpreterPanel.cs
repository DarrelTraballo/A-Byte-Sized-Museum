using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class InterpreterPanel : InteractableBase
    {
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
            if (Input.GetKeyDown(KeyCode.Escape) && isInterpreterOpen)
            {
                CloseInterpreter();
            }
        }

        public override void OnInteract()
        {
            if (!isInterpreterOpen)
            {
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

            gameManager.Player.canMove = false;
            // gameManager.SetCursorState(CursorLockMode.Confined);

            if (interpreterUIInstance == null)
            {
                interpreterUIInstance = Instantiate(interpreterUI);
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
            gameManager.Player.canMove = true;
            // gameManager.SetCursorState(CursorLockMode.Locked);

            interpreterUIInstance.SetActive(false);
            isInterpreterOpen = false;
        }
    }
}
