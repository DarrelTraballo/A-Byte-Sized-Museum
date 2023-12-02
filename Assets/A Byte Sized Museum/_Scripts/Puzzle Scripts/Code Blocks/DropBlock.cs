using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class DropBlock : CodeBlock
    {
        [Header("Game Event")]
        public GameEvent onDrop;

        public override IEnumerator ExecuteBlock()
        {
            Debug.Log("Drop Block");
            onDrop.Raise(this, name);
            yield return new WaitForSeconds(delay);
        }
    }
}
