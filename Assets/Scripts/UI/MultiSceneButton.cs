using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Call this function from your button OnClick() event
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    // Quit the game
    public void QuitGame()
    {
        // Works in a built game
        Application.Quit();

        // Stops play mode in the Unity Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
