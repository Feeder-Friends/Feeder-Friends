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
    public GameObject reticle;
    public GameObject winScreen;
    public Animator zoomAnimator;
    public OrbitCamera orbitCamera;
    
    private int birdsSpawned = 0;
    private int birdsSpotted = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if(cameraAnimator != null)
        {
            if (playerController != null) playerController.enabled = false;
            if (mouseLook != null) mouseLook.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            StartCoroutine(PlayIntro());
        }
        
        if(zoomAnimator != null)
        {
            orbitCamera.enabled = false;
            winScreen.SetActive(false);
            if (mouseLook != null) mouseLook.enabled = false;
            StartCoroutine(PlayZoomIntro());
        }
    }

    void Update()
    {
        
    }

    IEnumerator PlayIntro()
    {
        playerMesh.enabled = false;
        reticle.SetActive(false);
        cameraAnimator.Play("WakingUp");
    
        yield return new WaitForSeconds(cameraAnimator.GetCurrentAnimatorStateInfo(0).length);
    
        cameraAnimator.enabled = false;
        reticle.SetActive(true);
        playerMesh.enabled = true;
        playerController.enabled = true;
        mouseLook.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    IEnumerator PlayZoomIntro()
    {
        zoomAnimator.Play("InitialZoom");
        reticle.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        yield return new WaitForSeconds(zoomAnimator.GetCurrentAnimatorStateInfo(0).length);
    
        reticle.SetActive(true);
        zoomAnimator.enabled = false;
        orbitCamera.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    IEnumerator WinSequence()
    {
        winScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("HouseScene");
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
            StartCoroutine(WinSequence());
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
