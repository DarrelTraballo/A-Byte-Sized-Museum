using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class MoveBlock : CodeBlock
    {
        [Header("Game Event")]
        public GameEvent onMove;

        public override IEnumerator ExecuteBlock()
        {
            Debug.Log("Move Block");
            onMove.Raise(this, name);
            yield return new WaitForSeconds(1f);
        }
    }
}
