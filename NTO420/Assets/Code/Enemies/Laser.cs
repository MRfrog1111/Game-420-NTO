using UnityEngine;
using UnityEngine;
using System.Collections;
public class Laser : MonoBehaviour
{
    private bool isActive = true;
    public  float timeToWait;
    [SerializeField] private int damage;
    private void Start()
    {
        StartCoroutine(Wait(timeToWait));
    }

    private void OnTriggerEnter(Collider col) {

        if(col.gameObject.CompareTag("Player")) 
            col.gameObject.GetComponent<HP>().GiveDamage(damage);

    }
    private IEnumerator Wait(float waitTime) {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            isActive = !isActive;
            gameObject.GetComponent<BoxCollider>().enabled = isActive;
            gameObject.GetComponent<MeshRenderer>().enabled = isActive;
        }
    }

}
