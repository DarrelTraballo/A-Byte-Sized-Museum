using System;
using UnityEngine;
using TMPro;

namespace KaChow.AByteSizedMuseum
{
    public class Stopwatch : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI stopwatchText;
        [SerializeField] private TextMeshProUGUI onCompleteText;
        [SerializeField] private GameObject onCompletePanel;

        private float currentTime = 0f;
        private bool timerActive = false;
        private TimeSpan timeSpan;

        // reference ng ending time ng player
        private TimeSpan stoppedTimeSpan;

        private void Update()
        {
            if (timerActive)
            {
                currentTime += Time.deltaTime;

                timeSpan = TimeSpan.FromSeconds(currentTime);

                stopwatchText.text = timeSpan.ToString(@"mm\:ss");
            }
        }

        private void StartTimer()
        {
            timerActive = true;
        }

        private void StopTimer()
        {
            timerActive = false;
        }

        private void ResetTimer()
        {
            timerActive = false;
            currentTime = 0f;
        }

        private void DisplayCompleteMessage()
        {
            onCompletePanel.SetActive(true);

            stoppedTimeSpan = TimeSpan.FromSeconds(currentTime);
            string stoppedTime = stoppedTimeSpan.ToString(@"mm\:ss");

            onCompleteText.text = 
                "Congratulations!\n" + 
                "You completed the game in\n" +
                $"{stoppedTime}";
        }

        private void OnTriggerEnter(Collider actor)
        {
            if (actor.CompareTag("Player"))
            {
                StartTimer();
            }
        }

        private void OnTriggerExit(Collider actor)
        {
            if (actor.CompareTag("Player"))
            {
                StopTimer();
                DisplayCompleteMessage();
            }
        }
    }
}
