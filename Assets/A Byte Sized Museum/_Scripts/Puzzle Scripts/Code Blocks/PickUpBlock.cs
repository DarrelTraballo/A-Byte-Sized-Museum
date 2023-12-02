using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class PickUpBlock : CodeBlock
    {
        [Header("Game Event")]
        public GameEvent onPickUp;

        public override IEnumerator ExecuteBlock()
        {
            Debug.Log("Pick Up Block");
            onPickUp.Raise(this, name);
            yield return new WaitForSeconds(delay);
        }
    }
}
