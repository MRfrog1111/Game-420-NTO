using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomGeneration : MonoBehaviour
{
    [SerializeField] private GameObject cube;

    private HeightMap heightMap;
    private void Start()
    {
        cube.transform.position = new Vector3(heightMap.values[0, 0], 0, 0);
    }
}
