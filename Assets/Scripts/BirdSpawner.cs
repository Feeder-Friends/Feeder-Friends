using System.Collections;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject[] birdPrefabs;
    public int maxBirdCount = 3;
    public LevelManager levelManager;
    public GameObject ledge;

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
        var positionOffset = Random.insideUnitSphere * 5;
        
        GameObject spawnedBird = Instantiate(randomPrefab, transform.position + positionOffset, transform.rotation);
        spawnedBird.GetComponent<SparrowBehavior>().ledge = ledge;
        
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