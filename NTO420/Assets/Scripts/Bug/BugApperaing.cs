using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugApperaing : MonoBehaviour
{
    public List<Transform> points = new List<Transform>();
    public  List<GameObject> bugs = new List<GameObject>();
    [SerializeField] private int bugAmount;
    [SerializeField] private GameObject bugPrefab;

    private void SpawnBugs()
    {
        bugs = new List<GameObject>();
        for (int i = 0; i < bugAmount; i++)
        {
            GameObject bug = Instantiate(bugPrefab, points[Random.Range(0,points.Count-1)].position, Quaternion.identity);
            bugs.Add(bug);
        }
    }

    private void DestroyBugs()
    {
        for (int i = 0; i < bugs.Count; i++)
        {
            Destroy(bugs[i].gameObject);
        }
    }
    private void DeleteBugFromList(int obj)
    {
        bugs.Remove(bugs[obj]);
    }
    private void OnEnable()
    {
        DayCycleManager.NightBegun += SpawnBugs;
        DayCycleManager.DayBegun += DestroyBugs;
        BugBehavior.OnDeath += DeleteBugFromList;
    }

    

    private void OnDisable()
    {
        DayCycleManager.NightBegun -= SpawnBugs;
        DayCycleManager.DayBegun -= DestroyBugs;
        BugBehavior.OnDeath -= DeleteBugFromList;
    }
}
