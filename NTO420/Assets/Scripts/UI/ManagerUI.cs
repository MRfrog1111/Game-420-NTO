using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerUI : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject Setings;
    public GameObject PauseMenu;
    public GameObject CraftMenu;
    public GameObject ShopUI;
    
    private bool InventoryOpen = false;
    private bool SetingsOpen = false;
    private bool PauseMenuOpen = false;
    private bool CraftMenuOpen = false;
    private bool ShopUIOpen = false;

    private bool canOpen = true;
    private void Awake()
    {
        Inventory.SetActive(true);
        Inventory.SetActive(false);
        Setings.SetActive(false);
        PauseMenu.SetActive(false);
        CraftMenu.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)) InventoryShow();
        if(Input.GetKeyDown(KeyCode.Escape)) PauseMenuShow();
        if(Input.GetKeyDown(KeyCode.Q)) CraftMenuShow();
        if (Input.GetKeyDown(KeyCode.T)) OpenShop();
    }

    public void OpenShop()
    {
        ShopUIOpen = !ShopUIOpen;
        if(ShopUIOpen && canOpen)
        {
            Cursor.lockState = CursorLockMode.None;
           // Cursor.visible = tr
            ShopUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else 
        {
            Cursor.lockState = CursorLockMode.Locked;
            ShopUI.SetActive(false);
            Time.timeScale = 1f;
        }
        canOpen = !canOpen;
    }
    public void InventoryShow()
    {
        InventoryOpen = !InventoryOpen;
        if(InventoryOpen && canOpen)
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
        canOpen = !canOpen;
    }
    public void SetingsShow()
    {
        SetingsOpen = !SetingsOpen;
        if (SetingsOpen && canOpen)
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

        canOpen = !canOpen;
    }
    public void PauseMenuShow()
    {
        PauseMenuOpen = !PauseMenuOpen;
        if (PauseMenuOpen && canOpen)
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
        canOpen = !canOpen;

    }

    public void CraftMenuShow()
    {
        CraftMenuOpen = !CraftMenuOpen;
        if (CraftMenuOpen && canOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            CraftMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            CraftMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        canOpen = !canOpen;
    }
}
