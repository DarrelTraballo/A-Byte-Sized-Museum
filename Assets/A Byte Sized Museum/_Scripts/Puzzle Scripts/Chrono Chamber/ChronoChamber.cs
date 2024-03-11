using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class ChronoChamber : InteractableBase
    {
        [SerializeField] private Timer timer;

        public override void OnLookEnter()
        {
            base.OnLookEnter();
            // TODO: ayaw mapatungan
            gameManager.UpdateToolTipText("Chrono Chamber", "Press E to repair");
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
                gameManager.UpdateToolTipText("Not enough Fragments", "");
            }
        }
    }
}
