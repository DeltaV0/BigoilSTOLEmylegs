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

    public Transform head;

    public Transform[] eyes;

    public Rigidbody rb;

    public bool pathAvailable;
    public NavMeshPath navMeshPath;

    public Transform patrolPoints;

    public Vector3 playerLastSeen;

    public int wait;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshPath = new NavMeshPath();
        agent.SetDestination(transform.position);
        playerLastSeen = transform.position;
        //agent.updatePosition = false;
        //offset timers
        timer[1] = 20;
        timer[2] = 40;
        timer[3] = 60;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //unused rn
        if (wait > 0)
        {
            wait--;
        }
        //more advanced system ig
        //calc path
        NavMeshHit hit25;
        pathAvailable = agent.CalculatePath(player.position, navMeshPath);
        //idk what's broken here but the pathfinding is a bit broke
        if (!agent.Raycast(player.position, out hit25) && navMeshPath.status == NavMeshPathStatus.PathComplete)
        {
            //go to player if player is visible and it can actually get to the player
            agent.SetDestination(player.position);
            //set the variable to the last seen position
            playerLastSeen = player.position;
        }
        else
        {
            //if it cant see the player, go to the last seen player position
            agent.SetDestination(playerLastSeen);
        }
        //rotate body towards where moving (maybe not?)    
        //transform.LookAt(transform.position + agent.velocity);

        //look at player
        head.DODynamicLookAt(player.position, 5f);

        //move eyes seperately
        for (int a = 0; a < eyes.Length; a++)
        {
            eyes[a].DODynamicLookAt(player.position, 0.1f);
        }
        //go through all timers moving legs if it's zero
        for (int i = 0; i < tipstarget.Length; i++)
        {
            if (timer[i] <= 0)
            {
                //move leg x
                timer[i] = 80;
                RaycastHit hit;
                Physics.Raycast(offsets[i].position + (agent.velocity * 1.25f) * transform.localScale.z, Vector3.down, out hit, 100f, LayerMask.GetMask("Ground"));
                tipstargetPos[i] = hit.point;
                tipstarget[i].DOJump(tipstargetPos[i], Vector3.Distance(tipstarget[i].position, tipstargetPos[i]) * 0.2f, 1, 0.5f);
                //transform.DOShakeRotation(0.1f, 22.5f, 1, 22.5f, true);

            }
            else
            {
                timer[i]--;
            }

        }


    }

}
