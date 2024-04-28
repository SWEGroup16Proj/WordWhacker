using System.Collections;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Typer typer = null;

    private float minimumSpawnTime = 2f;
    private float maximumSpawnTime = 4f;
    private float spawnTime;
    private float startTime; // Time when the game started
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject canvas;
    public bool canSpawn = true;

    public float testReduction;

    void Awake()
    {
        // Record the start time of the game
        startTime = Time.time;
        SetInitialSpawnTime();
    }

    void Start()
    {
        SpawnNow();
        StartCoroutine(Spawn());
    }

    private void SetInitialSpawnTime()
    {
        spawnTime = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }

    private void UpdateSpawnTime()
    {
        // Calculate how much time has passed since the start
        float timeSinceStart = Time.time - startTime;

        // At 60 seconds (1 minute), we want the spawn time to be 2/3 of the initial value
        // Calculate a reduction factor based on the elapsed time
        float reductionFactor = Mathf.Clamp(timeSinceStart / 180f, 0f, 1f) / 3f;
        float reducedMinimumSpawnTime = minimumSpawnTime * (1f - reductionFactor);

        float reducedMaximumSpawnTime = maximumSpawnTime * (1f - reductionFactor);
        testReduction = reductionFactor;
        // Now set the spawn time using these new bounds
        spawnTime = Random.Range(reducedMinimumSpawnTime, reducedMaximumSpawnTime);
    }

    private IEnumerator Spawn()
    {
        while (canSpawn)
        {
            UpdateSpawnTime(); // Recalculate the spawn time every cycle
            yield return new WaitForSeconds(spawnTime);

            Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(-350f, 350f), 1025f, 0);
            
            if(canSpawn)
            {
                GameObject enemyObject = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, canvas.transform);

                TMP_Text textComponent = enemyObject.transform.GetChild(0).GetComponent<TMP_Text>();
                if (textComponent != null)
                {
                    textComponent.text = typer.GetNewWord();
                }
                else
                {
                    Debug.LogError("TMP_Text component not found on the child object.");
                }
            }
        }
    }


    public void SpawnNow()
    {
        if (canSpawn)
        {
            // Vector3 spawnPosition = new Vector3(
            //     transform.position.x + Random.Range(-150f, 150f), // Random offset applied here
            //     955f, // Same fixed y-coordinate as before
            //     0);
            Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(-350f, 350f), 1025f, 0);

            // GameObject enemyObject = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity/*, canvas.transform*/);
            GameObject enemyObject = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, canvas.transform);

            TMP_Text textComponent = enemyObject.transform.GetChild(0).GetComponent<TMP_Text>();
            if (textComponent != null)
            {
                textComponent.text = typer.GetNewWord();
            }
            else
            {
                Debug.LogError("TMP_Text component not found on the child object.");
            }
        }
    }
}
