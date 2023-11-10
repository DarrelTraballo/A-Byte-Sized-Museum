using UnityEngine;

namespace KaChow.AByteSizedMuseum {
    public class WallPuzzle : InteractableBase
    {
        [SerializeField]
        private GameObject puzzlePrefab;
        private GameObject puzzlePrefabInstance;
        [SerializeField]
        private bool isPuzzleOpen;

        private void Start()
        {
            gameManager = GameManager.Instance;
        }
        // Simple word puzzle lang muna
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && isPuzzleOpen)
            {
            ClosePuzzle();
            }
        }

        public override void OnInteract()
        {
            if (!isPuzzleOpen)
            {
                OpenPuzzle();
            }
        }

        private void OpenPuzzle() 
        {
            if (puzzlePrefab == null) 
            {
                Debug.Log("Puzzle Prefab not set");
                return;
            }

            gameManager.Player.canMove = false;
            // gameManager.SetCursorState(CursorLockMode.Confined);

            if (puzzlePrefabInstance == null) 
            {
                puzzlePrefabInstance = Instantiate(puzzlePrefab);
                puzzlePrefabInstance.transform.SetParent(GameObject.Find("Canvas").transform, false);
                isPuzzleOpen = true;
            }
            else
            {
                isPuzzleOpen = !isPuzzleOpen;
                puzzlePrefabInstance.SetActive(isPuzzleOpen);
            }
        }

        public void ClosePuzzle()
        {
            gameManager.Player.canMove = true;
            // gameManager.SetCursorState(CursorLockMode.Locked);

            puzzlePrefabInstance.SetActive(false);
            isPuzzleOpen = false;
        }
    }
}