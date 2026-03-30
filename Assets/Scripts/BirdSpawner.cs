using System.Collections;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject ledge;
    public GameObject[] birdPrefabs;
    public static int maxBirdCount = 3;
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
        Debug.Log("SpawnBird called");
        var randomPrefab = birdPrefabs[Random.Range(0, birdPrefabs.Length)];
        // var positionOffset = Random.insideUnitSphere * 5;
        
        GameObject spawnedBird = Instantiate(randomPrefab, transform.position, transform.rotation);
        spawnedBird.GetComponent<SparrowBehavior>().ledge = ledge;
        ledge.GetComponent<LedgeStatus>().Occupy();
        levelManager.AddBird();
    }

    IEnumerator SpawnBirds(float spawnInterval)
    {
        //Debug.Log("before yield " + Time.time);
        while(true)
        {
            var birdCount = GameObject.FindGameObjectsWithTag("Bird").Length;
            
            if(birdCount < maxBirdCount && !ledge.GetComponent<LedgeStatus>().GetOccupied() && PurchaseFood.foodLevel > 0)
                Debug.Log("bird spawned by " + gameObject.name);
                SpawnBird();
            Debug.Log("waiting to spawn...");
            yield return new WaitForSeconds(spawnInterval);
            //Debug.Log("after yield " + Time.time);
        }
    }
}