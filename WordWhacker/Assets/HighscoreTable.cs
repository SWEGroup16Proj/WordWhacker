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
       /* Tester
        highscoreEntryList=new List<HighscoreEntry>(){
            new HighscoreEntry{score = 777, name ="AAA"},
            new HighscoreEntry{score = 425, name ="CAT"},
            new HighscoreEntry{score = 680, name ="MAR"},
            new HighscoreEntry{score = 420, name ="GLO"},
            new HighscoreEntry{score = 800, name ="LOL"},
        };
        */

        string jsonString=PlayerPrefs.GetString("highscoreTable");
        Highscores highscores=JsonUtility.FromJson<Highscores>(jsonString);

          if (highscores == null) {
            // There's no stored table, initialize
            Debug.Log("Initializing table with default values...");
            AddHighscoreEntry(999, "CMK");
            AddHighscoreEntry(555, "JOE");
            AddHighscoreEntry(333, "DAV");
            AddHighscoreEntry(222, "CAT");
            AddHighscoreEntry(111, "MAX");
            AddHighscoreEntry(444, "AAA");
            // Reload
            jsonString = PlayerPrefs.GetString("highscoreTable");
            highscores = JsonUtility.FromJson<Highscores>(jsonString);
        }
        //Sorter
        for(int i =0;i<highscoreEntryList.Count;i++){
            for(int j=i+1;j<highscoreEntryList.Count;j++){
                if(highscoreEntryList[j].score>highscoreEntryList[i].score)
                {
                    //swap
                    HighscoreEntry tmp=highscoreEntryList[i];
                    highscoreEntryList[i]=highscoreEntryList[j];
                    highscoreEntryList[j]=tmp;
                }
            }
        }
        highscoreEntryTransformList=new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscoreEntryList){
            CreateHighscoreEntryTransform(highscoreEntry,entryContainer,highscoreEntryTransformList);
        }
       
       //saving system
       /*
       Testers
       Highscores highscores=new Highscores{highscoreEntryList=highscoreEntryList};
       string json=JsonUtility.ToJson(highscores);
       PlayerPrefs.SetString("highscoreTable",json);
       PlayerPrefs.Save();
       Debug.Log(PlayerPrefs.GetString("highscoreTable"));
       */
    }
//Function to create/add entries into the list

private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container,List<Transform> transformList){
      float templateHeight = 40f;

        // Loop to instantiate entry templates for testing purposes
            // Instantiate entry template as a child of the entry container
            Transform entryTransform = Instantiate(entryTemplate, container);

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

private class Highscores{
    public List<HighscoreEntry> highscoreEntryList;
}

private void AddHighscoreEntry(int score, string name) {
        // Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };
        
        // Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null) {
            // There's no stored table, initialize
            highscores = new Highscores() {
                highscoreEntryList = new List<HighscoreEntry>()
            };
        }
}
//Represents single high score entry
[System.Serializable]
    private class HighscoreEntry{
        public int score; 
        public string name;
    }
}
