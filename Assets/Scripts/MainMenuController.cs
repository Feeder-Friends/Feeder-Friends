using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene("Interlude1");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }


}
