using UnityEngine;

public class AutoCanvasSwitcher : MonoBehaviour
{
    [Header("Assign all canvases here")]
    public GameObject[] canvases;

    [Header("Time between switches (seconds)")]
    public float switchInterval = 3f;

    private int currentIndex = -1;
    private float timer = 0f;
    private bool finished = false;

    void Start()
    {
        if (canvases.Length > 0)
        {
            ShowCanvas(0); // Start with the first canvas
        }
    }

    void Update()
    {
        if (finished || canvases.Length == 0) return;

        timer += Time.deltaTime;

        if (timer >= switchInterval)
        {
            ShowNextCanvas();
            timer = 0f;
        }
    }

    void ShowCanvas(int index)
    {
        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].SetActive(i == index); // only show the current one
        }
        currentIndex = index;
    }

    void ShowNextCanvas()
    {
        int nextIndex = currentIndex + 1;

        if (nextIndex < canvases.Length)
        {
            ShowCanvas(nextIndex);
        }
        else
        {
            finished = true; // stop switching after last canvas
        }
    }
}