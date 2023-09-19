using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordPuzzle : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> letters;
    [SerializeField]
    private TextMeshProUGUI puzzleRemarks;
    private int currentIndex = 0;
    private string answer = "";
    private WallPuzzle wallPuzzle;

    private void Start() 
    {
        wallPuzzle = FindObjectOfType<WallPuzzle>();
    }

    private void Update()
    {
        // on key press, lalabas sa mga blanks yung tinype na letter
        if (Input.anyKeyDown)
        {
            foreach (char c in Input.inputString)
            {
                // Check if the character is a letter and within the array bounds
                if (char.IsLetter(c) && currentIndex < letters.Count)
                {
                    // Assign the character to the current TextMeshProUGUI
                    var nextLetter = char.ToUpper(c).ToString();
                    letters[currentIndex].text = nextLetter;
                    answer += nextLetter;

                    // Move to the next TextMeshProUGUI
                    currentIndex++;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (currentIndex > 0) 
            {
                currentIndex--;
                answer = answer.Remove(answer.Length - 1);
                letters[currentIndex].text = "";
            }
        }
    }

    public void SubmitAnswer()
    {
        if (answer == "KEY") 
        {
            // COnnect this bullshit wihth the door opening thingy
            Debug.Log("Correct");
            wallPuzzle.ClosePuzzle();
            GameManager.Instance.isDoorUnlocked = true;
        }
        else 
        {
            puzzleRemarks.text = "Wrong";
        }
    }
}
