using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SquirrelBehavior : MonoBehaviour
{
    public SquirrelSpawner spawner;
    public GameObject annoyedIcon;
    int mouseClicks = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator beAnnoyed()
    {
        annoyedIcon.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        annoyedIcon.SetActive(false);
    }

    void OnMouseDown()
    {
        mouseClicks++;
        StartCoroutine(beAnnoyed());
        if(mouseClicks >= 3)
        {
            mouseClicks = 0;
            spawner.DefeatSquirrel();
        }
    }
}
