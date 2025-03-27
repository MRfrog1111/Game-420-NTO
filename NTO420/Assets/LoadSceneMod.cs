using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneMod : MonoBehaviour
{
    public void File()
    {
        SceneManager.LoadScene("FileLoadingScene");
    }
    public void Texture()
    {
        SceneManager.LoadScene("TextureModding");
    }
    public void Robot()
    {
        SceneManager.LoadScene("RobotLevelRedactor");
    }
}
