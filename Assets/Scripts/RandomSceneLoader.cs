using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class RandomSceneLoader : MonoBehaviour
{
    [Header("Scene Settings")]
    public string[] sceneNames = { "pairRoom", "2ndRoom", "SortWord" };
    public float delayBeforeLoad = 0.5f;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        // ปิดการคลิกชั่วคราว
        button.interactable = false;     

        // สุ่มเลือก Scene
        string sceneToLoad = sceneNames[Random.Range(0, sceneNames.Length)];
        Debug.Log("กำลังโหลด: " + sceneToLoad);

        // โหลด Scene หลังดีเลย์
        Invoke(nameof(LoadRandomScene), delayBeforeLoad);
    }

    private void LoadRandomScene()
    {
        string sceneToLoad = sceneNames[Random.Range(0, sceneNames.Length)];
        SceneManager.LoadScene(sceneToLoad);
    }
}