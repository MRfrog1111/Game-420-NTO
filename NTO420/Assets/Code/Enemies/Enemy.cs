using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Preferences")]
   // private bool Static;

    public Transform[] Waypoints;
    public float Speed;
    public  float TimeToWait;

    [Header("Technical")]
    private bool _wait;
    public bool _isMovingForward = true;

    public int _waypointNum = 0;

   /* public void Initialize(Vector3[] waypoints, float speed, float timeToWait) {

        Waypoints = waypoints;
        Speed = speed;
        TimeToWait = timeToWait;

        Static = false;

    }

    public void Initialize() {

        Static = true;

    }*/

    /*public virtual void FixedUpdate() {

        if(!Static) EnemyMoving();

    }*/

    public  void EnemyMoving() {

        if(_wait) return;
        
        if (_waypointNum == Waypoints.Length)
        {
            _isMovingForward = false;
            _waypointNum -= 1;
        }

        if (_waypointNum == -1)
        {
            _isMovingForward = true;
            _waypointNum += 1;
        }
        gameObject.transform.LookAt(Waypoints[_waypointNum]);
        transform.position = Vector3.MoveTowards(transform.position, Waypoints[_waypointNum].transform.position, Time.deltaTime * Speed);
        //Vector3 check = new Vector3(Waypoints[_waypointNum].x,transform.position.y,);
        
        if(Vector3.Distance(transform.position, Waypoints[_waypointNum].transform.position) < 1f)
        {
            if (_isMovingForward) {
                 _waypointNum += 1;
            } 
            else {
                 _waypointNum -= 1;
            }
            StartCoroutine(Wait(TimeToWait));
            _wait = true;
        }
        

    }

    IEnumerator Wait(float waitTime) {

        yield return new WaitForSeconds(waitTime);

        _wait = false;

    }

}
