using TMPro;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private TMP_Text fragmentsAmountText;
        private float remainingTimeInSeconds;
        private int secondsToAdd;

        private int fragmentsAmount = 0;
        private int totalFragments;

        public float RemainingTimeInSeconds
        {
            get { return remainingTimeInSeconds; }
            set { remainingTimeInSeconds = Mathf.Max(value, 0); }
        }

        private GameManager gameManager;

        private void Start()
        {
            gameManager = GameManager.Instance;
            RemainingTimeInSeconds = (gameManager.RemainingTimeInMinutes * 60) + 5f;
            totalFragments = gameManager.PuzzleExhibitAmount;
            secondsToAdd = gameManager.SecondsToAdd;

            UpdateFragmentsText();
        }

        private void Update()
        {
            if (RemainingTimeInSeconds > 0)
            {
                RemainingTimeInSeconds -= Time.deltaTime;
            }
            else
            {
                if (gameManager.currentState != GameState.GameOver)
                    gameManager.SetGameState(GameState.GameOver);
            }

            int minutes = Mathf.FloorToInt(RemainingTimeInSeconds / 60);
            int seconds = Mathf.FloorToInt(RemainingTimeInSeconds % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            DebugCommands();
        }

        public void AddSecondsToTimer()
        {
            AddSecondsToTimer(secondsToAdd);
        }

        private void AddSecondsToTimer(int secondsToAdd)
        {
            StartCoroutine(gameManager.SetToolTipText($"Added {secondsToAdd}s", "to timer", 1.5f));
            RemainingTimeInSeconds += secondsToAdd;
            UpdateFragmentsText();
        }

        public void RemoveSecondsToTimer(int secondsToRemove)
        {
            RemainingTimeInSeconds -= secondsToRemove;
        }

        public bool UseFragment()
        {
            Debug.Log("Called");
            if (fragmentsAmount > 0 && totalFragments > 0)
            {
                fragmentsAmount--;
                totalFragments--;
                CheckWinCondition();
                return true;
            }
            UpdateFragmentsText();
            return false;
        }

        private void CheckWinCondition()
        {
            if (totalFragments <= 0)
            {
                gameManager.SetGameState(GameState.PlayerWin);
            }
        }

        public void AddFragment()
        {
            if (fragmentsAmount < totalFragments)
            {
                StartCoroutine(gameManager.SetToolTipText("Puzzle Solved!", "Received FRAGMENT", 5f));
                fragmentsAmount++;
                UpdateFragmentsText();
            }
            else
            {
                Debug.Log("Cannot add more fragments. Maximum reached.");
            }
        }

        private void UpdateFragmentsText()
        {
            fragmentsAmountText.text = $"Fragments: {fragmentsAmount}/{totalFragments}";
        }

        private void DebugCommands()
        {
            if (!gameManager.DebugModeEnabled) return;

            if (Input.GetKeyDown(KeyCode.F))
                Debug.Log($"Time left : {RemainingTimeInSeconds}");

            if (Input.GetKeyDown(KeyCode.G))
                AddFragment();

            if (Input.GetKeyDown(KeyCode.H))
                RemoveSecondsToTimer(60);
        }
    }
}
