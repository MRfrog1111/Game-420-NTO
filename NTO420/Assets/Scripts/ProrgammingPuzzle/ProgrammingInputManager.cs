using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class ProgrammingInputManager : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private Vector3 lastpos;

    [SerializeField] private LayerMask placementLayerMask;

    public event Action OnClicked, OnExit;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClicked?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnExit?.Invoke();
        }
    }

    public bool isPointerOverUI()
    => EventSystem.current.IsPointerOverGameObject(); // то эе самое, что return
    
    
    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = cam.nearClipPlane;
        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, placementLayerMask))
        {
            lastpos = new Vector3(hit.point.x,0,hit.point.z);
        }
        return lastpos;
    }
}
