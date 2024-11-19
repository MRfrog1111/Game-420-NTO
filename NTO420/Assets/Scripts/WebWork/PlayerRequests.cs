using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DefaultNamespace;
public class PlayerRequests : MonoBehaviour
{
    private IEnumerator UpdatePlayerResources(string url, PlayerResources new_res)
    {

        UpdateResourcesStruct upd = new UpdateResourcesStruct()
        {
            resources = JsonUtility.ToJson(new_res)
        };
        string json = JsonUtility.ToJson(upd);
        print(json);
        byte[] data = System.Text.Encoding.UTF8.GetBytes(json);
        UnityWebRequest req = UnityWebRequest.Put(url,data);
        req.SetRequestHeader("Content-Type", "application/json");
        yield return req.SendWebRequest();
        //print(req.downloadHandler.text);
    }

    private IEnumerator AddPlayer(string url,string p_name)
    {
        //WWWForm form = new WWWForm();
        PlayerStruct p = new PlayerStruct()
        {
            name = p_name,
        };
        string json = JsonUtility.ToJson(p);
        print(json);
        UnityWebRequest req = UnityWebRequest.Post(url,json,"application/json");
        //req.SetRequestHeader();
        yield return req.SendWebRequest();
        
        print(req.downloadHandler.text);
    }

    private IEnumerator GetPlayers(string url)
    {
        UnityWebRequest req = UnityWebRequest.Get(url);

        yield return req.SendWebRequest();
        
        //то, что мы делаем после получения запроса
        string json = "{\"players\":" + req.downloadHandler.text+ "}";
        PlayersStruct response = JsonUtility.FromJson<PlayersStruct>(json);
        //return response;
    }
}
