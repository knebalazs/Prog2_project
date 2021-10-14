using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trajectory_line : MonoBehaviour
{
    private LineRenderer lr;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void call_trajectory(Rigidbody2D rb,  Vector2 position, Vector2 rb_velocity, int point_count)
    {
            Vector2[] trajectory = Plot(rb, position, rb_velocity, point_count);
            lr.positionCount = trajectory.Length;
            Vector3[] positions = new Vector3[trajectory.Length];
            for (int i = 0; i < trajectory.Length; i++)
            {
                positions[i] = trajectory[i];
            }
            lr.SetPositions(positions);
    }

    public Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps)
    {
        Vector2[] results = new Vector2[steps];

        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * 3f * timestep * timestep;

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
}
