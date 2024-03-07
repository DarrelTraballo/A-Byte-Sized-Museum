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

        private GameManager gameManager;

        private void Start()
        {
            gameManager = GameManager.Instance;
            remainingTimeInSeconds = (remainingTimeInMinutes * 60) + 5f;
        }

        private void Update()
        {
            if (remainingTimeInSeconds > 0)
            {
                remainingTimeInSeconds -= Time.deltaTime;
                remainingTimeInSeconds = Mathf.Max(remainingTimeInSeconds, 0);
            }

            int minutes = Mathf.FloorToInt(remainingTimeInSeconds / 60);
            int seconds = Mathf.FloorToInt(remainingTimeInSeconds % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);


            if (gameManager.DebugModeEnabled)
            {
                if (Input.GetKeyDown(KeyCode.R))
                    AddSecondsToTimer(secondsToAdd);
            }
        }

        private void AddSecondsToTimer(int secondsToAdd)
        {
            remainingTimeInSeconds += secondsToAdd;
        }
    }
}
