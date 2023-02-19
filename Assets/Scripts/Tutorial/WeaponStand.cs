using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponStand : MonoBehaviour
{
 [SerializeField]
 private GameObject sword;
 [SerializeField]
 private GameObject shield;



 private void OnTriggerEnter(Collider other)
 {
  if (other.CompareTag("Player"))
  {
   
    sword.SetActive(true);

    shield.SetActive(true);
   
  }
 }

 private void OnTriggerStay(Collider other)
 {
  if (other.CompareTag("Player"))
  {
   if (other.CompareTag("Player"))
   {

    sword.SetActive(true);

    shield.SetActive(true);

   }
  }
 }
}
