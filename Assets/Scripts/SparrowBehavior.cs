using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SparrowBehavior : MonoBehaviour
{
    public enum SparrowState {Enter, Eat, Exit}
    public SparrowState currentState = SparrowState.Enter;
    private Animator animator;
    public GameObject ledge;
    public GameObject purchaseManager;
    public GameObject note;
    public int eatTime = 5;
    private WaitForSeconds eatWait;
    public float speed = 5;
    private Vector3 startPosition;
    private int animState = 0;
    private AudioSource chirp;
    public bool hasEaten = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!purchaseManager)
        {
            purchaseManager = GameObject.FindGameObjectWithTag("PurchaseManager");
        }
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
        transform.position = Vector3.MoveTowards(transform.position, ledge.transform.position, speed*Time.deltaTime);
    }

    IEnumerator Eat()
    {
        animState = 2;
        if(!hasEaten)
            PurchaseFood.foodLevel -= 2;
            hasEaten = true;
        yield return eatWait;
        currentState = SparrowState.Exit;
        yield break;
    }

    private void Exit()
    {
        transform.LookAt(startPosition);
        animState = 1;
        transform.position = Vector3.MoveTowards(transform.position, startPosition, speed*Time.deltaTime);
        Invoke(nameof(UnoccupyLedge), 4);
        Destroy(gameObject, 5);
        
    }

    public void UnoccupyLedge()
    {
        ledge.GetComponent<LedgeStatus>().Occupy(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ledge"))
        {
            chirp.Play();
            currentState = SparrowState.Eat;
            Invoke(nameof(SpawnNote), 1);
        }
    }

    void SpawnNote()
    {
        var notePos = transform.position;
        notePos.y += 0.5f;
        var noteRot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 180, transform.eulerAngles.z);
        Instantiate(note, notePos, Quaternion.Euler(noteRot));
        Debug.Log("Note spawned");
        var noteScript = purchaseManager.GetComponent<PurchaseFood>();
        // Debug.Log("NoteScript is" + noteScript.name);
        if(noteScript)
        {
            noteScript.AddNotes();
        }
    }
}