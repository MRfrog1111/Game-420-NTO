using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LoadMap : MonoBehaviour
{
    public GameObject[] maps;
    [SerializeField] private SavePuzzleLevel savePuzzleLevel;
    private int currentMap;
    // Start is called before the first frame update
    void Start()
    {
        currentMap = PlayerPrefs.GetInt("RobotMap");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (currentMap == 0)
        {
            savePuzzleLevel.LevelLoading(Application.dataPath + "/level0.json");
            player.GetComponent<CheckRobotLevels>().robotLevels.Remove(Application.dataPath + "/level0.json");
        }
        else
        {
            int mapIndex = Random.Range(0, player.GetComponent<CheckRobotLevels>().robotLevels.Count - 1);
            savePuzzleLevel.LevelLoading(player.GetComponent<CheckRobotLevels>().robotLevels[mapIndex]);
            player.GetComponent<CheckRobotLevels>().robotLevels.RemoveAt(mapIndex);
            if (player.GetComponent<CheckRobotLevels>().robotLevels.Count == 0)
            {
                player.GetComponent<CheckRobotLevels>().FindRobotLevels();
            }
        }
       ///maps[currentMap].SetActive(true);
    }
}
