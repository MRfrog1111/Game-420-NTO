using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class BugBehavior : Enemy
{
   // public int _waypointNum = 0;
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    public int movingState = 0; //0 - ходит, 1 - идет к игроку , 2 - аттакует 
    private Rigidbody rb;
    [SerializeField] private float maxDistance;
    [SerializeField] private int damage;

    [SerializeField] private GameObject attackHitbox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (movingState == 0)
        {
            EnemyMoving();
        }
        else
        {
            movingState = 1;
            transform.LookAt(player.transform);
            rb.MovePosition(transform.position + transform.forward*Time.deltaTime*speed);
            if (Vector3.Distance(transform.position, player.transform.position) > maxDistance)
            {
                Attack();
            }
        }
    }
    
    private void Attack()
    {
        
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
        }
    }
}
