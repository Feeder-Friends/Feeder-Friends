using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseFood : MonoBehaviour
{
    public TMP_Text noteCount;
    private int noteAmount;
    public GameObject foodButton;
    public static bool shopIsOpen = false;
    private AudioSource audioSource;
    public AudioClip foodPurchaseSFX;
    public GameObject hintText;
    public GameObject okButton;
    public static bool hasReadHint = false;
    void Start()
    {
       hasReadHint = false;

       foodButton.SetActive(false);
       hintText.SetActive(true);
       okButton.SetActive(true);

       Cursor.lockState = CursorLockMode.None;
       Cursor.visible = true;
       
       noteAmount = 30;
       UpdateUI();
       Debug.Log("PurchaseFood is running");
       audioSource = GetComponent<AudioSource>();
    }

    public void InstructionsRead()
    {
       hintText.SetActive(false);
       okButton.SetActive(false); 
       hasReadHint = true;
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void CloseShop()
    {
        shopIsOpen = false;
        foodButton.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    void Update()
    {
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
