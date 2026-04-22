using UnityEngine;

public class InteractableProximityBehavior : MonoBehaviour
{
    
    public Transform player;
    public MonoBehaviour outlineScript;
    public float distance = 5f;
    
    void Start()
    {
        outlineScript.enabled = false;
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
    }
}
