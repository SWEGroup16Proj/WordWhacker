using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public Typer typer = null;

    private float minimumSpawnTime = 1f;
    private float maximumSpawnTime = 3f;
    private float spawnTime = 1f;
    private float difficulty = 1f;
    // private float spawnRate = 1f;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject canvas;
    public bool canSpawn = true;

    void Awake()
    {
        SetSpawnTime();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnNow();
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    // private void Update()
    // {
    //     spawnTime -= Time.deltaTime * spawnRate;
    // }

    // determine spawn rate of enemies
    private void SetSpawnTime()
    {
        spawnTime = Random.Range(minimumSpawnTime, maximumSpawnTime/difficulty);

        // if(difficulty < 4.5f)
        // {
        //     difficulty *= 1.1f;
        // }
        // else if(minimumSpawnTime > 0.01f)
        // {
        //     minimumSpawnTime -= 0.01f;
        // }

        // Debug.Log(minimumSpawnTime + " < " + spawnTime + " < " + maximumSpawnTime/difficulty);
    }

    // instantiate enemies
    private IEnumerator Spawn()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnTime);

        while(canSpawn)
        {
            yield return wait;
            GameObject enemyObject = Instantiate(enemyPrefab, new Vector3(Random.Range(-7f, 7f), 5.5f, 0), Quaternion.identity, canvas.transform);

            // change the enemy's word text
            TMP_Text textComponent = enemyObject.transform.GetChild(0).GetComponent<TMP_Text>();
            
            // Change the text of the TMP_Text component
            if (textComponent != null)
            {
                textComponent.text = typer.GetNewWord(); // Set the new text
            }
            else
            {
                Debug.LogError("TMP_Text component not found on the child object.");
            }
            
            SetSpawnTime();
        }
    }

    // instantiate enemies immediately
    public void SpawnNow()
    {
        if(canSpawn)
        {
            GameObject enemyObject = Instantiate(enemyPrefab, new Vector3(Random.Range(-7f, 7f), 5.5f, 0), Quaternion.identity, canvas.transform);

            // change the enemy's word text
            TMP_Text textComponent = enemyObject.transform.GetChild(0).GetComponent<TMP_Text>();
            
            // Change the text of the TMP_Text component
            if (textComponent != null)
            {
                textComponent.text = typer.GetNewWord(); // Set the new text
            }
            else
            {
                Debug.LogError("TMP_Text component not found on the child object.");
            }
        }
    }
}