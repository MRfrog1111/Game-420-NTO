using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangePrefabName : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnChange()
    {
        print(dropdown.value);
    }
}
