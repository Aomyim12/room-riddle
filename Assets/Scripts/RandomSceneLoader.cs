using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class RandomSceneLoader : MonoBehaviour
{
    [Header("Scene Settings")]
    public string[] sceneNames = { "1stRoom", "2ndRoom", "3ndRoom" };
    public float delayBeforeLoad = 0.5f;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        // �Դ��ä�ԡ���Ǥ���
        button.interactable = false;     

        // �������͡ Scene
        string sceneToLoad = sceneNames[Random.Range(0, sceneNames.Length)];
        Debug.Log("���ѧ��Ŵ: " + sceneToLoad);

        // ��Ŵ Scene ��ѧ������
        Invoke(nameof(LoadRandomScene), delayBeforeLoad);
    }

    private void LoadRandomScene()
    {
        string sceneToLoad = sceneNames[Random.Range(0, sceneNames.Length)];
        SceneManager.LoadScene(sceneToLoad);
    }
}