using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Typer : MonoBehaviour
{
    public TMP_Text wordOutput = null;
    public Spawner spawner = null;
    public WordBank wordBank = null;
    private GameObject enemyObject;
    
    [SerializeField] Player player;
    private bool canType = true;
    private int wordCounter = 0;

    private string newWord = string.Empty;
    private string typedWord = string.Empty;

    // Start is called before the first frame update
    private void Start()
    {
        // GetNewWord();
        wordOutput.text = string.Empty;
    }

    // Update is called once per frame
    private void Update()
    {
        if(canType)
        {
            CheckInput();
        }
    }

    // get word from word bank
    public string GetNewWord()
    {
        newWord = wordBank.GetWord();

        return newWord;
    }

    // process user input for letters pressed
    private void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Delete))
        {
            RemoveLetter();
        }
        else if(Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;
            keysPressed.ToLower();

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

    // check if letter match with input
    private void EnterLetter(string typedLetter)
    {
        if(IsCorrectLetter(typedLetter))
        {
            AddLetter(typedLetter);

            if(IsWordComplete())
            {
                // Reset typed buffer
                typedWord = string.Empty;
                wordOutput.text = string.Empty;

                // WIP: calculate WPM
                wordCounter++;
            }
        }
    }

    // true if user buffer matches with enemy words
    private bool IsCorrectLetter(string letter)
    {
        // Find all instances of the enemy word prefab
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemyObject in enemyObjects)
        {
            TMP_Text textComponent = enemyObject.transform.GetChild(0).GetComponent<TMP_Text>();

            // Check if the text matches the typed string
            if (textComponent.text.StartsWith(typedWord + letter))
            {
                // Debug.Log("Match found");
                return true;
            }
        }

        return false;
    }

    // add letter to user typed buffer
    private void AddLetter(string typedLetter)
    {
        wordOutput.text = typedWord + typedLetter;
        typedWord += typedLetter;
    }

    // delete letter from user typed buffer
    private void RemoveLetter()
    {
        if (typedWord.Length > 0)
        {
            typedWord = typedWord.Remove(typedWord.Length - 1);
            wordOutput.text = typedWord;
        }
    }

    // delete enemy word once typed out completely
    private bool IsWordComplete()
    {
        // Find all instances of the enemy word prefab
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemyObject in enemyObjects)
        {
            TMP_Text textComponent = enemyObject.transform.GetChild(0).GetComponent<TMP_Text>();

            // Check if the text matches the typed string
            if (textComponent.text == typedWord)
            {
                Debug.Log("Destroy Word");
                player.IncrementPoints(5);
                Destroy(enemyObject);
                
                // Debug.Log("No Enemy: " + GameObject.FindWithTag("Enemy"));

                // if(GameObject.Find("Enemy(Clone)") == null)
                // {
                //     Debug.Log("SPAWN NOW");
                //     spawner.SpawnNow();
                // }

                return true;
            }
        }

        return false;
    }

    // trigger game over once it reached bottom of screen
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(player.health>0)
        {
            player.health--;
        }
        else
        {
                spawner.canSpawn = false;
                 canType = false;

                 Debug.Log("GAMEOVER");
             wordOutput.text = "<color=red>Game Over</color>";

             // to prevent memory overload
             GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
             foreach (GameObject enemyObject in enemyObjects)
                     {
                      Destroy(enemyObject);

                     }       
        }
        
    }
}