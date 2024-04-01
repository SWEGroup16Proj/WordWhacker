using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    private float minimumSpawnTime = 1f;
    private float maximumSpawnTime = 5f;
    private float spawnTime = 1f;
    private float difficulty = 1f;
    // private float spawnRate = 1f;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject canvas;
    private bool canSpawn = true;

    void Awake()
    {
        SetSpawnTime();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemyPrefab, new Vector3(Random.Range(-8f, 8f), 5.5f, 0), Quaternion.identity, canvas.transform);
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    // private void Update()
    // {
    //     spawnTime -= Time.deltaTime * spawnRate;
    // }

    private void SetSpawnTime()
    {
        spawnTime = Random.Range(minimumSpawnTime, maximumSpawnTime/difficulty);

        if(difficulty < 4.5f)
        {
            difficulty *= 1.1f;
        }
        else if(minimumSpawnTime > 0.01f)
        {
            minimumSpawnTime -= 0.01f;
        }

        Debug.Log(minimumSpawnTime + " < " + spawnTime + " < " + maximumSpawnTime/difficulty);
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnTime);

        while(canSpawn)
        {
            yield return wait;
            Instantiate(enemyPrefab, new Vector3(Random.Range(-8f, 8f), 5.5f, 0), Quaternion.identity, canvas.transform);
            SetSpawnTime();
        }
    }
}