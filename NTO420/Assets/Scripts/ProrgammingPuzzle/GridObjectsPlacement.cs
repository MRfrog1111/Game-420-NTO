using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public List<GameObject> placedBlocks = new();
    private bool canPlace = false;

    public Savedobjects savedObjects;
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
        mouseIndicator.SetActive(false);
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

    private IEnumerator enableSript(GameObject block)
    {
        yield return new WaitForSecondsRealtime(1f);
        if (block!= null && block.GetComponentInChildren<BlockBehavior>())
        {
            block.GetComponentInChildren<BlockBehavior>().enabled = true;
        }
    }

    public void BlockPlacing(Vector3 pos, int blockID,bool isAdding)
    {
        Vector3Int gridPosition = grid.WorldToCell(pos);       
        GameObject newBlock = Instantiate(database.blockData[blockID].Prefab);
        newBlock.transform.position = grid.CellToWorld(gridPosition);
        newBlock.transform.position = new Vector3(newBlock.transform.position.x, newBlock.transform.position.y-0.1f, newBlock.transform.position.z);
        //print("p" + gridPosition);
        placedBlocks.Add(newBlock);
        blockData.AddBlockAt(gridPosition,database.blockData[blockID].Size,
            database.blockData[blockID].ID,placedBlocks.Count-1);
        if (isAdding)
        {
            SaveableGridObject gridObject = new SaveableGridObject()
            {
                ID = blockID,
                xPos = pos.x,
                yPos = pos.y,
                zPos = pos.z
            };
            savedObjects.savedObjects.gridObjects.Add(gridObject);
        }
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
        BlockPlacing(mousePosition, selectedObjectIndex,true);
        selectedObjectIndex = -1;
        cellIndicator.SetActive(false);
        StopPlacement();
       // print("p2"+placedBlocks[blockData.GetBlockIndex(gridPosition)].gameObject.name);
    }

    void DeleteBlockOnClick()
    {
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        DeleteBlock(mousePosition);
    }
    public void DeleteBlock(Vector3 pos)
    {
        Vector3Int gridPosition = grid.WorldToCell(pos);
        //print(gridPosition);
        if (!blockData.CanPlaceObjectAt(gridPosition,new Vector2Int(1,1)))
        {
            int idx = blockData.DeleteBlockAt(gridPosition);
            //print("delete2"+placedBlocks[idx].gameObject.name);
            Destroy(placedBlocks[idx].gameObject);
            placedBlocks[idx] = null;
            //placedBlocks.RemoveAt(idx);
        }
    }
    
    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedBlockIndex)
    {
        return blockData.CanPlaceObjectAt(gridPosition, database.blockData[selectedBlockIndex].Size);
    }
    
    public int GetLowerBlock (Vector3 position)
    {
        Vector3Int gridPos = grid.WorldToCell(position);
        gridPos.y = -4;
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
        mouseIndicator.SetActive(true);
        inputManager.OnClicked -= PlaceBlock;
        inputManager.OnExit -= StopPlacement;
    }

    //[SerializeField] private GameObje
    void Update()
    {
        if (selectedObjectIndex < 0)
        {
           /* Vector3 mousePosition = inputManager.GetSelectedMapPosition();
            Vector3Int gridPosition = grid.WorldToCell(mousePosition);
            //mouseIndicator.transform.position = grid.CellToWorld(gridPosition);
            mouseIndicator.transform.position = grid.CellToWorld(gridPosition);
            /* bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
            previewRenderer.material.color = placementValidity ? Color.green : Color.red;*/
            /*if (!blockData.CanPlaceObjectAt(gridPosition, new Vector2Int(1, 1)))
            {
                print("test" + gridPosition+" "+blockData.GetBlockPlacedIndex(gridPosition)/*+" "+placedBlocks[blockData.GetBlockIndex(gridPosition)].gameObject.name);
            //}*/
            return;
        }
        if (Input.GetMouseButtonDown(1))
        {
            DeleteBlockOnClick();
        }
        else
        {
            Vector3 mousePosition = inputManager.GetSelectedMapPosition();
            Vector3Int gridPosition = grid.WorldToCell(mousePosition);

            bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
            previewRenderer.material.color = placementValidity ? Color.green : Color.red;

           // mouseIndicator.transform.position = mousePosition;
            cellIndicator.transform.position = grid.CellToWorld(gridPosition);
            /*if (!blockData.CanPlaceObjectAt(gridPosition, new Vector2Int(1, 1)))
            {
                print("test" + gridPosition + " " + blockData.GetBlockPlacedIndex(gridPosition));
            }*/
        }
    }
}




