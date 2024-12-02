using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DefaultNamespace;
public class ShopRequests : MonoBehaviour
{
    public string currentPlayerName;
    public string shopName;
    void Start()
    {
        //StartCoroutine(AddShop(currentPlayerName));
        
    }
    public IEnumerator AddShop(string p_name)
    {
        //WWWForm form = new WWWForm();
        //print("start");
        string url = "https://2025.nti-gamedev.ru/api/games/c94756a8-d518-48fa-90ca-3bb7c23fd1a2/players/"+currentPlayerName+"/shops/";
        ShopResources res = new ShopResources()
        {
            quantum_beacon_of_return = 1,
            atmospheric_filter = 1,
            protective_dome = 1,
            bee_plush = 1,
            bug_plush = 1
        };
        ShopStruct sh = new  ShopStruct()
        {
            name = "shop_norm",
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
        string url = "https://2025.nti-gamedev.ru/api/games/c94756a8-d518-48fa-90ca-3bb7c23fd1a2/players/"+currentPlayerName+"/shops/"+shopName;
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
            string url = "https://2025.nti-gamedev.ru/api/games/c94756a8-d518-48fa-90ca-3bb7c23fd1a2/players/"+currentPlayerName+"/shops/"+shopName+"/";
            UpdateResourcesStruct upd = new UpdateResourcesStruct()
            {
                resources = JsonUtility.ToJson(new_res)
            };
            //print("upd"+upd.resources);
            string json = JsonUtility.ToJson(upd);
            // print(json);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(json);
            UnityWebRequest req = UnityWebRequest.Put(url, data);
            req.SetRequestHeader("Content-Type", "application/json");
            yield return req.SendWebRequest();
           //print("new_re" + new_res.quantum_beacon_of_return);
           /* print("a "+JsonUtility.ToJson(upd));
            byte[] myData = System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(upd));
            UnityWebRequest www = UnityWebRequest.Put(url, myData);
            yield return www.SendWebRequest();*/
            print(req.downloadHandler.text);
        }
    }
}
