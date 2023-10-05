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
        [Header("Exhibit Generator variables")]
        [SerializeField] private ExhibitGenerator exhibitGenerator;
        [SerializeField] private int exhibitCount = 1;

        [Space]
        [Range(15, 30)]
        [SerializeField] private int minGridSize;

        [Range(15, 30)]
        [SerializeField] private int maxGridSize;

        [Space]
        [SerializeField] private int rows;
        [SerializeField] private int cols;

        [Header("Player variables")]
        [SerializeField] private Vector3 playerStartPosition;
        public Player Player { get ; private set; }


        public WaveFunctionCollapse WFC { get; private set; }
        private CharacterController characterController;

        private void Start()
        {
            WFC = GetComponent<WaveFunctionCollapse>();
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();

            SetCursorState(CursorLockMode.Locked);

            GenerateExhibits();
        }

        private void GenerateExhibits() 
        {
            Vector3 exhibitStartPos;
            for (int i = 0; i < exhibitCount; i++)
            {
                rows = Random.Range(minGridSize, maxGridSize + 1);
                cols = Random.Range(minGridSize, maxGridSize + 1);
                
                if (i == 0) 
                    exhibitStartPos = new Vector3(0f, 0f, 0f);
                else
                    exhibitStartPos = new Vector3(Random.Range(-50f, 50f), 0f, Random.Range(-50f, 50f));
                    // exhibitGenerator.GenerateExhibit(new Vector3(-20, 0, -15), rows, cols, i);
                    
                exhibitGenerator.GenerateExhibit(exhibitStartPos, rows, cols, i);
            }
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
