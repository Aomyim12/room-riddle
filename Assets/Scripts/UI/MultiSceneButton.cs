using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    [SerializeField]
    public GameObject settingPanel;

    // Call this function from your button OnClick() event
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene("ChooseRoom");
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

    public void OpenSetting()
    {
        settingPanel.SetActive(true);
        Time.timeScale = 0f; // หยุดเกมชั่วคราว (ใช้ในเกม)
    }

    // เรียกใช้เมื่อกดปุ่ม Close
    public void CloseSetting()
    {
        settingPanel.SetActive(false);
        Time.timeScale = 1f; // กลับมาเล่นต่อ
    }
}
