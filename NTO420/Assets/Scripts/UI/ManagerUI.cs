using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerUI : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject Setings;
    public GameObject PauseMenu;

    private bool InventoryOpen = false;
    private bool SetingsOpen = false;
    private bool PauseMenuOpen = false;

    private void Awake()
    {
        Inventory.SetActive(true);
        Inventory.SetActive(false);
        Setings.SetActive(false);
        PauseMenu.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)) InventoryShow();
        if(Input.GetKeyDown(KeyCode.Escape)) PauseMenuShow();
    }

    public void InventoryShow()
    {
        InventoryOpen = !InventoryOpen;
        if(InventoryOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Inventory.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Inventory.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    public void SetingsShow()
    {
        SetingsOpen = !SetingsOpen;
        if (SetingsOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Setings.SetActive(true);
            PauseMenu.SetActive(false);
            Time.timeScale = 0f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Setings.SetActive(false);
            PauseMenu.SetActive(true);
            Time.timeScale = 1f;
        }
    }
    public void PauseMenuShow()
    {
        PauseMenuOpen = !PauseMenuOpen;
        if (PauseMenuOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            PauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            PauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
