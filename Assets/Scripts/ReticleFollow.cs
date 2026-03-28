using UnityEngine;

public class ReticleFollow : MonoBehaviour
{
    void Update()
    {
        transform.position = Input.mousePosition;
    }
}
