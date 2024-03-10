using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Typer : MonoBehaviour
{
    public TMP_Text wordOutput = null;
    public WordBank wordBank = null;

    private string currentWord = string.Empty;
    private string typedWord = string.Empty;
    private string remainingWord = string.Empty;

    // Start is called before the first frame update
    private void Start()
    {
        GetNewWord();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckInput();
    }

    private void GetNewWord()
    {
        // Reset typed buffer
        typedWord = string.Empty;

        // Get word from word bank
        currentWord = wordBank.GetWord();
        DisplayWord(typedWord, currentWord);
    }

    private void DisplayWord(string oldString, string newString)
    {
        typedWord += oldString;
        remainingWord = newString;
        wordOutput.text = "<color=grey>" + typedWord + "</color>" + remainingWord;
    }

    private void CheckInput()
    {
        if(Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;

            if(keysPressed.Length == 1)
            {
                EnterLetter(keysPressed);
            }
            else if(keysPressed.Length > 1)
            {
                for(int i = 0; i < keysPressed.Length; i++)
                {
                    EnterLetter(keysPressed.Substring(i,1));
                }
            }
        }
    }

    private void EnterLetter(string typedLetter)
    {
        if(IsCorrectLetter(typedLetter))
        {
            RemoveLetter();

            if(IsWordComplete())
            {
                GetNewWord();
            }
        }
    }

    private bool IsCorrectLetter(string letter)
    {
        // first letter of remaining word at index 0
        return remainingWord.IndexOf(letter) == 0;
    }

    private void RemoveLetter()
    {
        string oldString = remainingWord.Substring(0,1);
        string newString = remainingWord.Remove(0,1);
        DisplayWord(oldString, newString);
    }

    private bool IsWordComplete()
    {
        return remainingWord.Length == 0;
    }
}