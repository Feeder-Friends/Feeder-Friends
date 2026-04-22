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

    public BirdFeederSwitch feederSwitch;
    public LedgeSetter setter;
    public TMP_Text feederText;
    public Button fancyFeederButton;
    public Button oldFeederButton;

    public GameObject currentSeed;
    public GameObject[] feederSeeds;
    public static int activeFoodIndex = 0;
    public static int foodLevel = 0;
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
    bool boughtFeeder = false;
    bool isFancy = false;

    void Start()
    {
        hasReadHint = false;

        foreach (var item in foodItems)
        {
            item.foodButton.gameObject.SetActive(false);

        }

        catalog.SetActive(false);
        noteAmount = 30;
        Debug.Log("On start: NoteAmount is " + noteAmount);
        UpdateUI();
        audioSource = GetComponent<AudioSource>();
        currentSeed = feederSeeds[0];
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
        foreach (var item in foodItems)
        {
            item.foodButton.interactable = noteAmount >= item.cost;
        }
        if (!boughtFeeder)
        {
            fancyFeederButton.interactable = noteAmount >= 50;
        } else
        {
            fancyFeederButton.interactable = true;
        }
    }

    public void AddNotes(int notes)
    {
        Debug.Log("NoteAmount before adding: " + noteAmount);
        noteAmount += notes;
        Debug.Log("NoteAmount after adding: " + noteAmount);
        UpdateUI();
    }

    public void SubtractNotesBasic(int notes)
    {
        noteAmount -= notes;

        if (audioSource)
        {
            audioSource.clip = foodPurchaseSFX;
            audioSource.Play();
        }

        UpdateUI();
    }

    public void SubtractNotes(int foodIndex)
    {
        if (foodIndex < 0 || foodIndex >= foodItems.Length)
            return;

        FoodItem item = foodItems[foodIndex];

        if (noteAmount >= item.cost)
        {
            SubtractNotesBasic(item.cost);
            if (isFancy)
            {
                foodLevel += 24;
            }
            else
            {
                foodLevel += 12;
            }
            activeFoodIndex = foodIndex;
            currentSeed.SetActive(true);
            currentSeed.GetComponent<Renderer>().material = item.seedMaterial;
        }
        else
        {
            noteAmount = 0;
        }
    }

    public void EquipFancyFeeder()
    {
        if (!boughtFeeder)
        {
            if (noteAmount < 50)
            {
                return;
            }
            SubtractNotesBasic(50);
        }
        feederSwitch.Switch(1);
        ChangeSeed(1);
        setter.SetLedgesFancy();
        boughtFeeder = true;
        isFancy = true;
        feederText.text = "Equip Tube Feeder";
        oldFeederButton.interactable = true;
    }

    public void EquipOldFeeder()
    {
        if (isFancy)
        {
            if (audioSource)
            {
                audioSource.clip = foodPurchaseSFX;
                audioSource.Play();
            }
            feederSwitch.Switch(0);
            ChangeSeed(0);
            setter.SetLedgesRegular();
            isFancy = false;
        }
        oldFeederButton.interactable = false;
        UpdateUI();
    }



    public void OpenShop()
    {
        shopIsOpen = true;
        catalog.SetActive(true);
        foreach (var item in foodItems)
            item.foodButton.gameObject.SetActive(true);
        reticle.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseShop()
    {
        shopIsOpen = false;
        catalog.SetActive(false);
        foreach (var item in foodItems)
            item.foodButton.gameObject.SetActive(false);
        reticle.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    public void ChangeSeed(int index)
    {
        currentSeed = feederSeeds[index];
    }

    void Update()
    {
        currentSeed.SetActive(foodLevel > 0);

        if (!hasReadHint && hintText != null)
            return;

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (shopIsOpen)
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
