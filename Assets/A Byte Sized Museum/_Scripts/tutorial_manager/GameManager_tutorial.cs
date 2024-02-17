using KaChow.WFC;
using KaChow.Demo;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class GameManager_tutorial : MonoBehaviour
    {
        // Singleton reference, para isang instance lang ang kinukuha pag need i-reference from another script
        // can only access GameManager by using GameManager.Instance
        public static GameManager_tutorial Instance { get; private set; }
        #region Singleton
        private GameManager_tutorial() { }
        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;

        }
        #endregion

        [Header("Player variables")]
        [SerializeField] private Vector3 playerStartPosition;
        public Player Player { get; private set; }

        private CharacterController characterController;

        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        }

        // public void SetCursorState(CursorLockMode cursorLockMode)
        // {
        //     Cursor.lockState = cursorLockMode;
        //     switch (cursorLockMode)
        //     {
        //         case CursorLockMode.Locked:
        //             Cursor.visible = false;
        //             break;

        //         case CursorLockMode.Confined:
        //             Cursor.visible = true;
        //             break;

        //         default:
        //             break;
        //     }
        // }
    }
}
