using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float lifetime = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
