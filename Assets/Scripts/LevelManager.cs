using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public TMP_Text BirdCounter;
    public int maxBirdCount = 3;
    public MeshRenderer playerMesh;
    public PlayerController playerController;
    public MouseLook mouseLook;
    public Animator cameraAnimator;
    
    private int birdsSpawned = 0;
    private int birdsSpotted = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        playerController.enabled = false;
        mouseLook.enabled = false;
        Cursor.lockState = CursorLockMode.None;

        StartCoroutine(PlayIntro());
    }

    void Update()
    {
        
    }

    IEnumerator PlayIntro()
    {
        playerMesh.enabled = false;
        cameraAnimator.Play("WakingUp");
    
        yield return new WaitForSeconds(cameraAnimator.GetCurrentAnimatorStateInfo(0).length);
    
        cameraAnimator.enabled = false;
        playerMesh.enabled = true;
        playerController.enabled = true;
        mouseLook.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void AddBird()
    {
        birdsSpawned++;
    }

    public void SpotBird()
    {
        birdsSpotted++;
        BirdCounter.text = birdsSpotted + "/" + maxBirdCount; 

        if(birdsSpotted >= maxBirdCount)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void LoadLevel(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is empty!");
        }
    }
}
