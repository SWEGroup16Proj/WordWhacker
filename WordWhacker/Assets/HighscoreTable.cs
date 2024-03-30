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
   
    public TMP_Text scoreText;
    public TMP_Text nameText;
    [SerializeField] public Transform entryContainer;
    [SerializeField] public Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;
    private void Awake()
    {
       
        // Deactivate the entry template object
        entryTemplate.gameObject.SetActive(false);
        

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
        
        for(int i = 0;i<highscores.highscoreEntryList.Count;i++){
            for(int j=i+1;j<highscores.highscoreEntryList.Count;j++){
                if(highscores.highscoreEntryList[j].score>highscores.highscoreEntryList[i].score)
                {
                    //swap
                    HighscoreEntry tmp=highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i]=highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j]=tmp;
                }
            }
        }
        
        highscoreEntryTransformList=new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList){
            CreateHighscoreEntryTransform(highscoreEntry,entryContainer,highscoreEntryTransformList);
        }
       
       
       Debug.Log(PlayerPrefs.GetString("highscoreTable"));
       
       
    }
//Function to create/add entries into the list

private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container,List<Transform> transformList){
      float templateHeight = 40f;

        
            // Instantiate entry template as a child of the entry container
            Transform entryTransform = Instantiate(entryTemplate, container);

            // Get RectTransform component of the instantiated entry template
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();

            // Set anchored position of the instantiated entry template
            entryRectTransform.anchoredPosition = new Vector2(-361, -templateHeight * transformList.Count);

            // Activate the instantiated entry template object
            entryTransform.gameObject.SetActive(true);

         

        int score = highscoreEntry.score;
        scoreText.text=score.ToString();

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
            //adds entry
            highscores.highscoreEntryList.Add(highscoreEntry);

            //Saves updated scores
            string json=JsonUtility.ToJson(highscores);
            PlayerPrefs.SetString("highscoreTable",json);
            PlayerPrefs.Save();

        
}
//Represents single high score entry
[System.Serializable] private class HighscoreEntry{
        public int score; 
        public string name;
    }
}
