using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class DropBlock : CodeBlock
    {
        [Header("Game Event")]
        public GameEvent onDrop;

        public override IEnumerator ExecuteBlock(int botID)
        {
            onDrop.Raise(this, botID);
            yield return new WaitForSeconds(delay);
        }
    }
}
