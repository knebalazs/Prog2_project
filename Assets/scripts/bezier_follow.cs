using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bezier_follow : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes;

    private int routeToGo;

    private float tParam;

    private Vector2 objectPosition;

    //private float speedModifier;

    private bool coroutineAllowed;

    // Start is called before the first frame update
    void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        //speedModifier = 0.5f;
        coroutineAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
        if(GameObject.FindGameObjectWithTag("sun").transform.position.x < 13 && GameObject.FindGameObjectWithTag("sun").transform.position.y > 0 && GameObject.FindGameObjectWithTag("sky").GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity <= 1)
            GameObject.FindGameObjectWithTag("sky").GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity += 0.0005f;
        if(GameObject.FindGameObjectWithTag("sun").transform.position.x > 19 && GameObject.FindGameObjectWithTag("sky").GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity >= 0.1)
            GameObject.FindGameObjectWithTag("sky").GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity -= 0.0005f;
    }

    private IEnumerator GoByTheRoute(int routeNum)
    {
        coroutineAllowed = false;

        Vector2 p0 = routes[routeNum].GetChild(0).position;
        Vector2 p1 = routes[routeNum].GetChild(1).position;
        Vector2 p2 = routes[routeNum].GetChild(2).position;
        Vector2 p3 = routes[routeNum].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime / 20f;

            objectPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;

            transform.position = objectPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;

        routeToGo += 1;

        if (routeToGo > routes.Length - 1)
        {
            routeToGo = 0;
        }

        coroutineAllowed = true;

    }
}
