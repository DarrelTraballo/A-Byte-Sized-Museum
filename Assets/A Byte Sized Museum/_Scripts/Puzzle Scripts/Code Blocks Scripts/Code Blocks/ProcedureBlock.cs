using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KaChow.AByteSizedMuseum
{
    public class ProcedureBlock : CodeBlock
    {
        /*
            initially, wala yung holder ng method block sa bottom right panel
                > only appears when method block is in "edit mode"
                    - basically whenever the method block is clicked.

            white execution indicator stops on this block's line
            execution indicator appears on Method Block Lines UI, executes all lines inside method block
            then continues executing the other lines of interpreter
        */

        [SerializeField] private GameObject procedureBlockHolder;
        [SerializeField] private GameEvent onProcedureBlockClick;

        [SerializeField] private List<InterpreterLine> procedureInterpreterLines;

        public override IEnumerator ExecuteBlock(int botID)
        {
            throw new System.NotImplementedException();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            onProcedureBlockClick.Raise(this, this);
        }
    }
}
