using UnityEngine;

public class CanvasSwitcher : MonoBehaviour
{
    [Header("Assign all canvases here")]
    public GameObject[] canvases;

    private int currentIndex = -1;

    void Start()
    {
        if (canvases.Length > 0)
        {
            SwitchCanvas(); // Start with the first canvas
        }
    }

    // Call this function from your button
    public void SwitchCanvas()
    {
        int nextIndex = (currentIndex + 1) % canvases.Length; // loop back to 0

        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].SetActive(i == nextIndex); // show only the next one
        }

        currentIndex = nextIndex;
    }
}
