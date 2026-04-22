using TMPro;
using UnityEngine;

public class InteractableProximityBehavior : MonoBehaviour
{
    
    public Transform player;
    public MonoBehaviour outlineScript;
    public GameObject loreText;
    public float interactRange = 3f;
    public float distance = 5f;
    
    void Start()
    {
        outlineScript.enabled = false;
        loreText.SetActive(false);
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);
        Debug.Log(dist);

        if (dist <= distance)
        {
            outlineScript.enabled = true;
        }
        else
        {
            outlineScript.enabled = false;
        }

        if (dist > interactRange && loreText.activeSelf)
        {
            loreText.SetActive(false);
        }
    }

    void OnMouseDown()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if(distance <= interactRange)
        {
            loreText.SetActive(!loreText.activeSelf);
        }
    }

}
