using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : MonoBehaviour
{
   [SerializeField] private int damage;

   [SerializeField] private AudioClip bite;
   private void OnTriggerEnter(Collider coll)
   {
      gameObject.GetComponent<AudioSource>().PlayOneShot(bite);
      if (coll.CompareTag("Player"))
      {
         coll.gameObject.GetComponent<HP>().GiveDamage(damage);
      }
   }
}
