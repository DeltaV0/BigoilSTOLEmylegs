using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public Animator anim;

    public Transform[] tips;

    public Transform[] tipstarget;

    public Vector3[] tipstargetPos;
    //public Transform[] tipstarget;

    public int[] timer;

    public Transform[] offsets;

    public Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //agent.updatePosition = false;
        //offset timers
        timer[1] = 10;
        timer[2] = 20;
        timer[3] = 30;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //target player with ai
        agent.SetDestination(player.position);
        transform.LookAt(transform.position + agent.velocity);
        //rb.linearVelocity = new Vector3(agent.desiredVelocity.x, 0, agent.desiredVelocity.z);
        //agent.nextPosition = rb.position;

        tipstarget[0].position = Vector3.Lerp(tipstarget[0].position, tipstargetPos[0], 0.1f);
        tipstarget[1].position = Vector3.Lerp(tipstarget[1].position, tipstargetPos[1], 0.1f);
        tipstarget[2].position = Vector3.Lerp(tipstarget[2].position, tipstargetPos[2], 0.1f);
        tipstarget[3].position = Vector3.Lerp(tipstarget[3].position, tipstargetPos[3], 0.1f);

        // Vector3.Lerp()

        if (timer[0] <= 0)
        {
            //move leg one
            timer[0] = 40;
            RaycastHit hit;
            Physics.Raycast(offsets[0].position, Vector3.down, out hit, 100f, LayerMask.GetMask("Ground"));
            tipstargetPos[0] = hit.point;

        }
        else
        {
            timer[0]--;
        }

        if (timer[2] <= 0)
        {
            //move leg 2
            timer[2] = 40;
            RaycastHit hit;
            Physics.Raycast(offsets[2].position, Vector3.down, out hit, 100f, LayerMask.GetMask("Ground"));
            tipstargetPos[2] = hit.point;
        }
        else
        {
            timer[2]--;
        }


        if (timer[1] <= 0)
        {
            //move leg 3
            timer[1] = 40;
            RaycastHit hit;
            Physics.Raycast(offsets[1].position, Vector3.down, out hit, 100f, LayerMask.GetMask("Ground"));
            tipstargetPos[1] = hit.point;
        }
        else
        {
            timer[1]--;
        }

        if (timer[3] <= 0)
        {
            //move leg 4
            timer[3] = 40;
            RaycastHit hit;
            Physics.Raycast(offsets[3].position, Vector3.down, out hit, 100f, LayerMask.GetMask("Ground"));
            tipstargetPos[3] = hit.point;
        }
        else
        {
            timer[3]--;
        }
        //tipstarget[0].position = new Vector3(tipstarget[0].position.x, hit.point.y, tipstarget[0].position.z);
        //Vector3.Lerp(tipstarget[0].position, hit.point, 0.01f);


    }

}
