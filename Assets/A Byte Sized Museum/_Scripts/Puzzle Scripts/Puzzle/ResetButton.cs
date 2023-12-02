using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class ResetButton : InteractableBase
    {
        [Header("Game Event")]
        public GameEvent onReset;

        public override void OnInteract()
        {
            onReset.Raise(this, name);
        }

    }
}
