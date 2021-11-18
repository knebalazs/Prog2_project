using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class day_cycle_controller : MonoBehaviour
{
    private GameObject sun;
    private GameObject moon;
    private UnityEngine.Experimental.Rendering.Universal.Light2D global_light;
    [SerializeField] private Transform start_point;
    [SerializeField] private Transform curve_point;
    [SerializeField] private Transform end_point;
    private float interpolate_amount;
    public float cycle_speed;
    bool is_daytime;
    void Start()
    {
        sun = GameObject.Find("sun");
        moon = GameObject.Find("moon");
        global_light = GameObject.Find("weather").GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        is_daytime = true;
        cycle_speed = 0.20f;
    }

    void Update()
    {
        simulate_day_cycle();
    }

    public void route_follow(GameObject object_reff)
    {
        interpolate_amount = interpolate_amount + Time.deltaTime * cycle_speed / 10;
        var ab = Vector3.Lerp(start_point.position, curve_point.position, interpolate_amount);
        var bc = Vector3.Lerp(curve_point.position, end_point.position, interpolate_amount);
        object_reff.transform.position = Vector3.Lerp(ab, bc, interpolate_amount);
    }

    public void simulate_day_cycle()
    {
        if (is_daytime)
            route_follow(sun);
        else
            route_follow(moon);


        if (sun.transform.position == end_point.position && is_daytime)
        {
            interpolate_amount %= 1;
            is_daytime = false;
        }

        if (moon.transform.position == end_point.position && !is_daytime)
        {
            interpolate_amount %= 1;
            moon.transform.position = start_point.position;
            is_daytime = true;
        }

        if (Vector3.Distance(sun.transform.position, end_point.position) > 26 && global_light.intensity <= 1)
            global_light.intensity += (1 - global_light.intensity) * Time.deltaTime / 10;
        if (Vector3.Distance(sun.transform.position, end_point.position) < 14.5 && global_light.intensity >= 0.1)
            global_light.intensity -= 0.0005f;
    }
}
