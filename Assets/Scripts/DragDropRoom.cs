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
        // ตัวอย่างคำถาม (ในเกมจริงควรโหลดจากระบบคำถาม)
        questionText.text = "จับคู่คำศัพท์กับความหมาย";

        // สร้าง Drag Items และ Drop Zones
        CreatePair("แมว", "Cat");
        CreatePair("สุนัข", "Dog");
        CreatePair("นก", "Bird");
    }

    private void CreatePair(string word, string meaning)
    {
        // สร้าง Drag Item
        GameObject dragItem = Instantiate(dragItemPrefab, dragItemsParent);
        dragItem.GetComponentInChildren<TMP_Text>().text = word;
        dragItem.GetComponent<DraggableItem>().correctAnswer = meaning;

        // สร้าง Drop Zone
        GameObject dropZone = Instantiate(dropZonePrefab, dropZonesParent);
        dropZone.GetComponentInChildren<TMP_Text>().text = meaning;
        dropZone.GetComponent<DropZone>().expectedAnswer = meaning;
    }

    public void OnCorrectMatch()
    {
        roomScore += 10;
        GameManager.Instance.AddScore(10);
        UpdateUI();

        // ตรวจสอบว่าจบห้องหรือยัง
        if (roomScore >= 30) // ตัวอย่าง: 3 คู่ คู่ละ 10 คะแนน
        {
            Invoke("EndRoom", 1f);
        }
    }

    private void UpdateUI()
    {
        roundText.text = $"รอบ {GameManager.Instance.currentRound}";
        scoreText.text = $"คะแนนห้องนี้: {roomScore}";
    }

    private void EndRoom()
    {
        GameManager.Instance.LoadNextRoom();
    }
}