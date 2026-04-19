using TMPro;
using UnityEngine;

public class PlaytimeTimer : MonoBehaviour
{
    public TMP_Text playtimeText;
    private float timePassed;

    void Update()
    {
            timePassed += Time.unscaledDeltaTime;
            DisplayTime(timePassed);
    }
    void DisplayTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        playtimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
