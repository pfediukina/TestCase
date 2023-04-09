using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatArea : MonoBehaviour
{
   public Action OnDefeat;
   
   private void OnTriggerEnter2D(Collider2D col)
   {
      if (col.CompareTag("ball"))
      {
         col.gameObject.SetActive(false);
         OnDefeat?.Invoke();
      }
   }
}
