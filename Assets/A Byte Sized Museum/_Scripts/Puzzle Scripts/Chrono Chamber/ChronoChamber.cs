using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class ChronoChamber : InteractableBase
    {
        [SerializeField] private Timer timer;
        [SerializeField] private int fragmentsAmount = 0;

        [SerializeField] private TMP_Text fragmentsAmountText;

        public override void Start()
        {
            base.Start();
        }

        public override void OnLookEnter()
        {
            base.OnLookEnter();
            gameManager.UpdateToolTipText("Chrono Chamber", "Press E to repair");
        }

        private void Update()
        {
            if (gameManager.DebugModeEnabled)
            {
                if (Input.GetKeyDown(KeyCode.F))
                    Debug.Log($"Time left : {timer.RemainingTimeInSeconds}");

                if (Input.GetKeyDown(KeyCode.R))
                {
                    if (UseFragment())
                    {
                        gameManager.UpdateToolTipText($"Added {timer.SecondsToAdd}s", "to timer");
                        timer.AddSecondsToTimer(timer.SecondsToAdd);
                        UpdateFragmentsText();
                    }
                    else
                    {
                        gameManager.UpdateToolTipText("Not enough Fragments", "");
                    }
                }

                if (Input.GetKeyDown(KeyCode.G))
                {
                    AddFragment();
                    UpdateFragmentsText();
                }
            }
        }

        public bool UseFragment()
        {
            if (fragmentsAmount > 0)
            {
                fragmentsAmount--;
                return true;
            }
            return false;
        }

        private void AddFragment()
        {
            fragmentsAmount++;
        }

        private void UpdateFragmentsText()
        {
            fragmentsAmountText.text = $"Fragments: {fragmentsAmount}/0";
        }
    }
}
