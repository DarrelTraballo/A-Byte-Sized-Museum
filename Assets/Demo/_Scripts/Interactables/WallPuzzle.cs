using UnityEngine;

public class WallPuzzle : InteractableBase
{
    [SerializeField]
    private GameObject puzzlePrefab;
    private GameObject puzzlePrefabInstance;
    [SerializeField]
    private bool isPuzzleOpen;

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

        GameManager.Instance.Player.canMove = false;
        GameManager.Instance.SetCursorState(CursorLockMode.Confined);

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
        GameManager.Instance.Player.canMove = true;
        GameManager.Instance.SetCursorState(CursorLockMode.Locked);

        puzzlePrefabInstance.SetActive(false);
        isPuzzleOpen = false;
    }
}
