using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DefaultNamespace;
public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerRequests webAsker; // отправляет запросы на сервер
    public int golod = 1;
    private PlayerResources resources; // объект, где хранится всяинофрмация игрока
    [SerializeField] private float hungerTime; //время за которое персонаж проголодается
    [SerializeField] private float oxygenTime; //время за которое персонаж потеряет кислород
    
    private void Start()
    {
        PlayerResources test = new PlayerResources()
        {
            hp = 100,
            oxygen = 100,
            food = 100
        };
        StartCoroutine(webAsker.UpdatePlayerResources(test)); // меняет статы на значения в test
        StartCoroutine(webAsker.GetPlayerResources(GetRes)); //обновляет статы в соответствии со значением на сервере
        StartCoroutine(rashodOxygen());
        StartCoroutine(rashodFood());
    }
    public void GetRes(PlayerResources res)
    {
        resources = res;
    }
    
    private void FixedUpdate()
    {
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
        while(resources.food >= 0)
        {
            yield return new WaitForSeconds(hungerTime);
            StartCoroutine(webAsker.GetPlayerResources(GetRes));
            resources.food -=  golod;
            PChangeLogs changes = new PChangeLogs()
            {
                food_change = "-" + golod.ToString()
            };
            //StartCoroutine(webAsker.SendLog("player got more hungry", JsonUtility.ToJson(changes)));
            StartCoroutine(webAsker.UpdatePlayerResources(resources));
            StartCoroutine(webAsker.GetPlayerResources(GetRes));
        }
    }
    private IEnumerator rashodOxygen()
    {
        while (resources.oxygen >= 0)
        {
            StartCoroutine(webAsker.GetPlayerResources(GetRes));
            yield return new WaitForSeconds(oxygenTime);
            --resources.oxygen;
            StartCoroutine(webAsker.UpdatePlayerResources(resources));
            StartCoroutine(webAsker.GetPlayerResources(GetRes));
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
