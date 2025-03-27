using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour
{
    private bool canPressButton = false;

    private GameObject player;
    private Vector3 openPos;
    [SerializeField] private GameObject hint;
    private Animator anim;
    [SerializeField] private int openStage;
    private PlayerStats playerStats;
    static readonly int Open = Animator.StringToHash("Open");
    void Awake()
    {
        anim = GetComponent<Animator>();
       // anim.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
         player = GameObject.FindGameObjectWithTag("Player");
         playerStats = player.GetComponentInParent<PlayerStats>();
         if (playerStats.resources.stage >= openStage)
         {
             anim.SetTrigger(Open);
         }
             
    }
    // Update is called once per frame
    void Update()
    {
        if (canPressButton && Input.GetKeyDown(KeyCode.E)&&playerStats.resources.stage<openStage)
        {
            player.GetComponent<CharacterEnabler>().GotoPuzzle(0);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player1"))
        {
           canPressButton = true;
           hint.SetActive(true);
        }
    }
    void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Player1"))
        {
            canPressButton = false;
            hint.SetActive(false);
        }
    }
    
}
