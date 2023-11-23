using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class MoveBlock : CodeBlock
    {
        public override IEnumerator ExecuteBlock()
        {
            Debug.Log("Move Block");
            yield return new WaitForSeconds(1f);
        }
    }
}
