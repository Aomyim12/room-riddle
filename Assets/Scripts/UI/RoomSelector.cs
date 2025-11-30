using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSelector : MonoBehaviour
{
    public GameObject engRoom;
    public GameObject scienceRoom;
    public GameObject mathRoom;

    // ปิดทั้งหมดก่อน
    void DisableAllRooms()
    {
        engRoom.SetActive(false);
        scienceRoom.SetActive(false);
        mathRoom.SetActive(false);
    }

    public void OpenEngRoom()
    {
        DisableAllRooms();
        engRoom.SetActive(true);
    }

    public void OpenScienceRoom()
    {
        DisableAllRooms();
        scienceRoom.SetActive(true);
    }

    public void OpenMathRoom()
    {
        DisableAllRooms();
        mathRoom.SetActive(true);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
