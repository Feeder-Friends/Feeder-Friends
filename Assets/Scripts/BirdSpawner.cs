using System.Collections;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject ledge;
    public static int maxBirdCount = 3;
    public LevelManager levelManager;
    public PurchaseFood purchaseFood;
    private GameObject spawnedBird;

    void Start()
    {
        foreach (var item in purchaseFood.foodItems)
        {
            if(item.validBirds == null || item.validBirds.Length == 0)
            {
                Debug.LogWarning($"FoodItem '{item.name}' has no valid birds assigned.");
                return;
            }
        }

        Debug.Log("coroutine yield " + Time.time);
        StartCoroutine(SpawnBirds(2));
    }

    public void SetLedge(GameObject newLedge)
    {
        ledge = newLedge;
    }
    void SpawnBird(PurchaseFood.FoodItem activeFoodItem)
    {
        Debug.Log("SpawnBird called");
        var randomPrefab = activeFoodItem.validBirds[Random.Range(0, activeFoodItem.validBirds.Length)];
        
        spawnedBird = Instantiate(randomPrefab, transform.position, transform.rotation);
        spawnedBird.GetComponent<SparrowBehavior>().ledge = ledge;
        ledge.GetComponent<LedgeStatus>().Occupy(true);
        levelManager.AddBird();
    }

    IEnumerator SpawnBirds(float spawnInterval)
    {
        while(true)
        {
            var birdCount = GameObject.FindGameObjectsWithTag("Bird").Length;

            PurchaseFood.FoodItem activeFoodItem = purchaseFood.foodItems[PurchaseFood.activeFoodIndex];
            
            if(birdCount < maxBirdCount && !ledge.GetComponent<LedgeStatus>().GetOccupied() && PurchaseFood.foodLevel > 0 && spawnedBird == null)
            {
                Debug.Log("bird spawned by " + gameObject.name);
                SpawnBird(activeFoodItem);
            }
            Debug.Log("waiting to spawn...");
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}