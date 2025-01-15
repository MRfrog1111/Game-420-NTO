using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggingBlocks : MonoBehaviour
{
    [SerializeField] private GameObject mouseIndicator;
    [SerializeField] private ProgrammingInputManager inputManager;

    private void Update()
    {
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        mouseIndicator.transform.position = mousePosition;
    }
}
