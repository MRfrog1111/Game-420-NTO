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
       StartCoroutine(webAsker.GetPlayerResources(GetRes));
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
        StartCoroutine(webAsker.GetPlayerResources(GetRes));
    }
    private void Start()
    {
        //StartCoroutine(rashodOxygen());
        StartCoroutine(rashodFood());
    }

    public void GetRes(PlayerResources res)
    {
        resources = res;
        //print("test" + res.hp);
    }

    public void UpdateRes()
    {
        StartCoroutine(webAsker.UpdatePlayerResources(resources));
        ChangeInventory?.Invoke();
        StartCoroutine(webAsker.GetPlayerResources(GetRes));

       // print("got it" + resources.stage);
    }
    
    
    private void FixedUpdate()
    {/*
        print("hp " + resources.hp);
        print("oxygen " + resources.oxygen);
        print("food " + resources.food);
        
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
           StartCoroutine(webAsker.GetPlayerResources(GetRes));
            if (resources.food > 0)
            {
                
                if (isFirst)
                {
                    onResourcesChange?.Invoke();
                    isFirst = false;
                }
                
                resources.food -= golod;
                PlayerChangesLogs changes = new PlayerChangesLogs()
                {
                    food_change = "-" + golod.ToString()
                };
                //print("stage" + resources.stage);
                StartCoroutine(webAsker.SendLog("player got more hungry", changes));
                StartCoroutine(webAsker.UpdatePlayerResources(resources));
                StartCoroutine(webAsker.GetPlayerResources(GetRes));
            }
        }
    }
    private IEnumerator rashodOxygen()
    {
        while (resources.oxygen >= 0)
        {
            yield return new WaitForSeconds(oxygenTime);
            StartCoroutine(webAsker.GetPlayerResources(GetRes));
            if (resources.oxygen > 0)
            {
                --resources.oxygen;
                StartCoroutine(webAsker.UpdatePlayerResources(resources));
                StartCoroutine(webAsker.GetPlayerResources(GetRes));
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Base")
        {
            resources.oxygen = 100;
            StartCoroutine(webAsker.UpdatePlayerResources(resources));
        }
    }
}
