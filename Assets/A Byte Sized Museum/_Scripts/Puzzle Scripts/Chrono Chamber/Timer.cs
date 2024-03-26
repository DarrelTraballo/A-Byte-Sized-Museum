using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace KaChow.AByteSizedMuseum
{
    public class Timer : MonoBehaviour
    {
        public static Timer Instance { get; private set; }
        private Timer() { }

        [SerializeField] private TMP_Text timerText;
        [SerializeField] private TMP_Text fragmentsAmountText;

        private TimeSpan remainingTime;
        private int timeAllowance;
        private int secondsToAdd;

        private int fragmentsAmount = 0;
        private int totalFragments;

        private GameManager gameManager;


        [SerializeField] private List<Schedule> schedules;

        [Serializable]
        private class Schedule
        {
            public int Minutes;
            public int Seconds;
            public UnityEvent action;
        }

        private void Awake()
        {
            if (Instance != this && Instance != null)
                Destroy(this);
            else
                Instance = this;
        }

        private void Start()
        {
            gameManager = GameManager.Instance;
            totalFragments = gameManager.PuzzleExhibitAmount;

            timeAllowance = gameManager.TimeAllowance;
            remainingTime = TimeSpan.FromSeconds((gameManager.RemainingTimeInMinutes * 60) + timeAllowance);
            secondsToAdd = gameManager.SecondsToAdd;

            UpdateFragmentsText();
        }

        private void Update()
        {
            if (remainingTime.TotalSeconds > 0)
            {
                remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(Time.deltaTime));
                CheckSchedule();
            }
            else
            {
                if (gameManager.currentState != GameState.GameOver)
                    gameManager.SetGameState(GameState.GameOver);
            }

            timerText.text = string.Format("{0:00}:{1:00}", remainingTime.Minutes, remainingTime.Seconds);
        }

        public void AddSecondsToTimer()
        {
            AddSecondsToTimer(secondsToAdd);
        }

        public void AddSecondsToTimer(int secondsToAdd)
        {
            StartCoroutine(gameManager.SetToolTipTextCoroutine($"Added {secondsToAdd}s", "to timer"));
            remainingTime = remainingTime.Add(TimeSpan.FromSeconds(secondsToAdd));
            UpdateFragmentsText();
        }

        public void RemoveSecondsToTimer(int secondsToRemove)
        {
            StartCoroutine(gameManager.SetToolTipTextCoroutine($"Removed {secondsToRemove}s", "to timer"));
            remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(secondsToRemove));
        }

        private void CheckSchedule()
        {
            var schedule = schedules.FirstOrDefault(s =>
                s.Minutes == remainingTime.Minutes &&
                s.Seconds == remainingTime.Seconds);

            schedule?.action?.Invoke();
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

            if (totalFragments == 5)
            {
                Debug.Log("We're Halfway there!!");
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

        public void ScheduleTest(DialogueSO test)
        {
            StartCoroutine(gameManager.SetToolTipTextCoroutine("15:00 schedule", "Triggered!"));
        }
    }
}
