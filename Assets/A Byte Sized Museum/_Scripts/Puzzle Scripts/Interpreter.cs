using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class Interpreter : InteractableBase
    {
        [HideInInspector]
        public List<ICodeBlock> codeBlocks;

        [Header("UI")]
        [SerializeField]
        private GameObject interpreterUI;
        private GameObject interpreterUIInstance;

        [SerializeField]
        private bool isInterpreterOpen;

        public override void Start()
        {
            base.Start();
            codeBlocks = new List<ICodeBlock>();    
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && isInterpreterOpen)
            {
                CloseInterpreter();
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

        private void CloseInterpreter()
        {
            gameManager.Player.canMove = true;

            interpreterUIInstance.SetActive(false);
            isInterpreterOpen = false;
        }

        private void ExecuteCodeBlocks()
        {
            foreach (var codeBlock in codeBlocks)
            {
                // do something
            }
        }

        public override void OnInteract()
        {
            Debug.Log("Interacted with Interpreter");
            if (!isInterpreterOpen)
            {
                OpenInterpreter();
            }
        }
    }
}
