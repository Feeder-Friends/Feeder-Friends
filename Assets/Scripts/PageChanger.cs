using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PageChanger : MonoBehaviour
{
    public TMP_Text text;
    public GameObject[] pages = new GameObject[2];
    public GameObject currentPage;
    int currentIndex = 0;
    public Button backButton;
    public Button forwardButton;

    void Update()
    {
        if(currentIndex == 0)
        {
            backButton.interactable = false;
        } else
        {
            backButton.interactable = true;
        }

        if (currentIndex == pages.Length - 1)
        {
            forwardButton.interactable = false;
        } else
        {
            forwardButton.interactable = true;
        }
        
    }

    public void ChangePage(int index)
    {
        currentPage.SetActive(false);
        currentPage = pages[index];
        currentPage.SetActive(true);
        currentIndex = index;
        int page = index + 1;
        text.text = "Page " + page + "/2";
    }
}
