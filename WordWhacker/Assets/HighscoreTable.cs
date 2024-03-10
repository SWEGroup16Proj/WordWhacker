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
    public Transform entryContainer;
    public Transform entryTemplate;

    private void Awake()
    {
        // Find the entry container and entry template objects in the hierarchy
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = transform.Find("highscoreEntryTemplate");

        // Check if entry container or entry template is not found, log error and return
        if (entryContainer == null)
        {
            Debug.LogError("Entry container not found!");
            return;
        }

        if (entryTemplate == null)
        {
            Debug.LogError("Entry template not found!");
            return;
        }

        // Deactivate the entry template object
        entryTemplate.gameObject.SetActive(false);

        // Calculate the height of the entry template
        float templateHeight = 20f;

        // Loop to instantiate entry templates for testing purposes
        for (int i = 0; i < 4; i++)
        {
            // Instantiate entry template as a child of the entry container
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);

            // Get RectTransform component of the instantiated entry template
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();

            // Set anchored position of the instantiated entry template
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);

            // Activate the instantiated entry template object
            entryTransform.gameObject.SetActive(true);
        }

        // For Testing: Generate a random score and display it
        int score = Random.Range(0, 1000);
        scoreText.text = score.ToString();

        // For Testing: Set a default name
        string name = "AAA";
        nameText.text = name;
    }
}
