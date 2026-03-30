using UnityEngine;

public class LedgeStatus : MonoBehaviour
{
    private bool isOccupied = false;

    public void Occupy(bool occupation)
    {
        isOccupied = occupation;
    }

    public bool GetOccupied()
    {
        return isOccupied;
    }
}
