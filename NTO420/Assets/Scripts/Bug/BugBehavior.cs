using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.AI; 

public class BugBehavior : MonoBehaviour
{
    // public int _waypointNum = 0;
    public int bug_number;
    private GameObject player;
    [SerializeField] private float speed;
    public int movingState = 0; //0 - ходит, 1 - идет к игроку , 2 - стоит 
    private Rigidbody rb;
    [SerializeField] private float maxDistance;
    [SerializeField] private int damage;

    [SerializeField] private GameObject attackHitbox;

    [Header("Animation")] private Animator animator;

    private Animation anim;

    string currentAnimationState;

    public GameObject deathSpawner;
    
    public NavMeshAgent agent;
    [SerializeField] private  float range; 

    private Transform centrePoint; 
    public static event Action<int> OnDeath;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        centrePoint = transform;
    }

    // Update is called once per fram

    
    void Update()
    {
        if(agent.remainingDistance <= agent.stoppingDistance) 
        {
            if (movingState == 0)
            {
                Vector3 point;
                if (RandomPoint(centrePoint.position, range, out point))
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                    agent.SetDestination(point);
                }
            }
           /* else if (gameObject.GetComponent<BugBehavior>().movingState == 0)
            {
                Vector3 point = 
                if (Vector3.Distance(transform.position, player.transform.position) <= maxDistance)
                {
                    Attack();
                    print("attack");
                }
            }*/
        }


        

    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range; 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) 
        { 
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

  void FixedUpdate()
    {
        /*if (movingState == 1)
        {
           // movingState = 1;
            transform.LookAt(player.transform);
            rb.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, player.transform.position) <= maxDistance)
            {
                Attack();
                print("attack");
            }
        }*/
        if (movingState == 1)
        {
            if (agent.remainingDistance <= maxDistance)
            {
                Attack();
                print("attack");
            }
            else
            {
                agent.SetDestination(player.transform.position);
            }
        }
    }

    public void ChangeAnimationState(string newState)
    {
        // STOP THE SAME ANIMATION FROM INTERRUPTING WITH ITSELF //
        if (currentAnimationState == newState) return;

        // PLAY THE ANIMATION //
        currentAnimationState = newState;
        animator.CrossFadeInFixedTime(currentAnimationState, 0.2f);
    }

    private void Attack()
    {
        ChangeAnimationState("Attack");
        StartCoroutine(waitAttack(2f));
        movingState = 2;
        attackHitbox.SetActive(true);

    }

    private IEnumerator waitAttack(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        ChangeAnimationState("Walk");
        attackHitbox.SetActive(false);
        movingState = 1;
        agent.SetDestination(player.transform.position);
    }

  /*  private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.name == "Player")
        {
            //coll.gameObject.GetComponent<HP>().GiveDamage(damage);
            //movingState = 1;
        }
    }*/

    private void OnTriggerEnter(Collider coll)
    {
        print("bug"+coll.gameObject.name);
        print("bugtag"+coll.gameObject.tag);
        if (coll.CompareTag("Player1"))
        {
            agent.SetDestination(Vector3.zero);
            player = coll.gameObject;
            movingState = 1;
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Player1"))
        {
            agent.SetDestination(Vector3.zero);
            movingState = 0;
            ChangeAnimationState("Walk");
        }
    }

    public void Death()
    {
        movingState = 2;
        ChangeAnimationState("Apperance");
        OnDeath?.Invoke(bug_number);
        GameObject spw = GameObject.Instantiate(deathSpawner);
        spw.transform.position = gameObject.transform.position;
        Destroy(this.gameObject,1f);
    }
}
