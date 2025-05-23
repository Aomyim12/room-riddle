using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HintQuizRoom : MonoBehaviour
{
    [Header("References")]
    public TMP_Text[] hintTexts;
    public TMP_InputField answerInput;
    public TMP_Text resultText;
    public TMP_Text roundText;
    public Button submitButton;
    public Button nextHintButton;

    private int currentHint = 0;
    private int roomScore = 0;
    private string correctAnswer = "����";

    private void Start()
    {
        roundText.text = $"�ͺ {GameManager.Instance.currentRound}";
        ShowHint(0);
        submitButton.onClick.AddListener(CheckAnswer);
        nextHintButton.onClick.AddListener(ShowNextHint);
    }

    private void ShowHint(int index)
    {
        currentHint = index;
        string[] hints = {
            "�ء����ժ��Ե�������й��",
            "��㹡�ô�ç���Ե",
            "��鹡Ѻ����"
        };

        for (int i = 0; i < hintTexts.Length; i++)
        {
            hintTexts[i].text = i <= index ? $"�� {i + 1}: {hints[i]}" : "";
        }

        nextHintButton.interactable = index < 2;
    }

    private void ShowNextHint()
    {
        ShowHint(currentHint + 1);
    }

    private void CheckAnswer()
    {
        if (answerInput.text.Trim().ToLower() == correctAnswer.ToLower())
        {
            roomScore = (3 - currentHint) * 20;
            GameManager.Instance.AddScore(roomScore);
            resultText.text = $"�١��ͧ! +{roomScore} ��ṹ";
            resultText.color = Color.green;
        }
        else
        {
            resultText.text = "���١��ͧ! �ͧ�ա����";
            resultText.color = Color.red;
            return;
        }

        Invoke("EndRoom", 1.5f);
    }

    private void EndRoom()
    {
        GameManager.Instance.LoadNextRoom();
    }
}