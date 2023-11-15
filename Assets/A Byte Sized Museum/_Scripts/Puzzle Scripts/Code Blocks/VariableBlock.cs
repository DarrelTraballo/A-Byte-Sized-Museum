using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    public class VariableBlock : InteractableBase, ICodeBlock
    {
        [Header("Variable Block Variables")]
        public string variableName;
        public int variableValue;

        [Header("UI Prefab")]
        public GameObject UIPrefab;

        public override void Start()
        {
            base.Start();
        }

        public void ExecuteBlock()
        {
            throw new System.NotImplementedException();
        }
    }
}
