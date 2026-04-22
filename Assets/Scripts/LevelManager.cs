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
    public GameObject flavorText;
    public GameObject winScreen;
    public Animator zoomAnimator;
    public OrbitCamera orbitCamera;
    public GameObject UI;
    
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

    IEnumerator PlayIntro()
    {
        playerMesh.enabled = false;
        reticle.SetActive(false);
        flavorText.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cameraAnimator.Play("WakingUp");
        
        yield return new WaitForSeconds(cameraAnimator.GetCurrentAnimatorStateInfo(0).length);
    
        Debug.Log("Hint should be showing now");
        cameraAnimator.enabled = false;
        playerMesh.enabled = true;
        playerController.enabled = true;
        flavorText.SetActive(true);
        reticle.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
        UI.SetActive(true);
        reticle.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator WinSequence()
    {
        winScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    
        yield return new WaitForSecondsRealtime(3f);
    
        Time.timeScale = 1f;
    
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
    
        if (nextIndex >= SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene("MenuScene");
        }
        else
        {
            SceneManager.LoadScene(nextIndex);
        }
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
