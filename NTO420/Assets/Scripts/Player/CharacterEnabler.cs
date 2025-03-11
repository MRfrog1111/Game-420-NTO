using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterEnabler : MonoBehaviour
{
    public void ChangeState(bool isActive)
    {
       // gameObject.GetComponent<MeshRenderer>().enabled = isActive;
        gameObject.GetComponent<CharacterController>().enabled = isActive;
        gameObject.GetComponent<Controller>().enabled = isActive;
        gameObject.GetComponent<MC_attack>().enabled = isActive;
        gameObject.GetComponent<PlayerStats>().isActive=isActive;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(isActive);
        }
    }
}
