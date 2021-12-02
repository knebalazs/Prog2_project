using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class player_movement : MonoBehaviour {
    public enum movement {
        walking,
        rowing,
        fishing,
        bucket,
        idle
    }

    public Animator anim;
    private SpriteRenderer sp_renderer;
    public bool bucket_is_equipped = false;
    public bool is_draging_item;
    public movement current_movement;
    float offset;
    GameObject sky;
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
        inventory = GameObject.FindGameObjectWithTag("inventory");
        invent = new my_inventory();
        equ = new my_equip();
        ui_inventory.set_inventory(invent);
        inventory.SetActive(false);
        lamp.SetActive(false);
        is_draging_item = false;
        current_movement = movement.idle;
    }

    void Update() {

        watch_movement();


        if (Input.GetButtonDown("inventory"))
            inventory.SetActive(!inventory.activeSelf);


        if (sp_renderer.flipX)
            lamp.transform.position = this.transform.position + new Vector3(-0.55f, 0.08f, 0);
        else
            lamp.transform.position = this.transform.position + new Vector3(0.55f, 0.08f, 0);

    }


    public void watch_movement()
    {
        switch (current_movement)
        {
            case movement.idle:
                if (Input.GetAxis("Horizontal") != 0)
                    current_movement = movement.walking;

                if (Input.GetButtonDown("Jump"))
                {
                    current_movement = movement.rowing;
                    anim.SetBool("Is_row", true);
                    this.sp_renderer.sortingOrder = 3;
                }

                if (Input.GetButtonDown("use") && bucket_is_equipped == true)
                {
                    anim.SetBool("is_bucket_equipped", true);
                    current_movement = movement.bucket;
                }

                if (fishing.GetComponent<fishing>().is_equipped == true)
                {
                    anim.SetBool("is_fishing_rod_equipped", true);
                    current_movement = movement.fishing;
                }
                else
                    anim.SetBool("is_fishing_rod_equipped", false);

                break;

            case movement.walking:

                float x = Input.GetAxis("Horizontal") * Time.deltaTime * 2f;
                if (x < 0)
                    sp_renderer.flipX = true;
                if (x > 0)
                    sp_renderer.flipX = false;
                transform.Translate(x, 0, 0);
                anim.SetFloat("Speed", Mathf.Abs(x));

                if (Input.GetAxis("Horizontal") == 0)
                    current_movement = movement.idle;
                break;
            
            case movement.fishing:

                if (!EventSystem.current.IsPointerOverGameObject())
                    if(!is_draging_item)
                        fishing.GetComponent<fishing>().fishing_call();

                if (fishing.GetComponent<fishing>().is_casting)
                    anim.SetBool("is_casting", true);
                else
                    anim.SetBool("is_casting", false);
                if (fishing.GetComponent<fishing>().is_fishing)
                    anim.SetBool("is_fishing", true);
                else
                    anim.SetBool("is_fishing", false);

                if (fishing.GetComponent<fishing>().is_equipped == false)
                    current_movement = movement.idle;
                break;

            case movement.rowing:

                if (Input.GetButton("Jump"))
                {
                    offset += 0.2f * Time.deltaTime / 10f;
                    sky.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
                    if (GameObject.Find("player").GetComponent<stat_controller>().stamina > 0)
                        GameObject.Find("player").GetComponent<stat_controller>().stamina -= 11 * Time.deltaTime;
                }
                if (Input.GetButtonUp("Jump") || GameObject.Find("player").GetComponent<stat_controller>().stamina <= 0)
                {
                    anim.SetBool("Is_row", false);
                    this.sp_renderer.sortingOrder = 1;
                    current_movement = movement.idle;
                }
                break;

            case movement.bucket:
                if (GameObject.Find("player").GetComponent<stat_controller>().boat_status - 20 >= 0)
                    GameObject.Find("player").GetComponent<stat_controller>().boat_status -= 20 * Time.deltaTime;
                else
                    GameObject.Find("player").GetComponent<stat_controller>().boat_status = 0;

                if (GameObject.Find("player").GetComponent<stat_controller>().stamina > 0)
                    GameObject.Find("player").GetComponent<stat_controller>().stamina -= 20 * Time.deltaTime;
                else
                {
                    GameObject.Find("player").GetComponent<stat_controller>().stamina = 0;
                    anim.SetBool("is_bucket_equipped", false);
                    current_movement = movement.idle;
                }
                if (Input.GetButtonUp("use"))
                {
                    anim.SetBool("is_bucket_equipped", false);
                    current_movement = movement.idle;
                }
                break;
        }
    }
}
