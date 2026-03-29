using UnityEngine;

public class SceneChangeClick : MonoBehaviour
{
    public string sceneName;
    void OnMouseDown()
    {
        Debug.Log("clicked!");
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.LoadLevel(sceneName);
        }
    }
}
