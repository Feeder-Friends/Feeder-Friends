using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(AudioSource))]
public class SparrowBehavior : MonoBehaviour
{
    private Animator animator;
    public GameObject ledge;
    public float speed = 5;
    private Vector3 startPosition;
    private Quaternion turned; 
    private bool enter = true;
    private bool exit = false;
    private AudioSource chirp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        chirp = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enter)
        {
            transform.LookAt(ledge.transform);
            transform.position = Vector3.MoveTowards(transform.position, ledge.transform.position, speed * Time.deltaTime);
        }
        else if(exit)
        { 
            transform.rotation = Quaternion.Lerp(transform.rotation, turned, 1.5f*Time.deltaTime);
            if(Quaternion.Angle(transform.rotation, turned) <= 2f)
            {
                animator.speed = 1;
                animator.SetBool("flyAway", true); 
                transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
                if(Vector3.Distance(transform.position, startPosition) <= 0.01f)
                {
                    animator.SetBool("isStopped", true);
                }
                
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ledge"))
        {
            chirp.Play();
            enter = false;
            animator.SetBool("isEating", true);
            Invoke("TurnExit", 5);
        }
    }

    void TurnExit()
    {
        animator.SetBool("isEating", false);
        animator.speed = 0.25f;
        animator.SetBool("isTurning", true);
    }

    void ExitStart()
    {
        turned = transform.rotation * Quaternion.Euler(0, 180f, 0);
        exit = true;
    }

}