using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // ✅ New Input System

public class SceneFlowManager : MonoBehaviour
{
    public static SceneFlowManager Instance;

    private Stack<string> sceneHistory = new Stack<string>();

    private Vector2 touchStartPos;
    private bool isSwiping = false;
    private float minSwipeDistance = 100f; // adjust as needed

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (sceneHistory.Count == 0 || sceneHistory.Peek() != scene.name)
        {
            sceneHistory.Push(scene.name);
        }
    }

    void Update()
    {
        HandleSwipe();
        HandleKeyboardBack();
    }

    // ✅ Handle swipe using new Input System
    private void HandleSwipe()
    {
        if (Touchscreen.current == null) return;

        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            if (!isSwiping)
            {
                touchStartPos = Touchscreen.current.primaryTouch.position.ReadValue();
                isSwiping = true;
            }
        }
        else if (isSwiping) // finger lifted
        {
            Vector2 touchEndPos = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector2 swipeDelta = touchEndPos - touchStartPos;

            if (Mathf.Abs(swipeDelta.x) > minSwipeDistance && Mathf.Abs(swipeDelta.y) < 100f)
            {
                if (swipeDelta.x > 0)
                {
                    NavigateBack(); // Swipe right = go back
                }
            }

            isSwiping = false;
        }
    }

    // ✅ Handle Escape key using new Input System
    private void HandleKeyboardBack()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            NavigateBack();
        }
    }

    public void NavigateTo(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void NavigateBack()
    {
        if (sceneHistory.Count > 1)
        {
            sceneHistory.Pop(); // remove current
            string prevScene = sceneHistory.Pop(); // get previous
            SceneManager.LoadScene(prevScene);
        }
    }
}
