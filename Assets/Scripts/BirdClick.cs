using NUnit.Framework;
using UnityEngine;

public class BirdClick : MonoBehaviour
{
    private LevelManager levelManager;
    private bool hasBeenSpotted = false;
    void Start()
    {
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
        enabled = false;
    }
}
