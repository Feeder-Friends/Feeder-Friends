using System.IO;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public static bool readText = false;
    public GameObject hint;
    public GameObject flavorText;
    public GameObject okButton;
    public GameObject noteCounterUI;
    public static bool hasReadHint = false;
    public GameObject reticle;
    public MouseLook mouseLook;

    public void CloseFlavorText()
    {
       flavorText.SetActive(false);
       okButton.SetActive(false); 
       noteCounterUI.SetActive(true);
       reticle.SetActive(true);
       readText = true;
       Cursor.visible = false;
       Cursor.lockState = CursorLockMode.Locked;
       mouseLook.enabled = true;
    }
}
