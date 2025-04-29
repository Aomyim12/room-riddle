using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class QuestionManager : MonoBehaviour
{
    public List<QuestionData> allQuestions = new List<QuestionData>();

    public GameObject questionItemPrefab;
    public Transform listContent;

    public QuestionEditor editor;

    public void AddQuestion(QuestionData q)
    {
        allQuestions.Add(q);
        GameObject item = Instantiate(questionItemPrefab, listContent);
        item.GetComponentInChildren<TextMeshProUGUI>().text = q.questionText;

        // Assign click event
        item.GetComponent<Button>().onClick.AddListener(() => {
            editor.LoadQuestion(q);
        });
    }

    public void RemoveQuestion(QuestionData q)
    {
        allQuestions.Remove(q);
        // รีโหลดรายการใหม่ทั้งหมด หรือ ลบเฉพาะปุ่มที่เกี่ยวข้อง
    }

}
