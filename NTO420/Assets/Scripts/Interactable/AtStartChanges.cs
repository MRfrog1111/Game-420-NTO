using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtStartChanges : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isUpdated = false;
    private GameObject player;
    void Start()
    {
         player = GameObject.FindGameObjectWithTag("Player");
         print(player.name);
         player.GetComponent<CharacterController>().enabled = false;
         player.GetComponent<Controller>().enabled = false;
         player.transform.position = new Vector3(0, 15, 0);
         player.GetComponent<CharacterController>().enabled = true;
         player.GetComponent<Controller>().enabled = true;
    }

    // Update is called once per fra
}
