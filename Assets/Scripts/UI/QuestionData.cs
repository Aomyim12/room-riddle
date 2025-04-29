using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionData
{
    public string questionText; // �Ӷ��
    public string mode; // ���� �� "���ҧ����", "�ͺ����", "�Ѻ���"
    public List<string> options = new List<string>(); // ������͡
    public int correctOptionIndex; // index �ͧ�ӵͺ���١��ͧ
}
