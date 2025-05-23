using UnityEngine;
using TMPro;

public class DragDropRoom : MonoBehaviour
{
    [Header("References")]
    public TMP_Text questionText;
    public TMP_Text roundText;
    public TMP_Text scoreText;
    public GameObject dragItemPrefab;
    public GameObject dropZonePrefab;
    public Transform dragItemsParent;
    public Transform dropZonesParent;

    private int roomScore = 0;

    private void Start()
    {
        SetupRoom();
        UpdateUI();
    }

    private void SetupRoom()
    {
        // ������ҧ�Ӷ�� (�����ԧ�����Ŵ�ҡ�к��Ӷ��)
        questionText.text = "�Ѻ�����Ѿ��Ѻ��������";

        // ���ҧ Drag Items ��� Drop Zones
        CreatePair("���", "Cat");
        CreatePair("�عѢ", "Dog");
        CreatePair("��", "Bird");
    }

    private void CreatePair(string word, string meaning)
    {
        // ���ҧ Drag Item
        GameObject dragItem = Instantiate(dragItemPrefab, dragItemsParent);
        dragItem.GetComponentInChildren<TMP_Text>().text = word;
        dragItem.GetComponent<DraggableItem>().correctAnswer = meaning;

        // ���ҧ Drop Zone
        GameObject dropZone = Instantiate(dropZonePrefab, dropZonesParent);
        dropZone.GetComponentInChildren<TMP_Text>().text = meaning;
        dropZone.GetComponent<DropZone>().expectedAnswer = meaning;
    }

    public void OnCorrectMatch()
    {
        roomScore += 10;
        GameManager.Instance.AddScore(10);
        UpdateUI();

        // ��Ǩ�ͺ��Ҩ���ͧ�����ѧ
        if (roomScore >= 30) // ������ҧ: 3 ��� ����� 10 ��ṹ
        {
            Invoke("EndRoom", 1f);
        }
    }

    private void UpdateUI()
    {
        roundText.text = $"�ͺ {GameManager.Instance.currentRound}";
        scoreText.text = $"��ṹ��ͧ���: {roomScore}";
    }

    private void EndRoom()
    {
        GameManager.Instance.LoadNextRoom();
    }
}