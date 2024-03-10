using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreTable : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text nameText;
    public Transform entryContainer;
    public Transform entryTemplate;

    private void Awake()
    {
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = transform.Find("highscoreEntryTemplate");

        if (entryTemplate == null)
        {
            Debug.LogError("Entry template not found!");
            return;
        }
        entryTemplate.gameObject.SetActive(false);

        float templateHeight = 20f;
        for (int i = 0; i < 4; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryTransform.gameObject.SetActive(true);
        }

        // For Testing
        int score = Random.Range(0, 1000);
        scoreText.text = score.ToString();

        string name = "AAA";
        nameText.text = name;
    }
}
