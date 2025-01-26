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
    private int selectedObjectIndex = -1;

    void Start()
    {
        StopPlacement();
    }

    public void StartPlacement(int ID)
    {
        selectedObjectIndex = database.blockData.FindIndex(data => data.ID == ID); //находит объект в базе с нужным ID
        if (selectedObjectIndex <0){
            //ошибка, нет ID
            return;
        }
        cellIndicator.SetActive(true);
        inputManager.OnClicked += PlaceBlock;
        inputManager.OnExit += StopPlacement;

    }

    private void PlaceBlock()
    {
        if (inputManager.IsPointerOverUI())
        {
            return;
        }
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        GameObject newBlock = Instantiate(database.blockData[selectedObjectIndex].Prefab);
        newBlock.transform.position = grid.CellToWorld(gridPosition);
    }
    

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        cellIndicator.SetActive(false);
        inputManager.OnClicked -= PlaceBlock;
        inputManager.OnExit -= StopPlacement;
    }

    //[SerializeField] private GameObje
    void Update()
    {
        if (selectedObjectIndex < 0)
        {
            return;
        }
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);
    }
}
