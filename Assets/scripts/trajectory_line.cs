using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trajectory_line : MonoBehaviour
{
    public float power = 1f;
    private Vector3 DragStartPosition;
    Rigidbody2D rb;
    LineRenderer lr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DragStartPosition = GameObject.FindGameObjectWithTag("Fisherman").transform.position;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 DragEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 _velocity = (DragEndPosition - DragStartPosition) * power;
            
            Vector2[] trajectory = Plot(rb, (Vector2)transform.position, _velocity, 500);
            lr.positionCount = trajectory.Length;
            Vector3[] positions = new Vector3[trajectory.Length];
            for (int i = 0; i < trajectory.Length; i++)
            {
                positions[i] = trajectory[i];
            }
            lr.SetPositions(positions);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 DragEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 _velocity = (DragEndPosition - DragStartPosition) * power;
            rb.velocity = _velocity;
            StartCoroutine(wait_coroutine(1));
           
           
        }

    }

    public Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps)
    {
        Vector2[] results = new Vector2[steps];

        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * rigidbody.gravityScale * timestep * timestep;

        float drag = 1f - timestep * rigidbody.drag;
        Vector2 movestep = velocity * timestep;

        for (int i = 0; i < steps; i++)
        {
            movestep += gravityAccel;
            movestep *= drag;
            pos += movestep;
            results[i] = pos;
        }

        return results;
    }

    IEnumerator wait_coroutine(float wait_time)
    {
        yield return new WaitForSeconds(wait_time);
        lr.SetVertexCount(0);

    }
}
