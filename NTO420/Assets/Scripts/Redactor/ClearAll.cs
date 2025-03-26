using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearAll : MonoBehaviour
{
    [SerializeField] private GridObjectsPlacement[] placements;
    public void DeleteAll()
    {
        for (int i = 0; i < placements.Length; i++)
        {
            for (int j = 0; j < placements[i].savedObjects.savedObjects.gridObjects.Count; j++)
            {
                placements[i].DeleteBlock(new Vector3(placements[i].savedObjects.savedObjects.gridObjects[j].xPos, placements[i].savedObjects.savedObjects.gridObjects[j].yPos, placements[i].savedObjects.savedObjects.gridObjects[j].zPos));
            }
        }
    }

   /* public void PlaceAll()
    {
        for (int i = 0; i < placements.Length; i++)
        {
            for (int j = 0; j < placements[i].savedObjects.savedObjects.gridObjects.Count; j++)
            {
                placements[i].BlockPlacing(new Vector3(placements[i].savedObjects.savedObjects.gridObjects[j].xPos, placements[i].savedObjects.savedObjects.gridObjects[j].yPos, placements[i].savedObjects.savedObjects.gridObjects[j].zPos),placements[i].savedObjects.savedObjects.gridObjects[j].ID);
            }
        }
    }*/
}
