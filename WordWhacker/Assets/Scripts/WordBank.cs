using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WordBank : MonoBehaviour
{
    private List<string> originalWords = new List<string>()
    {
        "the", "quick", "brown", "fox", "jumps", "over", "the", "lazy", "dog"
    };

    private List<string> workingWords = new List<string>();

    private void Awake()
    {
        // copy words to a new list
        workingWords.AddRange(originalWords);
        Shuffle(workingWords);
        ConvertToLower(workingWords);
    }

    private void Shuffle(List<string> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            int random = Random.Range(i, list.Count);

            // switches words in list
            string temporary = list[i];
            list[i] = list[random]; 
            list[random] = temporary;
        }
    }

    private void ConvertToLower(List<string> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            list[i] = list[i].ToLower();
        }
    }

    public string GetWord()
    {
        string newWord = string.Empty;

        if(workingWords.Count != 0)
        {
            newWord = workingWords.Last();
            workingWords.Remove(newWord);
        }
        else
        {
            newWord = "gameover";
        }
        return newWord;
    }
}