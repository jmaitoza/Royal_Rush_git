using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
   public static LevelManager instance;

   public Transform respawnPoint;
   public GameObject playerPrefab;

   private void Awake()
   {
      instance = this; // make global instance
   }

   public void Respawn()
   {
      // Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
   }
}
