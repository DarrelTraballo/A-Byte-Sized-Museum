using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class ChronoChamber : InteractableBase
    {
        [SerializeField] private Timer timer;

        public override void Start()
        {
            base.Start();
            subtitleText = "Press E to REPAIR";
        }

        public override void OnLookEnter()
        {
            base.OnLookEnter();
            // TODO: ayaw mapatungan

        }

        public override void OnInteract()
        {
            base.OnInteract();
            if (timer.UseFragment())
            {
                timer.AddSecondsToTimer();
            }
            else
            {
                StartCoroutine(gameManager.SetToolTipTextCoroutine("Not enough Fragments", "", subtitleDelay));
            }
        }
    }
}
