using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DefaultNamespace;
public class Tutorial: MonoBehaviour
{
    public int stage;
    public PlayerResources[] neededRes;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private PlayerStats stats;
    [SerializeField] private string [] tasks;
    private PlayerResources currentRes;
    private bool isCompleted = false;
    private void OnEnable()
    {

        CollectResource.onResourcesChange += CheckStage;

    }

    private void OnDisable() {

        CollectResource.onResourcesChange -= CheckStage;
    }
    public void CheckStage()
    {
        //print("check");
        if (stage == 0)
        {
            /*print("check " + stats.resources.honey);
            print("check wax " + stats.resources.wax);*/
            if (stats.resources.honey >= neededRes[0].honey)
            {
                isCompleted = true;
            }
        }

        if (isCompleted)
        {
            tutorialText.text = tasks[stage];
            stage++;
            isCompleted = false;
        }
    }
}
