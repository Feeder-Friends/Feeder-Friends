using NUnit.Framework;
using UnityEngine;

public class BirdClick : MonoBehaviour
{
    private LevelManager levelManager;
    public AudioSource cameraSFX;
    private bool hasBeenSpotted = false;
    void Start()
    {
        if(!cameraSFX)
        {
            cameraSFX = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        }
       levelManager = FindAnyObjectByType<LevelManager>();
    }

    void OnMouseOver()
    {
        Debug.Log($"This is a {gameObject.name}");
    }

    public void OnMouseDown()
    {
        if(hasBeenSpotted) return;
        
        hasBeenSpotted = true;
        levelManager.SpotBird();
        cameraSFX.Play();
        enabled = false;
    }
}
