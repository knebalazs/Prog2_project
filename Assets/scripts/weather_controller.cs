using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weather_controller : MonoBehaviour
{
    private GameObject sun;
    private GameObject rain;
    private GameObject fog;
    private GameObject heat_wave;
    private GameObject background;
    private UnityEngine.Experimental.Rendering.Universal.Light2D global_light;
    float weather_duration;
    public weather current_weather;
    ParticleSystem.EmissionModule rain_emission;
    void Start()
    {
        current_weather = new weather();
        sun = GameObject.Find("sun");
        rain = GameObject.Find("rain");
        fog = GameObject.Find("fog");
        heat_wave = GameObject.Find("heat_wave");
        rain_emission = rain.GetComponent<ParticleSystem>().emission;
        global_light = this.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
    }

    void Update()
    {
        if (weather_duration < 0)
            set_random_weather();
        else
            weather_duration -= 1f * Time.deltaTime;
    }

    void set_random_weather()
    {
        rain.SetActive(false);
        fog.SetActive(false);
        heat_wave.SetActive(false);
        current_weather.set_random();
        switch (current_weather.type)
        {
            case weather.weather_type.clear:
                weather_duration = Random.Range(40, 80);
                break;
            case weather.weather_type.cloudy:
                weather_duration = Random.Range(30, 60);
                break;
            case weather.weather_type.rainy:
                weather_duration = Random.Range(20, 60);
                rain.SetActive(true);
                rain_emission.rateOverTime = 10f;
                break;
            case weather.weather_type.storm:
                weather_duration = Random.Range(20, 40);
                rain.SetActive(true);
                fog.SetActive(true);
                rain_emission.rateOverTime = 200f;
                fog.GetComponent<Renderer>().material.SetVector("_FogSpeed", new Vector3(0.5f, 0, 0));
                break;
            case weather.weather_type.misty:
                weather_duration = Random.Range(20, 40);
                fog.SetActive(true);
                break;
            case weather.weather_type.heat_wave:
                if (Vector3.Distance(sun.transform.position, GameObject.Find("end_point").transform.position) > 16 && Vector3.Distance(sun.transform.position, GameObject.Find("end_point").transform.position) < 30)
                {
                    weather_duration = Random.Range(20, 30);
                    global_light.intensity = 1.1f;
                    heat_wave.SetActive(true);
                }
                else
                    set_random_weather();
                break;
        }
    }

}
