using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class MoveBlock : CodeBlock
    {
        [Header("Game Event")]
        public GameEvent onMove;

        public override IEnumerator ExecuteBlock(int botID)
        {
            onMove.Raise(this, botID);
            yield return new WaitForSeconds(delay);
        }
    }
}
