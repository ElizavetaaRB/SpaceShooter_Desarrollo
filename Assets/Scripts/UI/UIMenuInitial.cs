using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuInitial : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("InitialScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
