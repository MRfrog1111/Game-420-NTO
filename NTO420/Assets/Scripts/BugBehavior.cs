using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class BugBehavior : Enemy
{
   // public int _waypointNum = 0;
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    public int movingState = 0; //0 - ходит, 1 - идет к игроку , 2 - стоит 
    private Rigidbody rb;
    [SerializeField] private float maxDistance;
    [SerializeField] private int damage;
    
    [SerializeField] private GameObject attackHitbox;

    [Header("Animation")] 
    private Animator animator;

    private Animation anim;
    string currentAnimationState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (movingState == 0)
        {
            EnemyMoving();
        }
        else if (movingState == 1)
        {
            movingState = 1;
            transform.LookAt(player.transform);
            rb.MovePosition(transform.position + transform.forward*Time.deltaTime*speed);
            if (Vector3.Distance(transform.position, player.transform.position) > maxDistance)
            {
                Attack();
                print("attack");
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

    }

    private IEnumerator waitAttack(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        movingState = 1;
    }
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "Player")
        {
            coll.gameObject.GetComponent<HP>().GiveDamage(damage);
            movingState = 2;
        }
    }
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            movingState = 1;
        }
    }
    private void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Player")
        {
            movingState = 0;
            ChangeAnimationState("Walk");
        }
    }
}
