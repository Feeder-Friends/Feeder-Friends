using UnityEngine;

public class LightswitchBehavior : MonoBehaviour
{
    public Light lightPoint;
    public Transform player;
    public float interactRange = 3f;

    void OnMouseDown()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if(distance <= interactRange)
        {
            lightPoint.enabled = !lightPoint.enabled;
        }
    }
}
