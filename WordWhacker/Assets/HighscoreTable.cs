using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//This script manages a highscore table UI in Unity. 
//It includes functionality to display highscores using TextMeshPro, 
//instantiate UI elements dynamically, 
//and set up default values for testing purposes.

public class HighscoreTable : MonoBehaviour
{
    // Variables to hold references to UI elements
   //[SerializeField] public GameObject entryContainer;
    //[SerializeField] public GameObject entryTemplate;
    public TMP_Text scoreText;
    public TMP_Text nameText;
    [SerializeField] public Transform entryContainer;
    [SerializeField] public Transform entryTemplate;
    private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;
    private void Awake()
    {
       
        // Deactivate the entry template object
        entryTemplate.gameObject.SetActive(false);
        highscoreEntryList=new List<HighscoreEntry>(){
            new HighscoreEntry{score = 777, name ="AAA"},
            new HighscoreEntry{score = 690, name ="CAT"},
            new HighscoreEntry{score = 680, name ="MAR"},
            new HighscoreEntry{score = 420, name ="GLO"},
        };

        highscoreEntryTransformList=new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscoreEntryList){
            CreateHighscoreEntryTransform(highscoreEntry,entryContainer,highscoreEntryTransformList);
        }
        {
            
        }
       
    }
//Function to create/add entries into the list

private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container,List<Transform> transformList){
      float templateHeight = 40f;

        // Loop to instantiate entry templates for testing purposes
            // Instantiate entry template as a child of the entry container
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);

            // Get RectTransform component of the instantiated entry template
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();

            // Set anchored position of the instantiated entry template
            entryRectTransform.anchoredPosition = new Vector2(-361, -templateHeight * transformList.Count);

            // Activate the instantiated entry template object
            entryTransform.gameObject.SetActive(true);

         // For Testing: Generate a random score and display it

        int score = highscoreEntry.score;
        scoreText.text=score.ToString();


        // For Testing: Set a default name
        string name = highscoreEntry.name;
        nameText.text=name;

        transformList.Add(entryTransform);
}
//Represents single high score entry
    private class HighscoreEntry{
        public int score; 
        public string name;
    }
}
