using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_attack : MonoBehaviour
{
   /* PlayerInput playerInput;
    PlayerInput.MainActions input;*/

    CharacterController controller;
    Animator animator;
    AudioSource audioSource;

    Vector3 _PlayerVelocity;
    

    [Header("Camera")]
    public Camera cam;
    public float sensitivity;

    float xRotation = 0f;

    void Awake()
    { 
       // controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();

        Cursor.lockState = CursorLockMode.Locked;
       // Cursor.visible = false;
    }

    void Update()
    {
        // Repeat Inputs
        if(Input.GetMouseButtonDown(0))
        { Attack(); }

        SetAnimations();
    }
    public const string IDLE = "Idle";
    public const string WALK = "Walk";
    public const string ATTACK1 = "Attack 1";
    public const string ATTACK2 = "Attack 2";

    string currentAnimationState;

    public void ChangeAnimationState(string newState) 
    {
        // STOP THE SAME ANIMATION FROM INTERRUPTING WITH ITSELF //
        if (currentAnimationState == newState) return;

        // PLAY THE ANIMATION //
        currentAnimationState = newState;
        animator.CrossFadeInFixedTime(currentAnimationState, 0.2f);
    }

    void SetAnimations()
    {
        // If player is not attacking
        if(!attacking)
        {
            if(_PlayerVelocity.x == 0 &&_PlayerVelocity.z == 0)
            { ChangeAnimationState(IDLE); }
            else
            { ChangeAnimationState(WALK); }
        }
    }
    
    [Header("Attacking")]
    public float attackDistance ;
    public float attackDelay ;
    public float attackSpeed ;
    public int attackDamage ;
    public LayerMask attackLayer;
    public float attackForce;
    public GameObject hitEffect;
    public AudioClip swordSwing;
    public AudioClip hitSound;

    bool attacking = false;
    bool readyToAttack = true;
    int attackCount;
    
    public void Attack()
    {
        if(!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRaycast), attackDelay);

      /*  audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(swordSwing);*/

        if(attackCount == 0)
        {
            ChangeAnimationState(ATTACK1);
            attackCount++;
        }
        else
        {
            ChangeAnimationState(ATTACK2);
            attackCount = 0;
        }
    }

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    void AttackRaycast()
    {
        //print("abc");
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        { 
           // HitTarget(hit.point);
           GameObject enemy = hit.collider.gameObject;
           enemy.GetComponent<Rigidbody>().AddForce(transform.forward*attackForce,ForceMode.Impulse);
             if(enemy.TryGetComponent<HP>(out HP enemy_hp))
            { enemy_hp.GiveDamage(attackDamage); }                         
        } 
    }

    /*void HitTarget(Vector3 pos)
    {
        audioSource.pitch = 1;
        audioSource.PlayOneShot(hitSound);

        GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
        Destroy(GO, 20);
    }*/

    public void Death()
    {
        print("Lox umer");
    }
}

