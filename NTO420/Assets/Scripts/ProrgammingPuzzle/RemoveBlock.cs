using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBlock : MonoBehaviour
{
    private int gameObjectIndex = -1;
    public Grid grid;
    public GridData blockData;

    void DeleteBlock(Vector3Int gridPosition)
    {
        if (blockData.CanPlaceObjectAt(gridPosition, Vector2Int.one)==false)
        {
            //gameObjectIndex = blockData.GetGridIndex(gridPosition);
        }
        if (gameObjectIndex == -1)
            return;
        //blockData.RemoveObjectAt(gridPosition);
        
    }
}
