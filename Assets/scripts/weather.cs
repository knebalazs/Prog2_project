using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weather 
{
   public enum weather_type
    {
        storm,
        rainy,
        cloudy,
        misty,
        sunny,
        heat_wave
    }

    bool is_daytime;
    public weather_type type;


    public void set_random()
    {
       type = (weather_type)Random.Range(0, 6);
    }

}
