using System.Collections;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class ForLoopBlock : CodeBlock
    {
        public override IEnumerator ExecuteBlock()
        {
            Debug.Log("For Loop Block");
            yield return new WaitForSeconds(delay);
        }
    }
}
