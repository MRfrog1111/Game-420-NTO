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

    private  GridData blockData;

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
        if (cellIndicator.transform.childCount>0)
        {
            Destroy(cellIndicator.transform.GetChild(0).gameObject);
        }
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        GameObject newBlock = Instantiate(database.blockData[selectedObjectIndex].Prefab, cellIndicator.transform);
        newBlock.transform.localPosition = new Vector3(0, 0, 0);
        previewRenderer = newBlock.GetComponentInChildren<Renderer>();
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
        newBlock.transform.position = new Vector3(newBlock.transform.position.x, newBlock.transform.position.y-0.1f, newBlock.transform.position.z);
        //gridPosition = grid.WorldToCell(newBlock.transform.position);
        //print("gridPos "+gridPosition);
        gridPosition.y = 0;
       // print("p" + gridPosition);
        placedBlocks.Add(newBlock);
        blockData.AddBlockAt(gridPosition,database.blockData[selectedObjectIndex].Size,
            database.blockData[selectedObjectIndex].ID,placedBlocks.Count-1);
        StopPlacement();
    }

    public void DeleteBlock()
    {
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        if (!blockData.CanPlaceObjectAt(gridPosition,new Vector2Int(1,1)))
        {
            int idx = blockData.DeleteBlockAt(gridPosition);
            Destroy(placedBlocks[idx]);
            placedBlocks.RemoveAt(idx);
        }
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedBlockIndex)
    {
        return blockData.CanPlaceObjectAt(gridPosition, database.blockData[selectedBlockIndex].Size);
    }
    
    public int GetLowerBlock (Vector3 position)
    {
        Vector3Int gridPos = grid.WorldToCell(position);
        gridPos.y = 0;
       //print("p1" + gridPos);
       /// print("pos"+position);
        //print(blockData.GetBlockIndex(gridPos));
        //print(CheckPlacementValidity(gridPos,0));
        if (!CheckPlacementValidity(gridPos, 0))
        {
            return blockData.GetBlockIndex(gridPos);
        }
        return -1;
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
        if (Input.GetMouseButtonDown(1))
        {
            DeleteBlock();
        }
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


