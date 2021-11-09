using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weather_controller : MonoBehaviour
{
    private GameObject sun;
    private GameObject rain;
    //private GameObject mist;
    private GameObject background;
    private Light Global_light;
    float weather_duration;
    weather current_weather;
    ParticleSystem.EmissionModule rain_emission;
    void Start()
    {
        current_weather = new weather();
        sun = GameObject.Find("sun");
        rain = GameObject.Find("rain");
        rain_emission = rain.GetComponent<ParticleSystem>().emission;
    }

    void Update()
    {
        Debug.Log(current_weather.type);
        Debug.Log(weather_duration);
        if (weather_duration < 0)
            set_weather();
        else
            weather_duration -= 1f * Time.deltaTime;
    }

    void set_weather()
    {
        rain.SetActive(false);
        current_weather.set_random();
        weather_duration = Random.Range(5, 8);
        switch (current_weather.type)
        {
            case weather.weather_type.rainy:
                rain.SetActive(true);
                rain_emission.rateOverTime = 10f;
                break;
            case weather.weather_type.storm:
                rain.SetActive(true);
                rain_emission.rateOverTime = 200f;
                break;
        }
    }

}
