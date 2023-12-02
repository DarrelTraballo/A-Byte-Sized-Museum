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
        private WaveFunctionCollapse waveFunctionCollapse;
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

        private MuseumGenerator museumGenerator;

        [Header("Player variables")]
        [SerializeField] private Vector3 playerStartPosition;
        public Player Player { get ; private set; }

        private CharacterController characterController;

        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();

            // SetCursorState(CursorLockMode.Locked);

            museumGenerator = MuseumGenerator.Instance;
            museumGenerator.Initialize();
            museumGenerator.GenerateExhibits();

            waveFunctionCollapse= GetComponent<WaveFunctionCollapse>();

            if (waveFunctionCollapse != null)
            {
                // Subscribe to the InitializationCompleteEvent
                Debug.Log("Intialization DONE");
                waveFunctionCollapse.InitializationCompleteEvent.AddListener(OnInitializationComplete);
            }
            else
            {
                // Subscribe to the InitializationCompleteEvent
                Debug.Log("Intialization IS NOT DONE");
                //waveFunctionCollapse.InitializationCompleteEvent.AddListener(OnInitializationComplete);
            }

        }

        /// //////////////////////////////////////////////////////////////////////////////

        private void OnInitializationComplete()
        {
            // Your code to execute after initialization is complete
            waveFunctionCollapse.DisableExhibits();
            Debug.Log("Disabling EXHIBIT DONE");
        }
        //////////////////////////////////////////////////////////////////////////////////

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
