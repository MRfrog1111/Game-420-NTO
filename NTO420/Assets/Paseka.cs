using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paseka : MonoBehaviour
{
    public int maxHoney = 100;
    public int HoneyInSecond = 5;
    public int HoneyNow = 0;
    public float _time = 5f;

    public Transform Camera;
    public LayerMask layerMask;

    private float hitRange = 3f;
    RaycastHit hit;

    private void Start()
    {
        StartCoroutine(Pasek());
        
    }

    private void Update()
    {
        
        /*if (Physics.Raycast(Camera.position, Camera.forward, out hit, hitRange, layerMask))
        {

        }*/
    }

    private IEnumerator Pasek()
    {
        while (true)
        {
            yield return new WaitForSeconds(_time);
            if (HoneyNow < maxHoney)
                HoneyNow += HoneyInSecond;
            else
                HoneyNow = 100;
            print("HoneyInPaseka " + HoneyNow);
        }
         
    }

}
