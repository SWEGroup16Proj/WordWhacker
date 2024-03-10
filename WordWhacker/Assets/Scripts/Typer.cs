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

    private string remainingWord = string.Empty;
    private string currentWord = string.Empty;

    // Start is called before the first frame update
    private void Start()
    {
        SetCurrentWord();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckInput();
    }

    private void SetCurrentWord()
    {
        // Get word from word bank
        currentWord = wordBank.GetWord();
        SetRemainingWord(currentWord);
    }

    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;
        wordOutput.text = remainingWord;
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
                // only take the first letter of string
                EnterLetter(keysPressed.Substring(0,1));
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
                SetCurrentWord();
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
        string newString = remainingWord.Remove(0,1);
        SetRemainingWord(newString);
    }

    private bool IsWordComplete()
    {
        return remainingWord.Length == 0;
    }
}