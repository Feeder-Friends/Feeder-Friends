using UnityEngine;

public class BirdFeederSwitch : MonoBehaviour
{
    public GameObject feeder;
    public GameObject[] feederTypes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Switch(int index)
    {
        feeder.SetActive(false);
        feeder = feederTypes[index];
        feeder.SetActive(true);
    }
}
