using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class main_menu : MonoBehaviour
{
    public void exit_button()
    {
        Application.Quit();
    }

    public void play_button()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
