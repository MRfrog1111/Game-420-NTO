using System;
using UnityEngine;
using System.Collections;
using UnityEngine;

public class TrapMoving : MonoBehaviour
{
    private bool isStatic;

    [SerializeField] private float range;
    public float Speed;
    public  float TimeToWait;
    private Vector3 target,pos1,pos2;
    private bool _wait;
    public bool _isMovingForward = true;
    [SerializeField] private int damage;
    private void Start()
    {
        target = transform.position;
        pos1 = transform.position;
        pos2 = new Vector3(transform.position.x, transform.position.y + range, transform.position.z);

    }

    public virtual void FixedUpdate() {

        if(!isStatic) Moving();

    }

    public  void Moving() {

        if(_wait) return;
        
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * Speed);
        
        if(Vector3.Distance(transform.position, target) < 0.001f)
        {
            if (_isMovingForward)
            {
                target = pos1;
            }
            else
            {
                target = pos2;
            }

            _isMovingForward = !_isMovingForward;
            StartCoroutine(Wait(TimeToWait));
            _wait = true;
        }
        

    }
   

    private void OnTriggerEnter(Collider col) {

        if(col.gameObject.CompareTag("Player")) 
            col.gameObject.GetComponent<HP>().GiveDamage(damage);

    }
    IEnumerator Wait(float waitTime) {

        yield return new WaitForSeconds(waitTime);

        _wait = false;

    }

}
