using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatArea : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D col)
   {
      if (col.CompareTag("ball"))
      {
         SceneManager.LoadScene(0);
      }
   }
}
