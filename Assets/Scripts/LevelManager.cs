using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class LevelManager : MonoBehaviour
{
    public TMP_Text BirdCounter;
    public int maxBirdCount = 3;
    private int birdsSpawned = 0;
    private int birdsSpotted = 0;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // void LoadSceneByName(string name)
    // {
    //     SceneManager.LoadScene(name);
    // }

    // void ReloadSameScene()
    // {
    //     Scene scene = SceneManager.GetActiveScene();
    //     SceneManager.LoadScene(scene.name);
    // }

    public void AddBird()
    {
        birdsSpawned++;
    }

    public void SpotBird()
    {
        birdsSpotted++;
        BirdCounter.text = birdsSpotted + "/" + maxBirdCount; 

        if(birdsSpotted >= maxBirdCount)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    
}
