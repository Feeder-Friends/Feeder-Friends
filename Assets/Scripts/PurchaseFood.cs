using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PurchaseFood : MonoBehaviour
{
    public static int foodLevel = 0;
    public GameObject seed;
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
    void Start()
    {
       hasReadHint = false;

       foodButton.SetActive(false);
       noteAmount = 30;
       Debug.Log("On start: NoteAmount is " + noteAmount);
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
    }

    private void UpdateUI()
    {
        noteCount.text = noteAmount.ToString();
    }

    public void AddNotes()
    {
        Debug.Log("NoteAmount before adding: " + noteAmount);
        noteAmount += 5;   
        Debug.Log("NoteAmount after adding: " + noteAmount);
        UpdateUI();
    }

    public void SubtractNotes()
    {
        if(noteAmount > 0)
        {
            noteAmount -= 10;
            if(audioSource)
                audioSource.clip = foodPurchaseSFX;
                audioSource.Play();
                
            UpdateUI();
            foodLevel += 12;
            seed.SetActive(true);
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
        Debug.Log("food level: " + foodLevel);
        if(foodLevel == 0)
        {
            seed.SetActive(false);
        }
        Debug.Log("Current note amount: " + noteAmount);
        if(!hasReadHint && hintText != null) return;
        
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
