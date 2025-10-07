using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public Animator anim;

    public Transform[] tips;

    public Transform[] tipstarget;
    //public Transform[] tipstarget;

    public int[] timer;

    public Transform[] offsets;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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


       // Vector3.Lerp()

        if (timer[0] <= 0)
        {
            //move leg one
            timer[0] = 40;
            RaycastHit hit;
            Physics.Raycast(offsets[0].position, Vector3.down, out hit, 100f, LayerMask.GetMask("Ground"));
            tipstarget[0].position = hit.point;

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
            tipstarget[2].position = hit.point;
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
            tipstarget[1].position = hit.point;
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
            tipstarget[3].position = hit.point;
        }
        else
        {
            timer[3]--;
        }
        //tipstarget[0].position = new Vector3(tipstarget[0].position.x, hit.point.y, tipstarget[0].position.z);
        //Vector3.Lerp(tipstarget[0].position, hit.point, 0.01f);


    }

}
