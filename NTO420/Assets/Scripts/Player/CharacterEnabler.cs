using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterEnabler : MonoBehaviour
{
    [SerializeField] private ManagerUI ui;
    public void ChangeState(bool isActive)
    {
       // gameObject.GetComponent<MeshRenderer>().enabled = isActive;
        gameObject.GetComponent<CharacterController>().enabled = isActive;
        //gameObject.GetComponent<Controller>().enabled = isActive;
        gameObject.GetComponent<MC_attack>().enabled = isActive;
        gameObject.GetComponent<PlayerStats>().isActive=isActive;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.name != "TutorialGleb (1)")
            {
                transform.GetChild(i).gameObject.SetActive(isActive);
            }
        }
    }
    public void GotoPuzzle(int puzzleNum)
    {
        ui.Close(ui.CraftMenu);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        string sceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("ReturnToSceneName", sceneName);
        gameObject.GetComponent<CharacterEnabler>().ChangeState(false);
        PlayerPrefs.SetInt("RobotMap", puzzleNum);
        SceneManager.LoadScene("ProgrammingTest");
    }
}
