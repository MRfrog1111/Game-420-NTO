using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DefaultNamespace;
public class PlayerStats : MonoBehaviour
{
    public PlayerRequests webAsker; // отправляет запросы на сервер
    public int golod = 1;
    public PlayerResources resources; // объект, где хранится всяинофрмация игрока
    [SerializeField] private float hungerTime; //время за которое персонаж проголодается
    [SerializeField] private float oxygenTime; //время за которое персонаж потеряет кислород
    public static event Action onResourcesChange;
    public static event Action ChangeInventory;
    private bool isFirst = true;
    public bool isWorking = true;
    private int connection = 1;
    private void Awake()
    {
        //
        
       //print("atart");/*
      /* resources = new PlayerResources()
        {
            hp = 100,
            oxygen = 100,
            food = 100
        };
        StartCoroutine(webAsker.UpdatePlayerResources(resources));*/
      connection = PlayerPrefs.GetInt("Connection");
      if (connection == 0)
      {
          resources = new PlayerResources()
          {
              hp = 100,
              food = 100,
              oxygen = 100
          };
      }
      else
      {
          StartCoroutine(webAsker.GetPlayerResources(GetRes));
      }

      print("awake");
    }
    
    /*private void OnEnable()
    {

        CollectResource.onResourcesChange += CheckUpdates;
        //print("check2 "+resources.honey);

    }

    private void OnDisable() {

        CollectResource.onResourcesChange -= CheckUpdates;

    }*/

    public  void CheckUpdates()
    {
        //StartCoroutine(webAsker.GetPlayerResources(GetRes));
    }
    private void Start()
    {
        StartCoroutine(rashodOxygen());
        StartCoroutine(rashodFood());
    }

    public void GetRes(PlayerResources res)
    {
        resources = res;
        //print("test" + res.hp);
    }

    public void UpdateRes()
    {
        if (connection == 1)
        {
            StartCoroutine(webAsker.UpdatePlayerResources(resources));
        }
        //ChangeInventory?.Invoke();
        //StartCoroutine(webAsker.GetPlayerResources(GetRes));

       // print("got it" + resources.stage);
    }
    
    
    private void FixedUpdate()
    {

        /*if (Input.GetKey(KeyCode.LeftShift))
           {
               if (x != 0 || z != 0) golod = 3;
           }
           else
           {
               golod = 1;
           }*/

    }
   
   private IEnumerator rashodFood()
    {
        while(true)
        {
            yield return new WaitForSeconds(hungerTime);
           // StartCoroutine(webAsker.GetPlayerResources(GetRes));
           if (resources.food > 0)
            {
                if (isFirst)
                {
                    onResourcesChange?.Invoke();
                    isFirst = false;
                }
               // StartCoroutine(webAsker.GetPlayerResources(GetRes));
                resources.food -= golod;
                PlayerChangesLogs changes = new PlayerChangesLogs()
                {
                    food_change = "-" + golod.ToString()
                };
                //print("stage" + resources.stage);
                StartCoroutine(webAsker.SendLog("player got more hungry", changes));
                StartCoroutine(webAsker.UpdatePlayerResources(resources));
               // StartCoroutine(webAsker.GetPlayerResources(GetRes));
            }
        }
    }
   private IEnumerator rashodOxygen()
    {
        while (resources.oxygen >= 0)
        {
            yield return new WaitForSeconds(oxygenTime);
          
            if (resources.oxygen > 0 && resources.atmospheric_filter == 0)
            {
                //StartCoroutine(webAsker.GetPlayerResources(GetRes));
                --resources.oxygen;
                StartCoroutine(webAsker.UpdatePlayerResources(resources));
                //StartCoroutine(webAsker.GetPlayerResources(GetRes));
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Home" && isWorking)
        {
            resources.oxygen = 100;
            StartCoroutine(webAsker.UpdatePlayerResources(resources));
        }
    }
}
