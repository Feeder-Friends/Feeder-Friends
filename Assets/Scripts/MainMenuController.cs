using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene("HouseScene");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }


}
