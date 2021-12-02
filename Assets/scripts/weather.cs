using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weather 
{
   public enum weather_type
    {
        cloudy,
        rainy,
        misty,
        clear,
        storm,
        heat_wave
    }

    bool is_daytime;
    public weather_type type;


    public void set_random()
    {
       type = (weather_type)Random.Range(0, 6);
    }

    public void set_weather(weather_type new_type)
    {
        type = new_type;
    }

}
