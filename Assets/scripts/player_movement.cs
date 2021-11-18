using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class player_movement : MonoBehaviour {
    float offset;
    public Animator anim;
    private SpriteRenderer sp_renderer;
    private bool is_moving = false;
    private bool is_rowing = false;
    private bool is_casting = false;
    private bool is_fishing = false;
    GameObject sky;
    GameObject boat;
    GameObject fishing;
    public GameObject lamp;
    public GameObject inventory;
    public my_inventory invent;
    public my_equip equ;
    [SerializeField] public UI_inventory ui_inventory;


    void Start() {
        sp_renderer = GetComponent<SpriteRenderer>();
        sky = GameObject.FindGameObjectWithTag("sky");
        lamp = GameObject.FindGameObjectWithTag("lamp");
        fishing = GameObject.FindGameObjectWithTag("fishing");
        boat = GameObject.FindGameObjectWithTag("boat");
        inventory = GameObject.FindGameObjectWithTag("inventory");
        //inventory.SetActive(false);
        invent = new my_inventory();
        equ = new my_equip();
        ui_inventory.set_inventory(invent);
        lamp.SetActive(false);
    }

    void Update() {
        rowing_movement();
        walking_movement();
        if (fishing.GetComponent<fishing>().is_equipped == true)
        {
            anim.SetBool("is_fishing_rod_equipped", true);
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            else
                fishing_movement();
        }
        else
            anim.SetBool("is_fishing_rod_equipped", false);



        if (Input.GetButtonDown("inventory"))
        {
            inventory.SetActive(!inventory.activeSelf);
        }


        if (sp_renderer.flipX)
            lamp.transform.position = this.transform.position + new Vector3(-0.55f, 0.08f, 0);
        else
            lamp.transform.position = this.transform.position + new Vector3(0.55f, 0.08f, 0);


    }



    void walking_movement()
    {
        if (is_moving | is_idle())
        {
            if (Input.GetAxis("Horizontal") != 0)
                is_moving = true;
            else
                is_moving = false;

            float x = Input.GetAxis("Horizontal") * Time.deltaTime * 2f;
            if (x < 0)
                sp_renderer.flipX = true;
            if (x > 0)
                sp_renderer.flipX = false;
            transform.Translate(x, 0, 0);
            anim.SetFloat("Speed", Mathf.Abs(x));
        }
        else
            is_moving = false;
    }

    void fishing_movement()
    {
        if (is_fishing | is_idle() )
        {
            fishing.GetComponent<fishing>().fishing_call();


            if (fishing.GetComponent<fishing>().is_casting)
                anim.SetBool("is_casting", true);
            else
                anim.SetBool("is_casting", false);
            if (fishing.GetComponent<fishing>().is_fishing)
                anim.SetBool("is_fishing", true);
            else
                anim.SetBool("is_fishing", false);
        }
        
    }

    void rowing_movement()
    {
        if (is_rowing | is_idle())
        {
            if (Input.GetButtonDown("Jump"))
            {
                is_rowing = true;
                anim.SetBool("Is_row", is_rowing);
                this.sp_renderer.sortingOrder = 5;
            }

            if (Input.GetButton("Jump"))
            {
                offset += 0.2f * Time.deltaTime / 10f;
                sky.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
                GameObject.Find("player").GetComponent<stat_controller>().stamina -= 11 * Time.deltaTime;
            }
            if (Input.GetButtonUp("Jump"))
            {
                is_rowing = false;
                anim.SetBool("Is_row", is_rowing);
                this.sp_renderer.sortingOrder = 1;
            }
        }
    }

    bool is_idle()
    {

        if (is_rowing || is_casting || is_fishing || is_moving)
            return false;
        else
            return true;
    }
}
