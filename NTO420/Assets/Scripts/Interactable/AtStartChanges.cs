using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtStartChanges : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isUpdated = false;
    private GameObject player;
    [SerializeField] private Craft craft;
    //private Tutorial tutor;
    void Start()
    {
         player = GameObject.FindGameObjectWithTag("Player");
         print(player.name);
         player.GetComponent<CharacterController>().enabled = false;
         player.GetComponent<Controller>().enabled = false;
         player.transform.position = new Vector3(0, 15, 0);
         player.GetComponent<CharacterController>().enabled = true;
         player.GetComponent<Controller>().enabled = true;
         craft.FirstUpdate();
        /* if (player.GetComponent<PlayerStats>().resources.stage > 0)
         {
             tutor = GameObject.FindObjectOfType<Tutorial>();
             tutor.FirstUpdate();
         }*/
    }

    // Update is called once per fra
}
