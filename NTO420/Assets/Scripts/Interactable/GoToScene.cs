using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    [SerializeField] private string sceneName;
    void OnTriggerEnter(Collider coll)
    {
        //print(coll.tag);
        if (coll.CompareTag("Player1"))
        {
            coll.gameObject.GetComponentInParent<PlayerStats>().resources.stage =0;
            coll.gameObject.transform.position = new Vector3(0, 15, 0);
            SceneManager.LoadScene(sceneName);
        }
    }
}
