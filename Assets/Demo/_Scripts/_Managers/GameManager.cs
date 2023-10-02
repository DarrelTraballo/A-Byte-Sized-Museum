using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace KaChow.Demo {
// Manages the entire game
    public class GameManager : MonoBehaviour
    {
        // Singleton reference, para isang instance lang ang kinukuha pag need i-reference from another script
        public static GameManager Instance { get; private set; }
        private void Awake() 
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else 
                Instance = this;

        }

        [Header("Levels")]
        public List<LevelData> levels;

        [Header("Level Info")]
        public int currentLevelIndex = 1;
        public TextMeshProUGUI txtMissionUpdate;
        public TextMeshProUGUI txtInteractMessage;
        public TextMeshProUGUI crossHairText;

        [Header("Level Elements")]
        public bool isDoorUnlocked;
        [SerializeField]
        public int levelUnlockProgress;
        public int levelUnlockCounter;
        private GameObject currentLevelPrefab;
        public Player Player { get; private set; }
        private CharacterController characterController;

        private void Start() 
        {
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
            SetCursorState(CursorLockMode.Locked);
            LoadLevel(currentLevelIndex);
        }

        private void Update() 
        {

        }

        // TODO: LEVEL 4
        //          NEED TO COMPLETE MULTIPLE PUZZLES/COLLECT N KEYS FOR DOOR TO UNLOCK
        //          > LevelManager?                

        // TODO: FIX PROMPTS
        //         > UIManager for UI elements? idk

        // TODO: REFACTOR EVERYTHING AND USE UnityEvents :>

        public void LoadNextLevel()
        {
            currentLevelIndex++;
            if (currentLevelIndex >= levels.Count)
            {
                Debug.Log("No more levels to load");
                return;
            }

            Destroy(currentLevelPrefab);
            LoadLevel(currentLevelIndex);
        }

        public void LoadLevel(int levelIndex)
        {
            var levelToLoad = levels[levelIndex];
            currentLevelPrefab = Instantiate(levelToLoad.levelPrefab);
            isDoorUnlocked = levelToLoad.isDoorUnlocked;
            levelUnlockProgress = levelToLoad.unlockConditions;
            levelUnlockCounter = 0;

            txtMissionUpdate.text = levelToLoad.levelMissionText;
            txtInteractMessage.text = levelToLoad.levelInteractText;

            if (Player == null) 
            {
                Debug.Log("no player found");
                return;
            }

            characterController.enabled = false;
            Player.transform.SetPositionAndRotation(levelToLoad.playerPosition, Quaternion.identity);
            characterController.enabled = true;
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
