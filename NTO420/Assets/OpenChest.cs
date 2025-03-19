using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    [SerializeField] private GameObject[] chest;
    [SerializeField] private GameObject[] chsetInfo;
    [SerializeField] private GameObject slotInventory;
    [SerializeField] private GameObject inventoryInterf;
    [SerializeField] private GameObject chestInterf;

    private float rangeHit = 3f;

    private void Start()
    {
        chestInterf.SetActive(false);
    }

    private void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, rangeHit, layer))
        {
            if (hit.collider.gameObject.tag == "Chest" && Input.GetKeyDown(KeyCode.E))
            {
                for (int i = chest.Length - 1; i < chest.Length; i++)
                {
                    chsetInfo[i].SetActive(false);
                }

                for (int i = chest.Length - 1; i < chest.Length; i++)
                {
                    if(hit.collider.gameObject == chest[i])
                    {
                        chestInterf.SetActive(true);
                        slotInventory.SetActive(true);
                        inventoryInterf.SetActive(true);
                        chsetInfo[i].SetActive(true);

                        Time.timeScale = 0f;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            for (int i = chest.Length - 1; i < chest.Length; i++)
            {
                chsetInfo[i].SetActive(false);
                chestInterf.SetActive(false);
                inventoryInterf.SetActive(false);
                slotInventory.SetActive(false);
                Time.timeScale = 1f;
            }
        }

    }
}
