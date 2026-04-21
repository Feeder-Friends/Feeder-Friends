using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseFood : MonoBehaviour
{
    [System.Serializable]
    public class FoodItem
    {
        public string name;
        public Button foodButton;
        public int cost;
        public Material seedMaterial;
        public GameObject[] validBirds;
    }

    public GameObject seed;
    
    public static int activeFoodIndex = 0;
    public static int[] foodLevel = new int[3];
    public FoodItem[] foodItems = new FoodItem[3];
    public GameObject noteCounterUI;
    public GameObject catalog;
    public TMP_Text noteCount;
    private int noteAmount;
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

       foreach(var item in foodItems)
        {
            item.foodButton.gameObject.SetActive(false);

        }

       catalog.SetActive(false);
       noteAmount = 30;
       Debug.Log("On start: NoteAmount is " + noteAmount);
       UpdateUI();
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
        foreach(var item in foodItems)
        {
            item.foodButton.interactable = noteAmount >= item.cost;
        }
    }

    public void AddNotes()
    {
        Debug.Log("NoteAmount before adding: " + noteAmount);
        noteAmount += 5;   
        Debug.Log("NoteAmount after adding: " + noteAmount);
        UpdateUI();
    }

    public void SubtractNotes(int foodIndex)
    {
        if(foodIndex < 0 || foodIndex >= foodItems.Length) 
            return;

        FoodItem item = foodItems[foodIndex];
        
        if(noteAmount >= item.cost)
        {
            noteAmount -= item.cost;

            if(audioSource)
            {
                audioSource.clip = foodPurchaseSFX;
                audioSource.Play();
            }    

            UpdateUI();
            foodLevel[foodIndex] += 12;
            activeFoodIndex = foodIndex;
            seed.SetActive(true);
            seed.GetComponent<Renderer>().material = item.seedMaterial;
        }
        else 
        {
            noteAmount = 0;
        }
    }

    public void OpenShop()
    {
        shopIsOpen = true;
        catalog.SetActive(true);
        foreach(var item in foodItems)
            item.foodButton.gameObject.SetActive(true);
        reticle.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void CloseShop()
    {
        shopIsOpen = false;
        catalog.SetActive(false);
        foreach(var item in foodItems)
            item.foodButton.gameObject.SetActive(false);
        reticle.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    void Update()
    {
        bool anyFood = false;
        for(int i = 0; i < foodItems.Length; i++)
        {
            if (foodLevel[i] > 0)
            {
                anyFood = true;
                break;
            }
        }
        seed.SetActive(anyFood);

        if(!hasReadHint && hintText != null) 
            return;
        
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
