using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoSceneLoader : MonoBehaviour
{
    // Change this if you want to load a specific scene
    public float delayTime = 3f; // seconds

    void Start()
    {
        // Call LoadNextScene after delayTime seconds
        Invoke("LoadNextScene", delayTime);
    }

    void LoadNextScene()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the next scene (make sure scenes are added in Build Settings)
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
