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
    public bool isGotHoney;

   public void FirstUpdate()
   {
       print("updated");
       if (stats.resources.stage < tasks.Length)
       {
           tutorialText.text = tasks[stats.resources.stage];
       }

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
       if (stats.resources.stage == 1 && Input.GetKeyDown(KeyCode.C))
       {
           isCompleted = true;
           CheckStage();
       }
       else if (stats.resources.stage == 3 && Input.GetKeyDown(KeyCode.E))
       {
           isCompleted = true;
           CheckStage();
       }
       else if (stats.resources.stage == 4)
       {
           if (stats.resources.apiary_module >= neededRes[0].apiary_module)
           {
               isCompleted = true;
           }
       }
       else if (stats.resources.stage == 5 && isGotHoney)
       {
           isCompleted = true;
           CheckStage();
       }
   }

   private void OnEnable()
    {

       CollectResource.onResourcesChange += CheckStage;
        //PlayerStats.ChangeInventory += CheckStage;
        PlayerStats.onResourcesChange += FirstUpdate;

    }

    private void OnDisable() {

        CollectResource.onResourcesChange -= CheckStage;
        //PlayerStats.ChangeInventory -= CheckStage;
        PlayerStats.onResourcesChange -= FirstUpdate;
    }
    
    public void CheckStage()
    {
       // print("check");
        if (stats.resources.stage == 0)
        {
            /*print("check " + stats.resources.honey);
            print("check wax " + stats.resources.wax);*/
            if (stats.resources.honey >= neededRes[0].honey)
            {
                isCompleted = true;
            }
        }
        else if (stats.resources.stage == 2)
        {
            if (stats.resources.living_module >= neededRes[0].living_module)
            {
                isCompleted = true;
            }
        }
        else if (stats.resources.stage == 3)
        {
            isCompleted = true;
        }
        else if (stats.resources.stage == 4)
        {
            if (stats.resources.apiary_module >= neededRes[0].apiary_module)
            {
                isCompleted = true;
            }
        }
      
        if (isCompleted && stats.resources.stage < tasks.Length)
        {
            completedTasks[stats.resources.stage].text = "Выполнено!";
            stats.resources.stage ++;
            stats.UpdateRes();
            tutorialText.text = tasks[stats.resources.stage];
            //stats.CheckUpdates();
            isCompleted = false;

        }
    }
}
