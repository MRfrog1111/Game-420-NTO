using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CutsceneEndLoad : MonoBehaviour
{
    void Update()
    {
        if (gameObject.transform.position.x >= 2030)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
