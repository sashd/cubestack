using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private void Start()
    {
        Stop();
    }

   public void Resume()
   {
       Time.timeScale = 1;
   }

   public void Stop()
   {
       Time.timeScale = 0;
   }
   
}
