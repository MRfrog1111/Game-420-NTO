using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject player;

    private void Start()
    {
        player = GameObject.Find("PlayerCompleteEdition");
    }

    private void Update()
    {
        canvas.transform.LookAt(player.transform);
    }

    
}
