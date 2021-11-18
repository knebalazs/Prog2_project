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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stat_control();
        stamina += 5 * Time.deltaTime;
    }

    void stat_control()
    {
        thirst -= 1f * Time.deltaTime * 4;
        hunger -= 1f * Time.deltaTime;
        if (thirst < 0)
            health -= 1f * Time.deltaTime * 2;
        if (hunger < 0)
            health -= 1f * Time.deltaTime * 2;
        GameObject.Find("thirst").GetComponent<Image>().fillAmount = thirst/100;
        GameObject.Find("hunger").GetComponent<Image>().fillAmount = hunger / 100;
        GameObject.Find("hp").GetComponent<Image>().fillAmount = health / 100;
        GameObject.Find("energy").GetComponent<Image>().fillAmount = stamina / 100;
    }
}
