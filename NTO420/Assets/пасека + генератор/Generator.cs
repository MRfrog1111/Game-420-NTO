using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public int maxHoney = 100;
    public int rashodHoney = 100;
    public int HoneyNow = 100;
    
    public float timeRashodHoney = 1f;


    public GameObject panelGenerator;
    public Transform camera;
    public LayerMask layerMask;

    public ManagerUI managerUI;
    private bool generatorOpen = false;

    private float hitRange = 3f;
    RaycastHit hit;

    private void Awake()
    {
        panelGenerator.SetActive(false);
    }
    private void Start()
    {
        StartCoroutine(GeneratorOfHoney());
    }

    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(camera.position, camera.forward, out hit, hitRange, layerMask))
            {
                OpenPanel();
                GeneratorOfHoney();
            }
        }


    }

    private IEnumerator GeneratorOfHoney()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeRashodHoney);
            if (HoneyNow > 0)
                HoneyNow -= rashodHoney;
        }
        
    }

    private void OpenPanel()
    {
        generatorOpen = !generatorOpen;
        if (generatorOpen)
        {
            panelGenerator.SetActive(true);
            
            ///managerUI.OpenResurses();
        }
        else
        {
            panelGenerator.SetActive(false);
            
           // managerUI.OpenResurses();
        }
            
    }
}
