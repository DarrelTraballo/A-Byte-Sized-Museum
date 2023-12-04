using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    [CreateAssetMenu(menuName = "Code Block", fileName = "CodeBlock")]
    public class CodeBlockData : ScriptableObject
    {
        public string codeBlockName;
        
        [TextArea] public string codeBlockDescription;

        public GameObject interpreterImage;


        // public Image inventoryImage;

        // TODO: 
        // data to be displayed sa bottom right panel ng interpreter
        
    }
}
