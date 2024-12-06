using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Tutorial: MonoBehaviour
{
    public PlayerResources[] neededRes;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private PlayerStats stats;
    [SerializeField] private string [] tasks;
   // private PlayerResources currentRes;
    private bool isCompleted = false;
    [SerializeField] private TextMeshProUGUI [] completedTasks;
    private bool isPressedC = false;

   public void FirstUpdate()
   {
       print("updated");
       tutorialText.text = tasks[stats.resources.stage];
       for (int i = 0; i < completedTasks.Length; i++)
       {
           if (stats.resources.stage > i)
           {
               completedTasks[i].text = "Выполнено!";
           }
       }
   }

   private void Update()
   {
       print("stage " + stats.resources.stage);
   }

   private void OnEnable()
    {

        CollectResource.onResourcesChange += CheckStage;
        PlayerStats.onResourcesChange += FirstUpdate;

    }

    private void OnDisable() {

        CollectResource.onResourcesChange -= CheckStage;
        PlayerStats.onResourcesChange -= FirstUpdate;
    }
    
    public void CheckStage()
    {
        //print("check");
        if (stats.resources.stage == 0)
        {
            /*print("check " + stats.resources.honey);
            print("check wax " + stats.resources.wax);*/
            if (stats.resources.honey >= neededRes[0].honey)
            {
                isCompleted = true;
            }
        }
        if (stats.resources.stage == 1)
        {
            /*print("check " + stats.resources.honey);
            print("check wax " + stats.resources.wax);*/
            if (stats.resources.honey >= neededRes[1].honey)
            {
                isCompleted = true;
            }
        }
        if (isCompleted)
        {
            stats.CheckUpdates();
            completedTasks[stats.resources.stage].text = "Выполнено!";
            stats.resources.stage ++;
            stats.UpdateRes();
            tutorialText.text = tasks[stats.resources.stage];
            isCompleted = false;
            
           
        }
    }
}
