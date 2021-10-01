using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour{
    public float speed = 2f;
    public Animator anim;
    private bool is_rowing = false;
    private SpriteRenderer sp_renderer;
    

    // Start is called before the first frame update
    void Start(){
        sp_renderer = GetComponent<SpriteRenderer>();   
    }

    // Update is called once per frame
    void Update(){
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        if (!is_rowing){
            if (x < 0)
                sp_renderer.flipX = true;
            if (x > 0)
                sp_renderer.flipX = false;
            transform.Translate(x, 0, 0);
            anim.SetFloat("Speed", Mathf.Abs(x));
        }
        if (Input.GetButtonDown("Jump")){
            is_rowing = true;
        }
        if (Input.GetButtonUp("Jump"))
            is_rowing = false;
        anim.SetBool("Is_row", is_rowing);


    }
}
