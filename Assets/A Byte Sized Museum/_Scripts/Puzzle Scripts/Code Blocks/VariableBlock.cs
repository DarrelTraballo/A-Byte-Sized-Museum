using System.Collections;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class VariableBlock : CodeBlock
    {
        public override IEnumerator ExecuteBlock()
        {
            Debug.Log("Variable Block");
            yield return new WaitForSeconds(1f);
        }
    }
}
