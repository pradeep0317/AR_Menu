using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    // This will now call SceneFlowManager so history is tracked
    public void LoadScene(string sceneName)
    {
        if (SceneFlowManager.Instance != null)
        {
            SceneFlowManager.Instance.NavigateTo(sceneName);
        }
        else
        {
            Debug.LogWarning("SceneFlowManager not found in scene. Loading directly...");
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }

    // Optional: you can hook this to a back button in UI
    public void LoadPreviousScene()
    {
        if (SceneFlowManager.Instance != null)
        {
            SceneFlowManager.Instance.NavigateBack();
        }
    }
}