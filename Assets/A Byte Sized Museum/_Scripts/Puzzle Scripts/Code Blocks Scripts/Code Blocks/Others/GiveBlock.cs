using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class GiveBlock : CodeBlock
    {
        public override IEnumerator ExecuteBlock()
        {
            Debug.Log("Give Block");
            yield return new WaitForSeconds(delay);
        }
    }
}
