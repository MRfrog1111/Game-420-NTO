using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : MonoBehaviour
{
   [SerializeField] private int damage;
   private void OnTriggerEnter(Collider coll)
   {
      if (coll.CompareTag("Player"))
      {
         coll.gameObject.GetComponent<HP>().GiveDamage(damage);
      }
   }
}
