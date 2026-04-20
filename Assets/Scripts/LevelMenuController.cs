using Unity.VisualScripting;
using UnityEngine;

public class LevelMenuController : MonoBehaviour
{
    public bool menuActive;
    public bool isGamePaused;
    public GameObject levelMenu;
    public GameObject reticle;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("m"))
        {
            // if(menuActive)
            // {
            //     CloseMenu();
            // }
            // else
            // {
                OpenMenu();
            //}
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    void OpenMenu()
    {
        menuActive = true;
        levelMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        reticle.SetActive(false);
        PauseGame();
    }

    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        levelMenu.SetActive(false);
        reticle.SetActive(true);
    }

    // void CloseMenu()
    // {
    //     menuActive = false;
    //     levelMenu.SetActive(false);
    //     Cursor.lockState = CursorLockMode.None;
    //     Cursor.visible = false;
    //     ResumeGame();
    // }
}
