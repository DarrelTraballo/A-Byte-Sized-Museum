using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace KaChow.AByteSizedMuseum
{
    [CreateAssetMenu(fileName = "New Dialogue Script", menuName = "Scriptable Objects/Dialogue Script")]
    public class DialogueSO : ScriptableObject
    {
        public TextAsset dialogueFile;
        public Dialogue dialogue;

        public void LoadDialogueFromFile()
        {
            if (dialogueFile != null)
            {
                string fileContents = dialogueFile.text;
                string[] allLines = Regex.Split(fileContents, @"\n\s*\n+");

                if (allLines.Length > 0)
                {
                    dialogue.name = allLines[0];

                    var linesList = allLines.ToList();
                    linesList.RemoveAt(0);
                    dialogue.sentences = linesList.ToArray();
                }

            }
            else Debug.LogError("Dialogue File not assigned.");
        }
    }
}
