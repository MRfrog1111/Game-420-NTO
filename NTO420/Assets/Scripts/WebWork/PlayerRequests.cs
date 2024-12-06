using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DefaultNamespace;
public class PlayerRequests : MonoBehaviour
{
    public string currentPlayerName;

    private void Awake()
    {
        currentPlayerName = PlayerPrefs.GetString("PlayerName");
        print(currentPlayerName);
    }
    
    

    public IEnumerator GetPlayerResources(System.Action<PlayerResources> returnResources)
    {
        string url = "https://2025.nti-gamedev.ru/api/games/c94756a8-d518-48fa-90ca-3bb7c23fd1a2/players/"+currentPlayerName+"/";
        UnityWebRequest req = UnityWebRequest.Get(url);

        yield return req.SendWebRequest();
        
        //то, что мы делаем после получения запроса
        string json = req.downloadHandler.text;
        PlayerStruct response = JsonUtility.FromJson<PlayerStruct>(json);
        /*print("start "  + json);
        print("starthp "  + response.resources.hp);*/
        returnResources(response.resources);
    }

    public IEnumerator UpdatePlayerResources(PlayerResources new_res)
    {
        if (currentPlayerName != null){
            string url = "https://2025.nti-gamedev.ru/api/games/c94756a8-d518-48fa-90ca-3bb7c23fd1a2/players/"+currentPlayerName+"/";
            UpdateResourcesStruct upd = new UpdateResourcesStruct()
            {
                resources = new_res
            };
            string json = JsonUtility.ToJson(upd);
            //print(json);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(json);
            UnityWebRequest req = UnityWebRequest.Put(url, data);
            req.SetRequestHeader("Content-Type", "application/json");
            yield return req.SendWebRequest();
        }
    }

    public IEnumerator AddPlayer(string p_name)
    {
        //WWWForm form = new WWWForm();
        string url = "https://2025.nti-gamedev.ru/api/games/c94756a8-d518-48fa-90ca-3bb7c23fd1a2/players/";
        PlayerResources res = new PlayerResources()
        {
            hp = 100,
            food = 100,
            oxygen = 100
        };
        PlayerStruct p = new PlayerStruct()
        {
            name = p_name,
            resources = res
        };
        string json = JsonUtility.ToJson(p);
       // print(json);
        UnityWebRequest req = UnityWebRequest.Post(url,json,"application/json");
        //req.SetRequestHeader();
        yield return req.SendWebRequest();
        
    }

    public IEnumerator GetPlayers(System.Action<PlayersStruct> returnPlayers)
    {
        string url = "https://2025.nti-gamedev.ru/api/games/c94756a8-d518-48fa-90ca-3bb7c23fd1a2/players/";
        UnityWebRequest req = UnityWebRequest.Get(url);

        yield return req.SendWebRequest();
        
        //то, что мы делаем после получения запроса
        string json = "{\"players\":" + req.downloadHandler.text+ "}";
        PlayersStruct response = JsonUtility.FromJson<PlayersStruct>(json);
        returnPlayers(response);
        //print(req.downloadHandler.text);
        //return response;
    }
    public IEnumerator SendLog(string comm, PlayerChangesLogs changes)
    {
        string url =  "https://2025.nti-gamedev.ru/api/games/c94756a8-d518-48fa-90ca-3bb7c23fd1a2/logs/";
        PlayerLogs pl = new PlayerLogs()
        {
            comment = comm,
            player_name = currentPlayerName,
            resources_changed = changes
        };
        //print('a');
        /*UnityWebRequest www = 
            UnityWebRequest.PostWwwForm(url,JsonUtility.ToJson(pl));
        www.SetRequestHeader( "","application/json");*/
        UnityWebRequest www = UnityWebRequest.Post(url,JsonUtility.ToJson(pl),"application/json");
        yield return www.SendWebRequest();
        //print(www.downloadHandler.text);
        
    }
}
