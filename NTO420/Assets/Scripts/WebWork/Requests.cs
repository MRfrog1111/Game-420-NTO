using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DefaultNamespace;
public class Requests : MonoBehaviour
{
    public string url;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SendRequest());
    }

    private IEnumerator SendRequest()
    {
        UnityWebRequest req = UnityWebRequest.Get(this.url);

        yield return req.SendWebRequest();
        
        //то, что мы делаем после получения запроса
        string json = "{\"players\":" + req.downloadHandler.text+ "}";
        PlayersStruct response = JsonUtility.FromJson<PlayersStruct>(json);
        print(response.players[0].hunger);
    }
}
