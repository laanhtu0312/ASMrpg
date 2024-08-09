using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadgame : MonoBehaviour
{
   



    // Hàm để load scene theo tên
    public void LoadSceneByName(string SampleScene)
    {
        SceneManager.LoadScene("SampleScene");
    }

}
