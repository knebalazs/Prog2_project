using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class day_night : MonoBehaviour
{
    private GameObject sun;
    private Transform sun_transform;
    private Vector3 pos_change;

    float StartPointX = 0;
    float StartPointY = 0;
    float ControlPointX = 20;
    float ControlPointY = 50;
    float EndPointX = 0;
    float EndPointY = 0;
    float CurveX;
    float CurveY;
    float BezierTime = 0;
    Transform mySphere;

    void Start()
    {
        sun = GameObject.FindGameObjectWithTag("sun");
        sun_transform = sun.GetComponent<Transform>();
        pos_change = Vector3.zero;
    }

    void Update()
    {
        /*
        Debug.Log(sun_transform.position.x);
        if (sun_transform.position.x < 13)
            sun_transform.position += new Vector3(2f, 5f, 0f) * Time.deltaTime ;
        else
        {
            Debug.Log("asd");
            sun_transform.position += new Vector3(2f, -5f, 0f) * Time.deltaTime;
        }
        */




        BezierTime = BezierTime + Time.deltaTime;

        if (BezierTime  <= 1)
     {
            BezierTime = 0;
        }

        CurveX = (((1 - BezierTime) * (1 - BezierTime)) * StartPointX) + (2 * BezierTime * (1 - BezierTime) * ControlPointX) + ((BezierTime * BezierTime) * EndPointX);
        CurveY = (((1 - BezierTime) * (1 - BezierTime)) * StartPointY) + (2 * BezierTime * (1 - BezierTime) * ControlPointY) + ((BezierTime * BezierTime) * EndPointY);
        sun_transform.position = new Vector3(CurveX, CurveY, 0) * Time.deltaTime / 10f;
    }
}
