using UnityEngine;

public class ProximityUI : MonoBehaviour
{
    public GameObject uiElement;
    public Transform player;
    public float distance = 5f;

    void Start()
    {
        uiElement.SetActive(false);
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);
        Debug.Log(dist);

        if (dist <= distance)
        {
            uiElement.SetActive(true);
        }
        else
        {
            uiElement.SetActive(false);
        }
    }
}
