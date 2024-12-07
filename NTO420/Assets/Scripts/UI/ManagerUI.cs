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
    public GameObject Tasks;

    


    private bool InventoryOpen = false;
    private bool SetingsOpen = false;
    private bool PauseMenuOpen = false;
    private bool CraftMenuOpen = false;
    private bool ShopUIOpen = false;
    private bool tasksOpen = false;
    private bool canOpen = true;
    

    private void Awake()
    {
        Inventory.SetActive(true);
        Inventory.SetActive(false);
        Setings.SetActive(false);
        PauseMenu.SetActive(false);
        CraftMenu.SetActive(false);
        Tasks.SetActive(false);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InventoryShow();
           
        }
        
        if (Input.GetKeyDown(KeyCode.Escape)) PauseMenuShow();
        if(!CraftMenuOpen && Input.GetKeyDown(KeyCode.E)) CraftMenuClose();
        if (Input.GetKeyDown(KeyCode.T)) OpenShop();
        if (Input.GetKeyDown(KeyCode.C)) OpenTasks();
    }

    public void OpenTasks()
    {
        tasksOpen = !tasksOpen;
        Cursor.visible = tasksOpen;
        if(tasksOpen && canOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            // Cursor.visible = tr
            Tasks.SetActive(true);
            Time.timeScale = 0f;
        }
        else 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Tasks.SetActive(false);
            Time.timeScale = 1f;
        }
        canOpen = !canOpen;
    }
    public void OpenShop()
    {
        ShopUIOpen = !ShopUIOpen;
        Cursor.visible = ShopUIOpen;
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
        Cursor.visible = InventoryOpen;
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
        Cursor.visible = SetingsOpen;
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
        Cursor.visible = PauseMenuOpen;
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

    public void CraftMenuClose()
    {
        
        if (CraftMenuOpen)
        {
            Cursor.visible = false;
            CraftMenuOpen = !CraftMenuOpen;
            Cursor.lockState = CursorLockMode.Locked;
            CraftMenu.SetActive(false);
            Time.timeScale = 1f;
            canOpen = !canOpen;
        }
       
    }
    public void OpenCraftMenu()
    {
        
        if (!CraftMenuOpen)
        {
            Cursor.visible = true;
            CraftMenuOpen = !CraftMenuOpen;
            Cursor.lockState = CursorLockMode.None;
            CraftMenu.SetActive(true);
            Time.timeScale = 0f;
            canOpen = !canOpen;
        }
        
    }

    
}
