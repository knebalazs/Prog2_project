using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour{
    public float speed = 2f;
    float offset;
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
            anim.SetBool("Is_row", is_rowing);
            Debug.Log("asdsdasdas");
            this.sp_renderer.sortingOrder = 5;
        }
        if(Input.GetButton("Jump"))
        {
            //Vector3 new_pos = GameObject.FindGameObjectWithTag("boat").transform.position + new Vector3(0, 0, 0);
            //this.transform.position = new_pos;
            // GameObject.FindGameObjectWithTag("background").transform.Translate(-1 * Time.deltaTime, 0, 0);
            offset += 0.2f * Time.deltaTime / 10f;
            GameObject.FindGameObjectWithTag("background").GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }
        if (Input.GetButtonUp("Jump"))
        {
            is_rowing = false;
            anim.SetBool("Is_row", is_rowing);
            this.sp_renderer.sortingOrder = 1;
        }

        if (sp_renderer.flipX)
            GameObject.FindGameObjectWithTag("lamp").transform.position = this.transform.position + new Vector3(-1f, 0.05f, 0);
        else
            GameObject.FindGameObjectWithTag("lamp").transform.position = this.transform.position + new Vector3(1f, 0.05f, 0);


        if (GameObject.FindGameObjectWithTag("fishing").GetComponent<fishing>().is_casting)
            anim.SetBool("is_casting", true);
        else
            anim.SetBool("is_casting", false);
        if (GameObject.FindGameObjectWithTag("fishing").GetComponent<fishing>().is_fishing)
            anim.SetBool("is_fishing", true);
        else
            anim.SetBool("is_fishing", false);


    }
    void doing_something()
    {
    }
}
