using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        // ��駤�һ���� Inspector
        transform.Find("StartButton").GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.StartGame());
        transform.Find("ExitButton").GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.QuitGame());
    }
}