using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishing : MonoBehaviour
{
    public float power = 1f;
    public bool is_fishing = false;
    public bool is_casting = false;
    public bool is_equipped = false;
    public bool is_catching = false;
    public bool catched = false;
    Vector2 DragStartPosition;
    private Rigidbody2D rb_bait;
    private LineRenderer lr_trajectory;
    private LineRenderer lr_rope;
    private CircleCollider2D bait_collider;
    GameObject fisherman;
    GameObject bait;
    GameObject boat;
    void Start()
    {
        rb_bait = GameObject.FindGameObjectWithTag("bait").GetComponent<Rigidbody2D>();
        bait_collider = GetComponent<CircleCollider2D>();

    }

    public void fishing_call()
    {
        GameObject fisherman = GameObject.FindGameObjectWithTag("Fisherman");
        GameObject bait = GameObject.FindGameObjectWithTag("bait");
        GameObject boat = GameObject.FindGameObjectWithTag("boat");

        ////////////THROWING////////////
        if (Input.GetMouseButtonDown(0) && is_fishing == false)
        {
            if (GameObject.Find("player").GetComponent<stat_controller>().stamina - 30 > 0)
                GameObject.Find("player").GetComponent<stat_controller>().stamina -= 30;
            else
                GameObject.Find("player").GetComponent<stat_controller>().stamina = 0;

            DragStartPosition = fisherman.transform.position;
            bait.transform.position = fisherman.transform.position;
            is_casting = true;
            bait.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (Input.GetMouseButton(0) && is_fishing == false)
        {
            Vector2 DragEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 bait_velocity = (DragEndPosition - DragStartPosition) * power;
            bait.GetComponent<trajectory_line>().call_trajectory(rb_bait, bait.transform.position, bait_velocity, 500);
        }

        if (Input.GetMouseButtonUp(0) && is_fishing == false)
        {
            is_fishing = true;
            rb_bait.gravityScale = 3f;
            Vector2 DragEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 _velocity = (DragEndPosition - DragStartPosition) * power;
            rb_bait.velocity = _velocity;
            StartCoroutine(delete_trajectory_line(1));
        }
        ///////////DRAW ROPE//////////

        if (is_fishing)
        {
            GameObject.FindGameObjectWithTag("rope").GetComponent<verlet_rope>().DrawRope();
            if (Random.Range(0, 1000) == 1)
            {
                is_catching = true;
                catched = true;
            }
            if(is_catching == true)
                rb_bait.velocity = new Vector2 (0, Random.Range(-8,8));
        }

        ///////////PULLING///////////
        if (Input.GetMouseButtonDown(0) && is_fishing == true)
        {
            is_casting = false;
            is_catching = false;
            if(catched)
                bait.GetComponent<SpriteRenderer>().enabled = true;
            Physics2D.IgnoreCollision(bait.GetComponent<CircleCollider2D>(), boat.GetComponent<PolygonCollider2D>());
            DragStartPosition = bait.transform.position;
            Vector2 DragEndPosition = fisherman.transform.position;
            Vector2 _velocity = (DragEndPosition - DragStartPosition) * power;
            rb_bait.velocity = _velocity;
        }

        if (Input.GetMouseButtonUp(0) && is_casting == false)
        {
            if (rb_bait.gravityScale - 1 >= 0)
                rb_bait.gravityScale--;
        }

        if (Vector3.Distance(bait.transform.position, fisherman.transform.position) < 1 && is_casting == false)
        {
            rb_bait.velocity = Vector2.zero;
            bait.transform.position = fisherman.transform.position;
            is_fishing = false;
            if (catched)
            {
                catched = false;
                bait.GetComponent<SpriteRenderer>().enabled = false;
                fisherman.gameObject.GetComponent<player_movement>().invent.add_item(new my_item { item_type = my_item.Item_type.fish, amount = 1 });
                GameObject.FindGameObjectWithTag("Fisherman").GetComponent<player_movement>().ui_inventory.refresh_inventory();
            }
            GameObject.FindGameObjectWithTag("rope").GetComponent<LineRenderer>().positionCount = 0;

        }
    }
    IEnumerator delete_trajectory_line(float wait_time)
    {
        yield return new WaitForSeconds(wait_time);
        GameObject.FindGameObjectWithTag("bait").GetComponent<LineRenderer>().positionCount = 0;        
    }
}
