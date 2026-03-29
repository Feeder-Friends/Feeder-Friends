using System.Collections;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject[] birdPrefabs;
    public int maxBirdCount = 4;
    public LevelManager levelManager;

    void Start()
    {
        foreach (var b in birdPrefabs)
        {
            if(b == null)
            {
                Debug.LogWarning("One or more Bird prefabs are not assigned.");
                return;
            }
        }

        Debug.Log("coroutine yield " + Time.time);
        StartCoroutine(SpawnBirds(2));
    }
    void SpawnBird()
    {
        var randomPrefab = birdPrefabs[Random.Range(0, birdPrefabs.Length)];
        // var positionOffset = Random.insideUnitSphere * 5;
        
        GameObject spawnedBird = Instantiate(randomPrefab, transform.position, Quaternion.Euler(0, 180, 0));
        levelManager.AddBird();
    }

    IEnumerator SpawnBirds(float spawnInterval)
    {
        Debug.Log("before yield " + Time.time);
        while(true)
        {
            var birdCount = GameObject.FindGameObjectsWithTag("Bird").Length;
            
            if(birdCount < maxBirdCount)
            SpawnBird();
            yield return new WaitForSeconds(spawnInterval);
            Debug.Log("after yield " + Time.time);
        }
    }
}