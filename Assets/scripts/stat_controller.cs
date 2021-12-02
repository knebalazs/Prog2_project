using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stat_controller : MonoBehaviour
{
    public float health = 100;
    public float stamina = 100;
    public float thirst = 100;
    public float hunger = 100;
    public float boat_status = 0;
    public float bottle_contains;
    GameObject boat_status_obj;

    private void Awake()
    {
        bottle_contains = 2;
    }
    void Start()
    {
        boat_status_obj = GameObject.Find("boat_status");
        boat_status_obj.SetActive(false);
       
    }

    void Update()
    {
        stat_control();
        if(stamina < 100)
            stamina += 5 * Time.deltaTime;
        if (GameObject.Find("weather").GetComponent<weather_controller>().current_weather.type == weather.weather_type.rainy || GameObject.Find("weather").GetComponent<weather_controller>().current_weather.type == weather.weather_type.storm)
            bottle_contains = 3;
    }

    void stat_control()
    {
        if (thirst > 0)
            thirst -= 1f * Time.deltaTime * 4;
        if (hunger > 0)
            hunger -= 1f * Time.deltaTime;
        if (thirst <= 0 && health - 1 > -1)
            health -= 1f * Time.deltaTime * 2;
        if (hunger <= 0 && health - 1 > -1)
            health -= 1f * Time.deltaTime * 2;

        if (GameObject.Find("weather").GetComponent<weather_controller>().current_weather.type == weather.weather_type.rainy && boat_status < 100)
            boat_status += 1 * Time.deltaTime;
        if (GameObject.Find("weather").GetComponent<weather_controller>().current_weather.type == weather.weather_type.storm && boat_status < 100)
            boat_status += 10 * Time.deltaTime;
        if (boat_status != 0)
        {
            boat_status_obj.SetActive(true);
            GameObject.Find("fill").GetComponent<Image>().fillAmount = boat_status / 100;
        }
        GameObject.Find("thirst").GetComponent<Image>().fillAmount = thirst/100;
        GameObject.Find("hunger").GetComponent<Image>().fillAmount = hunger / 100;
        GameObject.Find("hp").GetComponent<Image>().fillAmount = health / 100;
        GameObject.Find("energy").GetComponent<Image>().fillAmount = stamina / 100;
    }
}
