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
        public Dictionary<string, Sprite> keyboardIcons; // Dictionary to hold keyboard icons

        // private void OnEnable()
        // {
        //     InitializeKeyboardIcons();
        // }

        // private void InitializeKeyboardIcons()
        // {
        //     // Assuming you have a folder named "KeyboardIcons" in your Resources folder with all icons named after the key they represent
        //     keyboardIcons = new Dictionary<string, Sprite>();
        //     Sprite[] icons = Resources.LoadAll<Sprite>("KeyboardIcons");
        //     foreach (Sprite icon in icons)
        //     {
        //         keyboardIcons.Add(icon.name, icon);
        //     }
        // }

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
                    // dialogue.sentences = linesList.Select(line => ParseLine(line)).ToArray();
                }

            }
            else Debug.LogError("Dialogue File not assigned.");
        }

        // private object[] ParseLine(string line)
        // {
        //     List<object> parsedLine = new List<object>();
        //     var segments = Regex.Split(line, "&(.*?)&");

        //     foreach (var segment in segments)
        //     {
        //         if (keyboardIcons.ContainsKey(segment))
        //         {
        //             parsedLine.Add(keyboardIcons[segment]); // Add the icon sprite
        //         }
        //         else
        //         {
        //             parsedLine.Add(segment); // Add the regular text
        //         }
        //     }

        //     return parsedLine.ToArray();
        // }
    }
}
