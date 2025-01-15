using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgrammingInputManager : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private Vector3 lastpos;

    [SerializeField] private LayerMask placementLayerMask;

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = cam.nearClipPlane;
        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, placementLayerMask))
        {
            lastpos = hit.point;
        }
        return lastpos;
    }
}
