using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

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

        
        //tipstarget[0].position = Vector3.Lerp(tipstarget[0].position, tipstargetPos[0], timer[0] / 40f);
        //tipstarget[1].position = Vector3.Lerp(tipstarget[1].position, tipstargetPos[1], timer[0] / 40f);
        //tipstarget[2].position = Vector3.Lerp(tipstarget[2].position, tipstargetPos[2], timer[0] / 40f);
        //tipstarget[3].position = Vector3.Lerp(tipstarget[3].position, tipstargetPos[3], timer[0] / 40f);

        // Vector3.Lerp()
        
        if (timer[0] <= 0)
        {
            //move leg one
            timer[0] = 40;
            RaycastHit hit;
            Physics.Raycast(offsets[0].position + agent.velocity * 0.5f, Vector3.down, out hit, 100f, LayerMask.GetMask("Ground"));
            tipstargetPos[0] = hit.point;
            tipstarget[0].DOJump(tipstargetPos[0], Vector3.Distance(tipstarget[0].position, tipstargetPos[0]) * 0.25f, 1, 0.25f);
            //transform.DOShakeRotation(0.1f, 22.5f, 1, 22.5f, true);

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
            Physics.Raycast(offsets[2].position + agent.velocity * 0.5f, Vector3.down, out hit, 100f, LayerMask.GetMask("Ground"));
            tipstargetPos[2] = hit.point;
            tipstarget[2].DOJump(tipstargetPos[2], Vector3.Distance(tipstarget[2].position, tipstargetPos[2]) * 0.25f, 1, 0.25f);
            //transform.DOShakeRotation(0.1f, 22.5f, 1, 22.5f, true);
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
            Physics.Raycast(offsets[1].position + agent.velocity * 0.5f, Vector3.down, out hit, 100f, LayerMask.GetMask("Ground"));
            tipstargetPos[1] = hit.point;
            tipstarget[1].DOJump(tipstargetPos[1], Vector3.Distance(tipstarget[1].position, tipstargetPos[1]) * 0.25f, 1, 0.25f);
           // transform.DOShakeRotation(0.1f, 22.5f, 1, 22.5f, true);
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
            Physics.Raycast(offsets[3].position + agent.velocity * 0.5f, Vector3.down, out hit, 100f, LayerMask.GetMask("Ground"));
            tipstargetPos[3] = hit.point;
            tipstarget[3].DOJump(tipstargetPos[3], Vector3.Distance(tipstarget[3].position, tipstargetPos[3]) * 0.25f, 1, 0.25f);
            //transform.DOShakeRotation(0.1f, 22.5f, 1, 22.5f, true);
        }
        else
        {
            timer[3]--;
        }
        //tipstarget[0].position = new Vector3(tipstarget[0].position.x, hit.point.y, tipstarget[0].position.z);
        //Vector3.Lerp(tipstarget[0].position, hit.point, 0.01f);


    }

}
