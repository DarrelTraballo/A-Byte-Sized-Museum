using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class MethodBlock : CodeBlock
    {
        /* 
            initially, wala yung holder ng method block sa bottom right panel
                > only appears when method block is in "edit mode"
                    - basically whenever the method block is clicked.

            white execution indicator stops on this block's line
            execution indicator appears on Method Block Lines UI, executes all lines inside method block
            then continues executing the other lines of interpreter
        */

        [SerializeField] private GameObject methodBlockHolder;
        // TODO: figure out how to spawn the holder to the bottom right panel of interpreter UI
        [SerializeField] private GameObject bottomRightPanel;

        public override IEnumerator ExecuteBlock()
        {
            throw new System.NotImplementedException();
        }
    }
}
