using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(AudioSource))]
public class SparrowBehavior : MonoBehaviour
{
    public enum SparrowState {Enter, Eat, Exit}
    public SparrowState currentState = SparrowState.Enter;
    private Animator animator;
    public GameObject ledge;
    public int eatTime = 5;
    private WaitForSeconds eatWait;
    public float speed = 5;
    private Vector3 startPosition;
    private bool flyAway = false;
    private int animState = 0;
    private AudioSource chirp;
    private bool turned = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Original rotation " + transform.eulerAngles);
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        chirp = GetComponent<AudioSource>();
        eatWait = new WaitForSeconds(eatTime);
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case SparrowState.Enter:
            {
                Enter();
                break;
            }
            case SparrowState.Eat:
            {
                StartCoroutine(Eat());
                break;
            }
            case SparrowState.Exit:
            {
                Exit();
                break;
            }
        }
        animator.SetInteger("animState", animState);
    }

    private void Enter()
    {
        animState = 1;
        transform.LookAt(ledge.transform);
        transform.position = Vector3.MoveTowards(transform.position, ledge.transform.position, speed*Time.deltaTime);
    }

    IEnumerator Eat()
    {
        animState = 2;
        yield return eatWait;
        currentState = SparrowState.Exit;
        yield break;
    }

    private void Exit()
    {
        Debug.Log("ExitTime: Flyaway is" + flyAway);
        if(!flyAway)
        {
            animState = 3;
        }
        else
        { 
            animState = 1;
            if(!turned)
            {
                transform.Rotate(0, 180, 0, Space.World);
                turned = true;
            }
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed*Time.deltaTime);
        }
        
    }

    public void SetFlyAway()
    {
        flyAway = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ledge"))
        {
            chirp.Play();
            currentState = SparrowState.Eat;
        }
    }
}