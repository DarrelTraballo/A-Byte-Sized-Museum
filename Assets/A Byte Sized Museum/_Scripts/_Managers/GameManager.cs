using KaChow.WFC;
using KaChow.Demo;
using UnityEngine;

namespace KaChow.AByteSizedMuseum 
{
    public class GameManager : MonoBehaviour
    {
        // Singleton reference, para isang instance lang ang kinukuha pag need i-reference from another script
        // can only access GameManager by using GameManager.Instance
        public static GameManager Instance { get; private set; }
        #region Singleton
        private GameManager() {}
        private void Awake() 
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else 
                Instance = this;
        }
        #endregion

        // TODO: - Level Generation using WFC
        //          - Do something about tilesets
        //       - Exhibit Generation using WFC
        //       - 
        
        // Exhibit Generator variables
        // [Header("Exhibit Generator variables")]


        public WaveFunctionCollapse WFC { get; private set; }
        public Player Player { get ; private set; }
        private CharacterController characterController;

        private void Start()
        {
            WFC = GetComponent<WaveFunctionCollapse>();
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();

            SetCursorState(CursorLockMode.Locked);
        }

        private void GenerateExhibit() 
        {

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

                default:
                    break;
            }
        }
    }
}
