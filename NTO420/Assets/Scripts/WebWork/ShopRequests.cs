using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DefaultNamespace;
public class ShopRequests : MonoBehaviour
{
    public string currentPlayerName;
    public string shopName;
    private string uuid = "36930e60-d1e4-4dfc-8d54-d90f343d9f81";
    void Start()
    {
        currentPlayerName = PlayerPrefs.GetString("PlayerName");
        shopName = "shop1";
        StartCoroutine(AddShop(currentPlayerName));
    }
    public IEnumerator AddShop(string p_name)
    {
        //WWWForm form = new WWWForm();
        //print("start");
        string url = "https://final.2025.nti-gamedev.ru/api/games/"+uuid+"/players/"+currentPlayerName+"/shops/";
        ShopResources res = new ShopResources()
        {
            flower = 1,
            atmospheric_filter = 1,
            bear_figure = 1,
            bee_plush = 1,
            bug_plush = 1
        };
        ShopStruct sh = new  ShopStruct()
        {
            name = "shop1",
            resources = res
        };
        UnityWebRequest www = UnityWebRequest.Post(url,JsonUtility.ToJson(sh),"application/json");
        yield return www.SendWebRequest();
        print(www.downloadHandler.text);
        /*string json = JsonUtility.ToJson(p);
        // print(json);
        UnityWebRequest req = UnityWebRequest.Post(url,json,"application/json");
        //req.SetRequestHeader();
        yield return req.SendWebRequest();*/
        
    }
    

    public IEnumerator GetShop(System.Action<ShopStruct> returnShop)
    {
        string url = "https://final.2025.nti-gamedev.ru/api/games/"+uuid+"/players/"+currentPlayerName+"/shops/"+shopName;
        UnityWebRequest req = UnityWebRequest.Get(url);

        yield return req.SendWebRequest();
        
        //то, что мы делаем после получения запроса
        string json = req.downloadHandler.text;
        ShopStruct response = JsonUtility.FromJson<ShopStruct>(json);
        returnShop(response);
    }
    
    public IEnumerator UpdateShopResources(ShopResources new_res)
    {
        if (currentPlayerName != null){
            string url = "https://final.2025.nti-gamedev.ru/api/games/"+uuid+"/players/"+currentPlayerName+"/shops/"+shopName+"/";
            UpdateShopResourcesStruct upd = new UpdateShopResourcesStruct()
            {
                resources = new_res
            };
            string json = JsonUtility.ToJson(upd);
            // print(json);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(json);
            UnityWebRequest req = UnityWebRequest.Put(url, data);
            req.SetRequestHeader("Content-Type", "application/json");
            yield return req.SendWebRequest();
        }
    }
    
    public IEnumerator SendLog(string comm, ShopChangesLogs changes)
    {
        string url =  "https://final.2025.nti-gamedev.ru/api/games/"+uuid+"/logs/";
        ShopLogs sl = new ShopLogs()
        {
            comment = comm,
            player_name = currentPlayerName,
            shop_name = shopName,
            resources_changed = changes
        };
        UnityWebRequest www = UnityWebRequest.Post(url,JsonUtility.ToJson(sl),"application/json");
        yield return www.SendWebRequest();
        print(www.downloadHandler.text);

    }
}
