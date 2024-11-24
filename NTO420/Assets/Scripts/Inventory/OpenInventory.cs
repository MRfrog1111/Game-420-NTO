using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    public GameObject inventory;

    private void Awake()
    {
        inventory.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventory.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void CloseInventory()
    {
        inventory.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
