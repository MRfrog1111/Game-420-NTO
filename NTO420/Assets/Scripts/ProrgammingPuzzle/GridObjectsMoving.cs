using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObjectsMoving : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseIndicator, cellIndicator;
    [SerializeField]
    private ProgrammingInputManager inputManager;
    [SerializeField]
    private Grid grid;
   
    [SerializeField] private ProgrammingBlocksDatabase database;
    private int selectedObject = -1;

    void Start()
    {
        StopPlacement();
    }

    public void StartPlacement(int ID)
    {
        selectedObject = database.blockData.FindIndex(data => data.ID == ID); //находит объект в базе с нужным ID
        
    }
    
    private void StopPlacement()
    {
        throw new System.NotImplementedException();
    }

    //[SerializeField] private GameObje
    void Update()
    {
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);
    }
}
