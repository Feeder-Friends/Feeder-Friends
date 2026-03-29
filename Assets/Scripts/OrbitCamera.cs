using Unity.VisualScripting;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Transform target;
    public float orbitSpeed = 100f;
    public float zoomSpeed = 5f;
    public float minZoom = 2f;
    public float maxZoom = 10f;
    public float minAngle = -180f;
    public float maxAngle = 180f;

    private float currentDistance;
    private float currentAngle = 90f;
    void Start()
    {
        currentDistance = Vector3.Distance(transform.position, target.position);
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float newAngle = currentAngle + horizontal * orbitSpeed * Time.deltaTime;

        if (newAngle >= minAngle && newAngle <= maxAngle)
        {
            currentAngle = newAngle;
            transform.RotateAround(target.position, Vector3.up, horizontal * orbitSpeed * Time.deltaTime);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentDistance -= scroll * zoomSpeed;
        currentDistance = Mathf.Clamp(currentDistance, minZoom, maxZoom);
        transform.position = (transform.position - target.position).normalized * currentDistance + target.position;

        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                BirdClick birdClick = hit.collider.GetComponent<BirdClick>();
                if(birdClick != null)
                {
                    birdClick.OnMouseDown();
                }
            }
        }
    }
}
