using UnityEngine;

public class LedgeSetter : MonoBehaviour
{
    public BirdSpawner left;
    public BirdSpawner right;
    public BirdSpawner back;
    public GameObject[] birdFeederLedges = new GameObject[3];
    public GameObject[] fancyFeederLedges = new GameObject[3];
    
    public void SetLedges(GameObject lTrigger, GameObject rTrigger, GameObject bTrigger)
    {
        left.SetLedge(lTrigger);
        right.SetLedge(rTrigger);
        back.SetLedge(bTrigger);
    }

    public void SetLedgesRegular()
    {
        SetLedges(birdFeederLedges[0], birdFeederLedges[1], birdFeederLedges[2]);
    }

    public void SetLedgesFancy()
    {
        SetLedges(fancyFeederLedges[0], fancyFeederLedges[1], fancyFeederLedges[2]);
    }
}
