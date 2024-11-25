using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> spawnObjects;

    public List<int> spawnNumber;
    [SerializeField] private bool isSpawning;
    [SerializeField] private float spawnTime;
    [SerializeField] private float spawnRadius;
    private int currentObjectNumber = 0;

    private float x, y,z;
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void  Spawn()
    {
        for (int i = 0; i < spawnObjects.Count; i++)
        {
            for (int j = 0; j < spawnNumber[i]; j++)
            {
               GameObject obj =  GameObject.Instantiate(spawnObjects[i]);
               x = Random.Range(transform.position.x - spawnRadius, transform.position.x + spawnRadius);
               z = Random.Range(transform.position.z - spawnRadius, transform.position.z + spawnRadius);
               y = transform.position.y+1;
               obj.transform.position = new Vector3(x,y,z);
               obj.name = spawnObjects[i].name;
               currentObjectNumber++;
            }
        }
    }
}
