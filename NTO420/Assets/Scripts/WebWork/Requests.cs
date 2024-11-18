using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using DefaultNamespace;
public class Requests : MonoBehaviour
{
    public string urlGet;

    public string urlPost;

    public string urlPut;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Post());
        StartCoroutine(Put());
        StartCoroutine(Get());
    }

    private IEnumerator Put()
    {
        resources new_r = new resources()
        {
            apples = 45
        };
        UpdateResourcesStruct upd = new UpdateResourcesStruct()
        {
            resources = JsonUtility.ToJson(new_r)
        };
        string json = JsonUtility.ToJson(upd);
        print(json);
        byte[] data = System.Text.Encoding.UTF8.GetBytes(json);
        UnityWebRequest req = UnityWebRequest.Put(this.urlPut,data);
        req.SetRequestHeader("Content-Type", "application/json");
        yield return req.SendWebRequest();
        print(req.downloadHandler.text);
    }

    private IEnumerator Post()
    {
        WWWForm form = new WWWForm();
        /*PostStrut post = new PostStrut()
        {
            id = ''
        } 
        */
        resources r = new resources()
        {
            apples = 2
        };
        PlayerStruct p = new PlayerStruct()
        {
            name = "test3",
            resources = JsonUtility.ToJson(r)
        };
        string json = JsonUtility.ToJson(p);
        print(json);
        UnityWebRequest req = UnityWebRequest.Post(this.urlPost,json,"application/json");
       /* byte[] postByte = Encoding.UTF8.GetBytes(json);
        UploadHandler upload = new UploadHandlerRaw(postByte);
        //UploadHandler upload = new UploadHandlerFile(json);
        req.uploadHandler = upload;*/
        //req.SetRequestHeader();
        yield return req.SendWebRequest();
        
        print(req.downloadHandler.text);
    }

    private IEnumerator Get()
    {
        UnityWebRequest req = UnityWebRequest.Get(this.urlGet);

        yield return req.SendWebRequest();
        
        //то, что мы делаем после получения запроса
        string json = "{\"players\":" + req.downloadHandler.text+ "}";
        PlayersStruct response = JsonUtility.FromJson<PlayersStruct>(json);
        print(req.downloadHandler.text);
    }
}
