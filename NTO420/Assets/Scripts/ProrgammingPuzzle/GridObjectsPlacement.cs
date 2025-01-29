using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObjectsPlacement : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseIndicator, cellIndicator;
    [SerializeField]
    private ProgrammingInputManager inputManager;
    [SerializeField]
    private Grid grid;
   
    [SerializeField] private ProgrammingBlocksDatabase database;
    private int selectedObjectIndex = -1;

    private GridData blockData;

    private Renderer previewRenderer;

    private List<GameObject> placedBlocks = new();
    void Start()
    {
        StopPlacement();
        blockData = new ();
        previewRenderer = cellIndicator.GetComponentInChildren<Renderer>();
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

        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (!placementValidity)
        {
            return;
        }
        
        GameObject newBlock = Instantiate(database.blockData[selectedObjectIndex].Prefab);
        newBlock.transform.position = grid.CellToWorld(gridPosition);
        
        placedBlocks.Add(newBlock);
        GridData selectedData = blockData;
        selectedData.AddBlockAt(gridPosition,database.blockData[selectedObjectIndex].Size,
            database.blockData[selectedObjectIndex].ID,placedBlocks.Count-1);
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedBlockIndex)
    {
        GridData selectedData = blockData;
        return selectedData.CanPlaceObjectAt(gridPosition, database.blockData[selectedBlockIndex].Size);
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
        
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        previewRenderer.material.color = placementValidity ? Color.green : Color.red;
        
        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);
    }
}


