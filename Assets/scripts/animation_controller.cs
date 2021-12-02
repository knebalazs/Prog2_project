using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation_controller : MonoBehaviour
{
    public Animator player_animator;
    private player_movement movement_reff;


    void Start()
    {
        movement_reff = GameObject.FindGameObjectWithTag("Fisherman").GetComponent<player_movement>();
    }


    void Update()
    {
        
    }
}
