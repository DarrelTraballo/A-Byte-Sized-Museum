using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class PickUpBlock : CodeBlock
    {
        [Header("Game Event")]
        public GameEvent onPickUp;

        public override IEnumerator ExecuteBlock(int botID)
        {
            onPickUp.Raise(this, botID);
            yield return new WaitForSeconds(delay);
        }
    }
}
