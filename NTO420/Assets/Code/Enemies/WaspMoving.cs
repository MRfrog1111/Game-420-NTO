using System;
using Unity.VisualScripting;
using UnityEngine;

public class WaspMoving : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    private int movingState = 0; //0 - стоит, 1 - летит к игроку, 2 - отелетает от игрока 
    private Rigidbody rb;
    [SerializeField] private float maxDistance;
    [SerializeField] private int damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(player.transform);
        if (movingState == 2)
        {
            rb.MovePosition(transform.position - transform.forward*Time.deltaTime*speed);
           if (Vector3.Distance(transform.position, player.transform.position) > maxDistance)
           {
               movingState = 0;
           }
        }

        if (movingState == 1)
        {
            rb.MovePosition(transform.position + transform.forward*Time.deltaTime*speed);

        }
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
