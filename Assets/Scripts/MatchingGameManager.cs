using UnityEngine;
using System.Collections.Generic;

public class MatchingGameManager : MonoBehaviour
{
    public static MatchingGameManager instance;

    public List<DropZone> allDropZones = new List<DropZone>();
    public int correctMatches = 0;
    public int totalMatches = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        totalMatches = allDropZones.Count;
    }

    public void CheckGameCompletion()
    {
        correctMatches = 0;

        foreach (DropZone zone in allDropZones)
        {
            if (zone.matchedItem != null)
            {
                correctMatches++;
            }
        }

        if (correctMatches == totalMatches)
        {
            Debug.Log("เกมจบ! คุณชนะ!");
            // แสดงหน้าจอชนะหรือเอฟเฟกต์
        }
    }
}