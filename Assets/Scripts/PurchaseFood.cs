using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PurchaseFood : MonoBehaviour
{
    public GameObject noteCounterUI;
    public TMP_Text noteCount;
    private int noteAmount;
    public GameObject foodButton;
    public static bool shopIsOpen = false;
    private AudioSource audioSource;
    public AudioClip foodPurchaseSFX;
    public GameObject hintText;
    public GameObject okButton;
    public static bool hasReadHint = false;
    public GameObject reticle;
    public MouseLook mouseLook;
    void Start()
    {
       hasReadHint = false;

       foodButton.SetActive(false);
       noteCounterUI.SetActive(false);
       noteAmount = 30;
       UpdateUI();
    //    Debug.Log("PurchaseFood is running");
       audioSource = GetComponent<AudioSource>();
    }

    public void InstructionsRead()
    {
       hintText.SetActive(false);
       okButton.SetActive(false); 
       hasReadHint = true;
       noteCounterUI.SetActive(true);
       reticle.SetActive(true);
       Cursor.visible = false;
       Cursor.lockState = CursorLockMode.Locked;
       mouseLook.enabled = true;
    }

    private void UpdateUI()
    {
        noteCount.text = noteAmount.ToString();
    }

    public void SubtractNotes()
    {
        if(noteAmount > 0)
        {
            noteAmount -= 5;
            if(audioSource)
                audioSource.clip = foodPurchaseSFX;
                audioSource.Play();
                
            UpdateUI();
        }
        else
        {
            noteAmount = 0;
        }
    }

    public void OpenShop()
    {
        shopIsOpen = true;
        foodButton.SetActive(true);
        reticle.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void CloseShop()
    {
        shopIsOpen = false;
        foodButton.SetActive(false);
        reticle.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    void Update()
    {
        if(!PurchaseFood.hasReadHint && hintText != null) return;
        
        if(Input.GetKeyDown(KeyCode.O))
        {
            if(shopIsOpen)
            {
               CloseShop();
               Debug.Log("Closing shop!"); 
            }
            else
            {
                OpenShop();
                Debug.Log("Opening shop!");
            }  
        }
    }
}
