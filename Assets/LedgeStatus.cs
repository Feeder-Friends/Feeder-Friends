using UnityEngine;

public class LedgeStatus : MonoBehaviour
{
    private bool isOccupied = false;

    public void Occupy()
    {
        isOccupied = true;
    }

    public bool GetOccupied()
    {
        return isOccupied;
    }
}
