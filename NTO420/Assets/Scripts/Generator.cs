using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public int maxHoney = 100;
    public int rashodHoney = 1;
    
    public float timeRashodHoney = 1f;

    public SlotInventory slot;
    
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
        slot.gameObject.SetActive(false);
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
            if (slot.count > 0)
            {
                yield return new WaitForSeconds(timeRashodHoney);
                slot.count -= rashodHoney;
                slot.itemCountText.text = slot.count.ToString();
            }
                
            
        }
        
    }

    private void OpenPanel()
    {
        generatorOpen = !generatorOpen;
        if (generatorOpen)
        {
            panelGenerator.SetActive(true);
            slot.gameObject.SetActive(true);
            managerUI.InventoryShow();
            managerUI.OpenResurses();
            Time.timeScale = 1f;
        }
        else
        {
            panelGenerator.SetActive(false);
            slot.gameObject.SetActive(false);
            managerUI.InventoryShow();
            managerUI.OpenResurses();
        }
            
    }
}
