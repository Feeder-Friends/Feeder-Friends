using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SquirrelSpawner : MonoBehaviour
{
    public AudioClip spawnSFX;
    public AudioClip successSFX;
    public AudioClip defeatSFX;
    public AudioSource source;
    public FlashingIcon warningScript;
    public GameObject squirrel;
    public GameObject squirrelAnnoy;
    public float probability = 0.2f;
    public int frequency = 2;
    WaitForSeconds wait;
    bool squirrelDefeat;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wait = new WaitForSeconds(frequency);
        StartCoroutine(SpawnChance());
    }

    public bool GetSquirrelDefeat()
    {
        return squirrelDefeat;
    }

    public void DefeatSquirrel()
    {
        source.clip = defeatSFX;
        source.Play();
        squirrel.SetActive(false);
        warningScript.StopBlinking();
        squirrelDefeat = true;
        Debug.Log("Squirrel defeated");
    }

    IEnumerator SpawnChance()
    {
        while (true)
        {
            if (PurchaseFood.foodLevel > 0)
            {
                float chance = Random.value;
                if (chance <= probability)
                {
                    Debug.Log("Chance is " + chance + ", squirrel spawning");
                    yield return StartCoroutine(EnemySequence());
                    squirrelDefeat = false;
                } else
                {
                    Debug.Log("Squirrel did not spawn, chance is " + chance);
                }
            }
            yield return wait;
        }
    }

    IEnumerator EnemySequence()
    {
        source.clip = spawnSFX;
        source.Play();
        Debug.Log("Squirrel Enemy routine Started");
        squirrelAnnoy.SetActive(false);
        squirrel.SetActive(true);
        yield return StartCoroutine(warningScript.Blink(5));
        if (!squirrelDefeat)
        {
            source.clip = successSFX;
            source.Play();
            Debug.Log("Squirrel not defeated, ate all the food");
            squirrel.SetActive(false);
            PurchaseFood.foodLevel = 0;
        }
    }
}
