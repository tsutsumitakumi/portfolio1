using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1 : MonoBehaviour
{
    public void change_button()
    {
        SceneManager.LoadScene("stage01");
        Time.timeScale = 1f;
    }
}
