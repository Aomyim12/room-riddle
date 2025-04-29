using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionData
{
    public string questionText; // คำถาม
    public string mode; // โหมด เช่น "สร้างรหัส", "ตอบเร็ว", "จับคู่"
    public List<string> options = new List<string>(); // ตัวเลือก
    public int correctOptionIndex; // index ของคำตอบที่ถูกต้อง
}
