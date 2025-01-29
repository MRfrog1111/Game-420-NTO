using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData : MonoBehaviour
{
    private Dictionary<Vector3Int, PlacementData> placedBlocks = new();

    public void AddBlockAt(Vector3Int gridPosition, Vector2 blockSize, int ID, int blockIndex)
    {
        List<Vector3Int> positionsToOccupy = CalculatePositions(gridPosition, blockSize);
        PlacementData data = new PlacementData(positionsToOccupy, ID, blockIndex);
        foreach (var pos in positionsToOccupy)
        {
            if (placedBlocks.ContainsKey(pos))
            {
                //поругаться на то, что позиция занята 
            }

            placedBlocks[pos] = data;
        }
    }

    private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2 blockSize)
    {
        List<Vector3Int> returnedValues = new();
        for (int x = 0; x < blockSize.x; x++)
        {
            for (int y = 0; y < blockSize.y; y++)
            {
                returnedValues.Add(gridPosition + new Vector3Int(x, 0, y));
            }
        }

        return returnedValues;
    }

    public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2 blockSize)
    {
        List<Vector3Int> positionsToOccupy = CalculatePositions(gridPosition, blockSize);
        foreach (var pos in positionsToOccupy)
        {
            if (placedBlocks.ContainsKey(pos))
            {
                return false;
            }
        }

        return true;
    }
}

internal class PlacementData
{
    public List<Vector3Int> occupiedPositions;
    public int ID { get; private set; }
    public int PlacedObjectIndex{get; private set;}

    public PlacementData(List<Vector3Int> occupiedPositions, int id, int placedObjectIndex)
    {
        this.occupiedPositions = occupiedPositions;
        this.ID = id;
        this.PlacedObjectIndex = placedObjectIndex;
    }
}
