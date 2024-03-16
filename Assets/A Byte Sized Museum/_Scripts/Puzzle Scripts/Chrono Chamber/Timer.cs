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
        private DevConsoleManager devConsoleManager;

        private void Start()
        {
            gameManager = GameManager.Instance;
            devConsoleManager = DevConsoleManager.Instance;
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
        }

        public void AddSecondsToTimer()
        {
            AddSecondsToTimer(secondsToAdd);
        }

        public void AddSecondsToTimer(int secondsToAdd)
        {
            StartCoroutine(gameManager.SetToolTipTextCoroutine($"Added {secondsToAdd}s", "to timer"));
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
                StartCoroutine(gameManager.SetToolTipTextCoroutine("Puzzle Solved!", "Received FRAGMENT"));
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
    }
}
