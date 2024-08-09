using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadmenu : MonoBehaviour
{




    // Hàm để load scene theo tên
    public void LoadSceneByName(string SampleScene)
    {
        SceneManager.LoadScene("Menu");
    }

}