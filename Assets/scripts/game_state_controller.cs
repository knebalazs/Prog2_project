using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game_state_controller : MonoBehaviour
{
    GameObject game_over_ui;
    GameObject escape_ui;
    stat_controller stat_ref;
    // Start is called before the first frame update
    void Start()
    {
        game_over_ui = GameObject.Find("Game_over");
        escape_ui = GameObject.Find("escape");
        stat_ref = GameObject.Find("player").GetComponent<stat_controller>();
        game_over_ui.SetActive(false);
        escape_ui.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escape_ui.SetActive(!escape_ui.activeSelf);
        }

        if (stat_ref.health <= 0 || stat_ref.boat_status >= 100)
            game_over_ui.SetActive(true);
    }

    public void restart_button()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void exit_button()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
