using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashingIcon : MonoBehaviour
{
    bool blink = true;
    public Image imageToToggle;
    public float interval = 0.5f;
    private WaitForSeconds wait;
    void Start()
    {
        wait = new WaitForSeconds(interval);
    }

    public IEnumerator Blink(int numBlinks)
    {
        Debug.Log("Blinking started");
        int blinks = 0;
        while(blinks < numBlinks && blink)
        {
            imageToToggle.enabled = true;
            yield return wait;
            imageToToggle.enabled = false;
            yield return wait;
            blinks++;
            Debug.Log("Blink times: " + blinks);
        }
        Debug.Log("Blinking stopped");
        blink = true;
    }

    public void StopBlinking()
    {
        blink = false;
    }
}
