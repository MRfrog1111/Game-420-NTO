using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerUI : MonoBehaviour
{
    public GameObject Inventory;
    //public GameObject Setings;
    public GameObject PauseMenu;
    public GameObject CraftMenu;
    public GameObject ShopUI;
    public GameObject Tasks;
    private GameObject currentWindow;


    private bool canOpen = true;
    public bool isWorking = true;


    private void Awake()
    {
        Inventory.SetActive(true);
        Inventory.SetActive(false);
        //Setings.SetActive(false);
        PauseMenu.SetActive(false);
        CraftMenu.SetActive(false);
        Tasks.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (canOpen && isWorking)
            {
                Open(Inventory);
            }
            else
            {
                Close(currentWindow);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (canOpen)
            {
                Open(PauseMenu);
            }
            else
            {
                Close(currentWindow);
            }
        }

        if (Input.GetKeyDown(KeyCode.T) && isWorking)
        {
            if (canOpen)
            {
                Open(ShopUI);
            }
            else
            {
                Close(currentWindow);
            }
        }

        if (Input.GetKeyDown(KeyCode.C)&& isWorking)
        {
            if (canOpen)
            {
                Open(Tasks);
            }
            else
            {
                Close(currentWindow);
            }
        }
        
    }

   
        public void Open(GameObject window)
        {
            if (window != null && canOpen)
            {
                window.SetActive(true);
                canOpen = false;
                Cursor.visible = true;
                //CraftMenuOpen = !CraftMenuOpen;
                Cursor.lockState = CursorLockMode.None;
                //CraftMenu.SetActive(true);
                Time.timeScale = 0f;
                currentWindow = window;
            }
        }

        public void Close(GameObject window)
        {
            if (window != null && !canOpen)
            {
                window.SetActive(false);
                canOpen = true;
                Cursor.visible = false;
                //CraftMenuOpen = !CraftMenuOpen;
                Cursor.lockState = CursorLockMode.Locked;
                //CraftMenu.SetActive(true);
                Time.timeScale = 1f;
                currentWindow = null;
            }
        }

        public void OpenCraftMenu()
        {

            /*if (!CraftMenuOpen)
            {
                print("open");
                Cursor.visible = true;
                CraftMenuOpen = !CraftMenuOpen;
                Cursor.lockState = CursorLockMode.None;
                CraftMenu.SetActive(true);
                Time.timeScale = 0f;
                //canOpen = !canOpen;
            }*/
            if (canOpen && isWorking)
            {
                Open(CraftMenu);
            }
        }

    public void ExitInMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}