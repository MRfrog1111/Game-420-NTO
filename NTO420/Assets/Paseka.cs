using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paseka : MonoBehaviour
{
    public int maxHoney = 100;
    public int HoneyInSecond = 5;
    public int HoneyNow = 0;
    public float _time;

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
        
        print("HoneyInPaseka " + HoneyNow);

        StartCoroutine(Pasek());
        if (Physics.Raycast(Camera.position, Camera.forward, out hit, hitRange, layerMask))
        {

        }
    }

    private IEnumerator Pasek()
    {
        if (HoneyNow < maxHoney)
            HoneyNow += HoneyInSecond;
        else
            HoneyNow = 100;
        yield return new WaitForSeconds(_time);   
    }

}
