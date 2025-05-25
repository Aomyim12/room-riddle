using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Settings")]
    public int totalRounds = 5;
    public int currentRound = 0;
    public int totalScore = 0;

    [Header("UI References")]
    public TMP_Text roundText;
    public TMP_Text scoreText;
    public GameObject mainMenuPanel;
    public GameObject gameOverPanel;
    public TMP_Text finalScoreText;

    private List<RoomType> roomOrder = new List<RoomType>();

    public enum RoomType { DragDrop, QuickQuiz, HintQuiz }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        currentRound = 0;
        totalScore = 0;
        GenerateRoomOrder();
        mainMenuPanel.SetActive(false);
        LoadNextRoom();
    }

    private void GenerateRoomOrder()
    {
        roomOrder.Clear();
        List<RoomType> allRooms = new List<RoomType> { RoomType.DragDrop, RoomType.QuickQuiz, RoomType.HintQuiz };

        // สุ่มห้องไม่ซ้ำจนครบ 3 ห้องก่อน แล้วจึงเริ่มสุ่มซ้ำ
        for (int i = 0; i < totalRounds; i++)
        {
            if (allRooms.Count == 0)
            {
                allRooms = new List<RoomType> { RoomType.DragDrop, RoomType.QuickQuiz, RoomType.HintQuiz };
            }

            int randomIndex = Random.Range(0, allRooms.Count);
            roomOrder.Add(allRooms[randomIndex]);
            allRooms.RemoveAt(randomIndex);
        }
    }

    public void LoadNextRoom()
    {
        currentRound++;
        UpdateUI();

        if (currentRound > totalRounds)
        {
            EndGame();
            return;
        }

        RoomType nextRoom = roomOrder[currentRound - 1];
        string sceneName = nextRoom.ToString() + "Room";
        SceneManager.LoadScene(sceneName);
    }

    public void AddScore(int score)
    {
        totalScore += score;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (roundText != null) roundText.text = $"รอบที่ {currentRound}/{totalRounds}";
        if (scoreText != null) scoreText.text = $"คะแนน: {totalScore}";
    }

    private void EndGame()
    {
        gameOverPanel.SetActive(true);
        finalScoreText.text = $"คะแนนรวม: {totalScore}";
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        mainMenuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}