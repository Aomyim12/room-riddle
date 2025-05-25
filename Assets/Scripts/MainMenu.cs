using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        // ตั้งค่าปุ่มใน Inspector
        transform.Find("StartButton").GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.StartGame());
        transform.Find("ExitButton").GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.QuitGame());
    }
}