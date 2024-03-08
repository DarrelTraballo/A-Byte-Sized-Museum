using TMPro;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TMP_Text timerText;
        [SerializeField, Range(5f, 20f)] private int remainingTimeInMinutes = 15;
        [SerializeField, Range(10f, 30f)] private int secondsToAdd = 30;
        private float remainingTimeInSeconds;

        public int SecondsToAdd
        {
            get { return secondsToAdd; }
            set { secondsToAdd = Mathf.Clamp(value, 10, 30); } // Ensures secondsToAdd is within the range 10 to 30.
        }

        public float RemainingTimeInSeconds
        {
            get { return remainingTimeInSeconds; }
            set { remainingTimeInSeconds = Mathf.Max(value, 0); }
        }

        private GameManager gameManager;

        private void Start()
        {
            gameManager = GameManager.Instance;
            RemainingTimeInSeconds = (remainingTimeInMinutes * 60) + 5f;
        }

        private void Update()
        {
            if (RemainingTimeInSeconds > 0)
            {
                RemainingTimeInSeconds -= Time.deltaTime;
            }

            int minutes = Mathf.FloorToInt(RemainingTimeInSeconds / 60);
            int seconds = Mathf.FloorToInt(RemainingTimeInSeconds % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        }

        public void AddSecondsToTimer(int secondsToAdd)
        {
            RemainingTimeInSeconds += secondsToAdd;
        }
    }
}
