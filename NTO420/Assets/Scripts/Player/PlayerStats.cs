using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DefaultNamespace;
public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerRequests webAsker; // отправляет запросы на сервер
    public int golod = 1;
    private PlayerResources resources; // объект, где хранится вся инофрмация игрока
    [SerializeField] private float hungerTime; //время за которое персонаж проголодается
    [SerializeField] private float oxygenTime; //время за которое персонаж потеряет кислород
    
    private void Awake()
    {
        PlayerResources test1 = new PlayerResources()
        {
            hp = 100,
            oxygen = 100,
            food = 100
        };
        StartCoroutine(webAsker.UpdatePlayerResources(test1));
        PlayerResources test = new PlayerResources()
        {
            hp = 100,
            oxygen = 100,
            food = 100
        };// меняет статы на значения в test
        StartCoroutine(webAsker.GetPlayerResources(GetRes)); //обновляет статы в соответствии со значением на сервере
        StartCoroutine(rashodOxygen());
        StartCoroutine(rashodFood());
    }
    public void GetRes(PlayerResources res)
    {
        resources = res;
    }
    
    private void Update()
    {
        
        
        if (Input.GetKey(KeyCode.LeftShift)) golod = 3;
        else golod = 1;

        if(resources.oxygen <= 0)
        {
            print("oxygen " + resources.oxygen);
            resources.oxygen = 0;
            HpMines();
        }

    }
   
   private IEnumerator rashodFood()
    {
        while(true)
        {
            yield return new WaitForSeconds(hungerTime);
            StartCoroutine(webAsker.GetPlayerResources(GetRes));
            print("food " + resources.food);
            print("hp " + resources.hp);
            resources.food -= resources.food * golod;
            PlayerChangesLogs changes = new PlayerChangesLogs()
            {
                food_change = "-" + golod.ToString()
            };
            StartCoroutine(webAsker.SendLog("player got more hungry",changes));
            StartCoroutine(webAsker.UpdatePlayerResources(resources));
            StartCoroutine(webAsker.GetPlayerResources(GetRes));
        }
    }
    private IEnumerator rashodOxygen()
    {
        while (resources.oxygen > 0)
        {
            StartCoroutine(webAsker.GetPlayerResources(GetRes));
            yield return new WaitForSeconds(oxygenTime);
            print("oxygen " + resources.oxygen);
            --resources.oxygen;
            StartCoroutine(webAsker.UpdatePlayerResources(resources));
            StartCoroutine(webAsker.GetPlayerResources(GetRes));
        }
    }

    private IEnumerator HpMines()
    {
        yield return new WaitForSeconds(5f);
        print("hp " + resources.hp);
        resources.hp -= 5;
        
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
